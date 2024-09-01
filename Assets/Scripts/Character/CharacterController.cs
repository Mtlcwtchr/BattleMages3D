using System;
using Character.Combat;
using UI;
using UnityEngine;

namespace Character
{
    public abstract class CharacterController : MonoBehaviour, IDamageable
    {
        public event Action OnAttackRequested;

        [SerializeField] protected MovementController movementController;
        [SerializeField] protected AttackStrategy attackStrategy;
        [SerializeField] protected RotationController rotationController;
        [SerializeField] private CharacterHud hud;
        [SerializeField] private Transform spellCastRoot;
        
        public ViewRootsData RootsData { get; private set; }
        
        public CharacterModel Model { get; private set; }

        protected virtual void Awake()
        {
            RootsData = new ViewRootsData(transform, spellCastRoot);
        }

        protected virtual void FixedUpdate()
        {
            Model.SetViewData(GetPosition(), GetAbsoluteDir(), GetAttackTarget(), GetSpeedNormalized());
        }

        public void Init(CharacterModel model)
        {
            Model = model;
            Model.Init(this);
            ListenModel(Model);

            attackStrategy.Init(Model);
            movementController.SetSpeed(Model.Data.MovementSpeed);
            
            hud?.Init(Model);
        }

        private void ListenModel(CharacterModel model)
        {
            model.OnCharacterDied += Die;
        }

        protected void AttackRequested()
        {
            OnAttackRequested?.Invoke();
        }

        public void Damage(float value)
        {
            Model.DamageReceiver.Damage(value);
        }

        protected abstract void Die();

        protected virtual Vector3 GetPosition() => transform.position;
        protected virtual Vector2 GetAbsoluteDir() => movementController.ConsumedInput;
        protected virtual float GetSpeedNormalized() => movementController.SpeedNormalized;
        protected abstract Vector3 GetAttackTarget();
    }
}