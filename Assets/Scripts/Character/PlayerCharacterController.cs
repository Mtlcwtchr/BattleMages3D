using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Character
{
    public class PlayerCharacterController : CharacterController
    {
        [SerializeField] private UnityEngine.Camera followUpCamera;

        public override Vector3 AttackDir => _selectionDir;

        private Vector2 _inputVector;

        private Vector3 _selectionPosition;
        private Vector3 _selectionNormal;
        private Vector3 _selectionDir;

        public Vector3 SelectionPosition => _selectionPosition;

        public Vector3 SelectionNormal => _selectionNormal;
        
        public bool SelectionVisible { get; private set; }

        protected override void Awake()
        {
            _selectionDir = transform.forward;
            OnDead += Dead;
        }

        private void FixedUpdate()
        {
            UpdateSelection();
            UpdateRotation();
            UpdateMove();
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
                _selectionPosition = hitPos;
                _selectionNormal = hit.normal;
                var dir = hitPos - Position;
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
                Entity.SpellBook.SelectNext();
            }
        }

        public void OnPrevSpell(InputAction.CallbackContext inputContext)
        {
            if(inputContext.performed)
            {
                Entity.SpellBook.SelectPrevious();
            }
        }

        private void Dead()
        {
            
        }
    }
}