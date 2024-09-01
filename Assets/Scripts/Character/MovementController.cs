using UnityEngine;
using Utilities;

namespace Character
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float strafeFactor;
        [SerializeField] private float movementSmoothTime;
        [SerializeField] private float stopSmoothTime;
        [SerializeField] private float maxMoveAngle;
        [SerializeField] private Rigidbody rb;
        
        private Vector2 _requestedInput;
        private Vector3 _movementInput;
        private Vector3 _lastMoveVector;
        
        private float _speed;
        private float _strafeSpeed;
        private float _currentSpeed;
        private float _movementRequestTime;
        private float _stopTime;
        
        private bool _inputConsumed;
        private bool _idle;
        private bool _strafe;

        private float MovementSpeed => _strafe ? _strafeSpeed : _speed;
        
        public float SpeedNormalized => _currentSpeed / MovementSpeed;

        public Vector2 ConsumedInput { get; private set; }

        public void SetSpeed(float speed)
        {
            _speed = speed;
            _strafeSpeed = speed * strafeFactor;
        }

        public void AddMovementInput(Vector3 movementRequest, Vector2 inputVector)
        {
            _movementInput = movementRequest;
            _strafe = inputVector.y <= Mathf.Epsilon;
            _requestedInput = inputVector;
            _inputConsumed = false;
        }

        private void FixedUpdate()
        {
            var dir = ConsumeMovementInput();
            
            if (dir == Vector3.zero ||
                ( !_idle && ConsumedInput.Opposite(_requestedInput)))
            {
                UpdateIdle();
                return;
            }
            
            _lastMoveVector = dir;
            UpdateMovement(dir);
        }

        private void UpdateIdle()
        {
            if (_idle)
                return;
            
            UpdateDeceleration();
            _currentSpeed = Mathf.Lerp(MovementSpeed, 0, _stopTime / stopSmoothTime);
            Move(_lastMoveVector * (_currentSpeed * Time.fixedDeltaTime));
        }

        private void UpdateDeceleration()
        {
            _stopTime += Time.fixedDeltaTime;
            if (_stopTime >= stopSmoothTime)
            {
                _currentSpeed = 0f;
                _movementRequestTime = 0;
                _lastMoveVector = Vector3.zero;
                ConsumedInput = Vector2.zero;
                _idle = true;
            }
        }

        private void UpdateMovement(Vector3 dir)
        {
            ConsumedInput = _requestedInput;
            _idle = false;
            _stopTime = 0f;
            _movementRequestTime += Time.fixedDeltaTime;
            _currentSpeed = Mathf.Lerp(0, MovementSpeed, _movementRequestTime / movementSmoothTime);
            Move(dir * (_currentSpeed * Time.fixedDeltaTime));
        }

        private void Move(Vector3 velocity)
        {
            var canMove = true;
            if (rb != null && rb.SweepTest(velocity, out var hit))
            {
                if (hit.collider.isTrigger && (Vector3.Angle(hit.normal, Vector3.up) <= maxMoveAngle))
                {
                    canMove = false;
                }
            }

            if (!canMove)
                return;
            
            transform.position += velocity;
        }

        private Vector3 ConsumeMovementInput()
        {
            if (_inputConsumed)
                return Vector3.zero;

            _inputConsumed = true;
            return _movementInput;
        }
    }
}