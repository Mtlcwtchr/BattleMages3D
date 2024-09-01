using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Character
{
    public class PlayerCharacterController : CharacterController
    {
        [SerializeField] private UnityEngine.Camera followUpCamera;

        private Vector2 _inputVector;

        private Vector3 _selectionDir;

        public Vector3 SelectionPosition { get; private set; }

        public Vector3 SelectionNormal { get; private set; }

        public bool SelectionVisible { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            _selectionDir = transform.forward;
        }

        protected override void FixedUpdate()
        {
            UpdateSelection();
            UpdateRotation();
            UpdateMove();
            
            base.FixedUpdate();
        }

        private void UpdateMove()
        {
            if (_inputVector == Vector2.zero)
                return;

            var movementVector = transform.rotation * _inputVector.Convert();
            movementController.AddMovementInput(movementVector, _inputVector);
        }

        private void UpdateSelection()
        {
            var mousePos = Input.mousePosition;
            var ray = followUpCamera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out var hit))
            {
                SelectionVisible = true;
                var hitPos = hit.point;
                SelectionPosition = hitPos;
                SelectionNormal = hit.normal;
                var dir = hitPos - Model.Position;
                dir.Normalize();
                _selectionDir = dir;
            }
            else
            {
                SelectionVisible = false;
            }
        }

        private void UpdateRotation()
        {
            var desiredEuler = Quaternion.LookRotation(_selectionDir).eulerAngles;
            desiredEuler.x = 0;
            desiredEuler.z = 0;
            rotationController.SetDesiredRotation(Quaternion.Euler(desiredEuler));
        }
        
        public void OnMove(InputAction.CallbackContext inputContext)
        {
            _inputVector = inputContext.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext inputContext)
        {
            if(inputContext.performed)
            {
                AttackRequested();
            }
        }

        public void OnNextSpell(InputAction.CallbackContext inputContext)
        {
            if(inputContext.performed)
            {
                Model.SpellBook.SelectNext();
            }
        }

        public void OnPrevSpell(InputAction.CallbackContext inputContext)
        {
            if(inputContext.performed)
            {
                Model.SpellBook.SelectPrevious();
            }
        }

        protected override void Die() { }

        protected override Vector3 GetAttackTarget() => _selectionDir;
    }
}