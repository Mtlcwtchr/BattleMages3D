using UnityEngine;

namespace Character.Combat
{
    public class MeleeAttackStrategy : AttackStrategy
    {
        [SerializeField] private DamageController damageController;

        protected override void InitInternal(CharacterModel model)
        {
            damageController.Damage = model.Data.Config.attackDamage;
        }

        protected override void AttackFinish()
        {
            base.AttackFinish();
            
            damageController.AwaitDamage = false;
        }

        protected override void AttackStart()
        {
            base.AttackStart();
            
            damageController.AwaitDamage = true;
        }
    }
}