using System;
using Character.Animation.Behaviour;
using UnityEngine;

namespace Character.Animation
{
    public class CharacterAnimationController : MonoBehaviour
    {
        private static readonly int AttackParam = Animator.StringToHash("Attack");
        private static readonly int ForwardParam = Animator.StringToHash("Forward");
        private static readonly int StrafeParam = Animator.StringToHash("Strafe");
        private static readonly int SpeedParam = Animator.StringToHash("Speed");
        
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController character;
        
        private AttackAnimationBehaviour _attackBehaviour;
        private Action _attackStart;
        private Action _attackFinish;

        private Vector2 _lastInput;

        public void SetAttackActions(Action attackStart, Action attackFinish)
        {
            _attackStart = attackStart;
            _attackFinish = attackFinish;
        }

        private void OnEnable()
        {
            _attackBehaviour = animator.GetBehaviour<AttackAnimationBehaviour>();
            _attackBehaviour.OnEnter += AttackEnter;
            _attackBehaviour.OnExit += AttackEnd;
        }

        private void OnDisable()
        {
            _attackBehaviour.OnEnter -= AttackEnter;
            _attackBehaviour.OnExit -= AttackEnd;
        }

        private void FixedUpdate()
        {
            var input = character.InputNormalized;
            var speed = character.SpeedNormalized;
            animator.SetFloat(SpeedParam, speed);
            if (_lastInput != input)
            {
                _lastInput = input;
                animator.SetFloat(StrafeParam, _lastInput.x);
                animator.SetFloat(ForwardParam, _lastInput.y);
            }
        }

        private void AttackEnter()
        {
            Debug.Log("Attack enter");
            _attackStart?.Invoke();
        }

        private void AttackEnd()
        {
            Debug.Log("Attack end");
            _attackFinish?.Invoke();
        }

        public void Attack()
        {
            Debug.Log("Attack request");
            animator.SetTrigger(AttackParam);
        }
    }
}