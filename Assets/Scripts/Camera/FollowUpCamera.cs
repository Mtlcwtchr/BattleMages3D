using Character;
using UnityEngine;
using Utilities;
using CharacterController = Character.CharacterController;

namespace Camera
{
    public class FollowUpCamera : MonoBehaviour
    {
        [SerializeField] private MovementController movementController;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float threshold;

        private CharacterController _followUpObject;
        
        private Vector2 _desiredPosition;

        private float _thresholdSq;

        private void Awake()
        {
            if (offset == Vector3.zero)
            {
                offset = transform.position;
            }

            _thresholdSq = threshold * threshold;
        }

        public void SetFollowingObject(CharacterController followObject)
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