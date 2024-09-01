using UnityEngine;

namespace Character
{
    public class RotationController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        private Quaternion _desiredQuat;

        public void SetDesiredRotation(Quaternion desiredRotation)
        {
            _desiredQuat = desiredRotation;
        }
        
        private void FixedUpdate()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _desiredQuat, Time.fixedDeltaTime * rotationSpeed);
        }
    }
}