using System;
using Character.Animation;
using Character.Combat;
using Character.Entity;
using UI;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour, IDamageable
    {
        public event Action OnAttackRequested;

        public event Action<float> OnHpChanged;
        public event Action OnDead;

        [SerializeField] protected MovementController movementController;
        [SerializeField] protected CharacterAnimationController animationController;
        [SerializeField] protected AttackStrategy attackStrategy;
        [SerializeField] protected RotationController rotationController;

        [SerializeField] private EntityConfig entityConfig;
        [SerializeField] private Transform spellCastRoot;
        [SerializeField] private CharacterHud hud;
        
        public bool IsVisible { get; set; }

        public Entity.Entity Entity { get; private set; }

        public Transform SpellCastRoot => spellCastRoot;

        public virtual Vector3 AttackDir => transform.forward;
        
        public Vector3 Position => movementController.Position;

        public Vector2 InputNormalized => movementController.ConsumedInput * movementController.SpeedNormalized;

        public float SpeedNormalized => movementController.SpeedNormalized;

        public void Init()
        {
            Clear();
            Entity = new Entity.Entity(entityConfig);
            IsVisible = true;
            if (attackStrategy is MagicAttackStrategy magicAttackStrategy)
            {
                magicAttackStrategy.SetupSpellBook(Entity.SpellBook);
            }

            if (attackStrategy is MeleeAttackStrategy meleeAttackStrategy)
            {
                meleeAttackStrategy.Damage = entityConfig.attackDamage;
            }
            
            movementController.SetSpeed(Entity.MovementSpeed);
            
            if (hud != null)
            {
                hud.Init(this);
            }
            
            Entity.OnHpChanged += HpChanged;
        }

        private void Clear()
        {
            if (Entity != null)
            {
                Entity.OnHpChanged -= HpChanged;
            }
        }

        protected virtual void Awake() { }

        protected void AttackRequested()
        {
            OnAttackRequested?.Invoke();
        }

        private void HpChanged(float obj)
        {
            OnHpChanged?.Invoke(obj);
        }

        public void Damage(float value)
        {
            var damageReduced = value / Entity.Armor;
            Entity.Hp -= damageReduced;

            if (Mathf.Approximately(Entity.Hp, 0))
            {
                Die();
            }
        }

        private void Die()
        {
            OnDead?.Invoke();
        }
    }
}