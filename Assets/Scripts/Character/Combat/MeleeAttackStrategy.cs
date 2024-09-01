using UnityEngine;

namespace Character.Combat
{
    public class MeleeAttackStrategy : AttackStrategy
    {
        [SerializeField] private DamageController damageController;
        
        private bool _attackInProgress;
        
        public float Damage
        {
            get => damageController.Damage;
            set => damageController.Damage = value;
        }
        
        protected override void AttackFinish()
        {
            _attackInProgress = false;
            damageController.AwaitDamage = false;
        }

        protected override void AttackStart()
        {
            _attackInProgress = true;
            damageController.AwaitDamage = true;
        }

        protected override bool CanAttack()
        {
            return !_attackInProgress;
        }
    }
}