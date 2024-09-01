using UnityEngine;
using Utilities;

namespace Character
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float strafeSpeed;
        [SerializeField] private float movementSmoothTime;
        [SerializeField] private float stopSmoothTime;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float maxMoveAngle;
        
        private float _speed;
        private float _strafeSpeed;
        
        private Vector3 _position;
        
        public Vector3 Position => _position;

        public Vector2 ConsumedInput => _consumedInput;
        
        public float SpeedNormalized => _currentSpeed / MovementSpeed;

        private float MovementSpeed => _strafe ? _strafeSpeed : _speed;
        
        private bool _inputConsumed;

        private Vector2 _requestedInput;
        private Vector2 _consumedInput;
        private Vector3 _movementInput;

        private float _currentSpeed;
        private float _movementRequestTime;

        private float _stopTime;

        private Vector3 _lastMoveVector;

        private bool _idle;

        private bool _strafe;

        public void SetSpeed(float speed)
        {
            var delta = strafeSpeed / movementSpeed;
            movementSpeed = speed;
            strafeSpeed = speed * delta;
        }

        public void AddMovementInput(Vector3 movementRequest, Vector2 inputVector)
        {
            _movementInput = movementRequest;
            _strafe = inputVector.y <= Mathf.Epsilon;
            _requestedInput = inputVector;
            _inputConsumed = false;
        }


        private void Awake()
        {
            _position = transform.position;
            _speed = movementSpeed;
            _strafeSpeed = strafeSpeed;
        }

        private void FixedUpdate()
        {
            var dir = ConsumeMovementInput();
            
            if (dir == Vector3.zero ||
                ( !_idle && _consumedInput.Opposite(_requestedInput)))
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
                _idle = true;
            }
        }

        private void UpdateMovement(Vector3 dir)
        {
            _consumedInput = _requestedInput;
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
            _position = transform.position;
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