using System;
using Pooling;
using UnityEngine;

namespace Character
{
    public class Enemy : PooledBehaviour
    {
        public event Action<Enemy> OnFreed;
        
        [SerializeField] private AICharacterController controller;
        [SerializeField] private CharacterConfig config;
        
        private CharacterModel _model;
        
        public void Init(CharacterModel model)
        {
            _model = model;
            _model.OnCharacterDied += Dead;
            
            controller.Init(_model);
        }

        public override void Free()
        {
            _model.OnCharacterDied -= Dead;
            OnFreed?.Invoke(this);
            
            base.Free();
        }

        private void Dead()
        {
            Free(this);
        }

        public void SetTarget(CharacterModel target)
        {
            controller.SetTarget(target);
        }
    }
}