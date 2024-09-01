using Character.Animation;
using UnityEngine;

namespace Character.Combat
{
    public abstract class AttackStrategy : MonoBehaviour
    {
        [SerializeField] protected CharacterController character;
        [SerializeField] protected CharacterAnimationController animationController;
        [SerializeField] protected float effectiveRange;
        [SerializeField] private float attackCooldown;
        [SerializeField] private float attackAngle;

        private float _effectiveRangeSq;
        public float EffectiveRange => effectiveRange;
        public float EffectiveRangeSq => _effectiveRangeSq;

        private float _nextAttackTime;

        protected virtual void Awake()
        {
            animationController.SetAttackActions(AttackStart, AttackFinish);

            _effectiveRangeSq = effectiveRange * effectiveRange;
        }

        protected virtual void OnEnable()
        {
            character.OnAttackRequested += CharacterAttackRequested;
        }

        protected virtual void OnDisable()
        {
            character.OnAttackRequested -= CharacterAttackRequested;
        }

        private void CharacterAttackRequested()
        {
            if (!CanAttack() || Time.fixedTime < _nextAttackTime)
                return;
            
            _nextAttackTime = Time.fixedTime + attackCooldown;
            animationController.Attack();
        }

        public bool InAttackRange(Vector3 position, Vector3 targetPosition) => (position - targetPosition).sqrMagnitude <= EffectiveRangeSq;

        public bool InAttackSector(Quaternion rotation, Quaternion lookRotation) => Quaternion.Angle(rotation, lookRotation) <= attackAngle;

        protected abstract void AttackFinish();

        protected abstract void AttackStart();

        protected abstract bool CanAttack();
    }
}