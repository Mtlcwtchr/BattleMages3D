using System;
using Pooling;
using UnityEngine;

namespace Character
{
    public class Enemy : PooledBehaviour
    {
        public event Action<Enemy> OnFreed;
        
        [SerializeField] private AICharacterController controller;

        public void Init()
        {
            controller.Init();
        }

        public override void Get()
        {
            controller.OnDead += Dead;
            
            base.Get();
        }

        public override void Free()
        {
            controller.OnDead -= Dead;
            OnFreed?.Invoke(this);
            
            base.Free();
        }

        private void Dead()
        {
            Free(this);
        }

        public void SetTarget(CharacterController target)
        {
            controller.SetTarget(target);
        }
    }
}