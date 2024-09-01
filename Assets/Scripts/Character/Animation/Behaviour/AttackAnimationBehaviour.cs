using System;
using UnityEngine;

namespace Character.Animation.Behaviour
{
    public class AttackAnimationBehaviour : StateMachineBehaviour, IBehaviour
    {
        [SerializeField] private float exitProgress;
        
        public event Action OnEnter;
        public event Action OnExit;

        private bool _entered;
        private bool _exited;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _entered = true;
            _exited = false;
            OnEnter?.Invoke();
            
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_entered)
            {
                _entered = true;
                _exited = false;
                OnEnter?.Invoke();
            }
            
            if (!_exited && stateInfo.normalizedTime >= exitProgress)
            {
                _exited = true;
                OnExit?.Invoke();
            }

            base.OnStateUpdate(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _entered = false;
            
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}
