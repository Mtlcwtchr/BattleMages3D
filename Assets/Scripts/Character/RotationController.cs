using UnityEngine;

namespace Character
{
    public class RotationController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        protected Quaternion _desiredQuat;

        public void SetDesiredRotation(Quaternion desiredRotation)
        {
            _desiredQuat = desiredRotation;
        }
        
        protected virtual void FixedUpdate()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _desiredQuat, Time.fixedDeltaTime * rotationSpeed);
        }
    }
}