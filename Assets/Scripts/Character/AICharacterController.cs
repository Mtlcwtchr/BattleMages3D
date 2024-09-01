using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace Character
{
    public class AICharacterController : CharacterController
    {
        [SerializeField] private NavMeshAgent agent;

        private const float RecalculatePathThreshold = 1.44f;

        private NavMeshPath _path;
        private bool _canMove;
        private Vector3[] _corners = new Vector3[256];
        private int _cornersCount;
        private int _currentCorner;
        private int NextCorner => _currentCorner + 1;

        private bool PathFinished => NextCorner >= _cornersCount;

        private Vector3 _lastPos;
        
        private CharacterController _target;

        private Vector3 _movementVector;

        private Vector3 TargetDir => _target.Position - Position;

        private Vector2 InputVector => transform.InverseTransformVector(_movementVector).Convert();

        private Quaternion _desiredRotation;
        private Quaternion Rotation => transform.rotation;

        private bool _rotationMatch;

        public void SetTarget(CharacterController target)
        {
            _target = target;
            RecalculatePath();
        }
        
        private void FixedUpdate()
        {
            if (_target == null || !_target.IsVisible)
                return;
            
            _movementVector = UpdateMovement();
            movementController.AddMovementInput(_movementVector, InputVector);

            _rotationMatch = UpdateRotation();
            UpdateAttack();
        }

        private void UpdateAttack()
        {
            if (attackStrategy.InAttackRange(Position, _target.Position) && _rotationMatch)
            {
                AttackRequested();
            }
        }

        private bool UpdateRotation()
        {
            _desiredRotation = Quaternion.LookRotation(TargetDir, Vector3.up);
            if(_desiredRotation != Rotation)
            {
                rotationController.SetDesiredRotation(_desiredRotation);
            }
            return attackStrategy.InAttackSector(Rotation, _desiredRotation);
        }

        private Vector3 UpdateMovement()
        {
            if (attackStrategy.InAttackRange(Position, _target.Position))
                return Vector3.zero;

            if (!_canMove)
            {
                RecalculatePath();
                return Vector3.zero;
            }

            var pos = Position;
            var delta = pos - _lastPos;
            
            if (delta.sqrMagnitude >= RecalculatePathThreshold || PathFinished)
            {
                _lastPos = pos;
                return RecalculatePath();
            }

            if (PathFinished)
                return _movementVector;
            
            var cornersDelta = (_corners[NextCorner] - _corners[_currentCorner]).sqrMagnitude;
            if (delta.sqrMagnitude >= cornersDelta || _movementVector == Vector3.zero)
            {
                _lastPos = pos;
                return AdvancePath();
            }

            return _movementVector;
        }

        private Vector3 AdvancePath()
        {
            ++_currentCorner;
            if (PathFinished)
                return Vector3.zero;
            
            var current = _corners[_currentCorner];
            var next = _corners[NextCorner];
            var dir = next - current;
            return dir.normalized;
        }

        private Vector3 RecalculatePath()
        {
            _currentCorner = -1;
            _path = new NavMeshPath();
            _canMove = agent.CalculatePath(_target.Position, _path);
            _cornersCount = _path.GetCornersNonAlloc(_corners);
            return AdvancePath();
        }
    }
}