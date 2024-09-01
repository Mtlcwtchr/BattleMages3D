using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace Character
{
    public class AICharacterController : CharacterController
    {
        private const float RecalculatePathThreshold = 1.44f;
        
        [SerializeField] private NavMeshAgent agent;
        
        private CharacterModel _target;
        private NavMeshPath _path;
        
        private readonly Vector3[] _corners = new Vector3[256];
        private Vector3 _lastPos;
        private Vector3 _movementVector;
        private Quaternion _desiredRotation;
        
        private bool _rotationMatch;
        private bool _canMove;
        
        private int _cornersCount;
        private int _currentCorner;
        
        private Vector3 TargetDir => _target.Position - Model.Position;
        private int NextCorner => _currentCorner + 1;
        private bool PathFinished => NextCorner >= _cornersCount;
        
        protected override void FixedUpdate()
        {
            if (_target == null || !_target.IsVisible)
                return;
            
            _movementVector = UpdateMovement();
            movementController.AddMovementInput(_movementVector, _movementVector.Convert());

            _rotationMatch = UpdateRotation();
            UpdateAttack();
            
            base.FixedUpdate();
        }

        public void SetTarget(CharacterModel target)
        {
            _target = target;
            RecalculatePath();
        }

        private void UpdateAttack()
        {
            if (attackStrategy.InAttackRange(Model.Position, _target.Position) && _rotationMatch)
            {
                AttackRequested();
            }
        }

        private bool UpdateRotation()
        {
            _desiredRotation = Quaternion.LookRotation(TargetDir, Vector3.up);
            if(_desiredRotation != transform.rotation)
            {
                rotationController.SetDesiredRotation(_desiredRotation);
            }
            return attackStrategy.InAttackSector(transform.rotation, _desiredRotation);
        }

        private Vector3 UpdateMovement()
        {
            if (attackStrategy.InAttackRange(Model.Position, _target.Position))
                return Vector3.zero;

            if (!_canMove)
            {
                RecalculatePath();
                return Vector3.zero;
            }

            var pos = Model.Position;
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

        protected override void Die() { }

        protected override Vector3 GetAttackTarget() => TargetDir;
    }
}