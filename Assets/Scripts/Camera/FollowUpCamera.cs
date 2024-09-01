using Character;
using UnityEngine;
using Utilities;

namespace Camera
{
    public class FollowUpCamera : MonoBehaviour
    {
        [SerializeField] private MovementController movementController;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float speed;
        [SerializeField] private float threshold;

        private CharacterModel _followUpObject;
        
        private Vector2 _desiredPosition;

        private float _thresholdSq;

        private void Awake()
        {
            if (offset == Vector3.zero)
            {
                offset = transform.position;
            }

            _thresholdSq = threshold * threshold;
            movementController.SetSpeed(speed);
        }

        public void SetFollowingObject(CharacterModel followObject)
        {
            _followUpObject = followObject;
        }

        private void FixedUpdate()
        {
            if (_followUpObject == null)
                return;
            
            _desiredPosition = (_followUpObject.Position + offset).Convert();

            var dir = _desiredPosition - transform.position.Convert();
            if (dir.sqrMagnitude < _thresholdSq)
                return;

            dir.Normalize();
            movementController.AddMovementInput(dir.Convert(), dir);
        }
    }
}