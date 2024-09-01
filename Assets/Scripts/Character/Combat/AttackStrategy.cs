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
        
        protected bool AttackInProgress;

        private float _effectiveRangeSq;
        
        private float _nextAttackTime;
        
        public void Init(CharacterModel model)
        {
            InitInternal(model);
        }

        protected abstract void InitInternal(CharacterModel model);

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

        public bool InAttackRange(Vector3 position, Vector3 targetPosition) => (position - targetPosition).sqrMagnitude <= _effectiveRangeSq;

        public bool InAttackSector(Quaternion rotation, Quaternion lookRotation) => Quaternion.Angle(rotation, lookRotation) <= attackAngle;

        protected virtual void AttackFinish()
        {
            AttackInProgress = false;
        }

        protected virtual void AttackStart()
        {
            AttackInProgress = true;
        }

        protected virtual bool CanAttack()
        {
            return !AttackInProgress;
        }
    }
}