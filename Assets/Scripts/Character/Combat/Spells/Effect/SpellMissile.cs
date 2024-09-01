using System;
using Pooling;
using UnityEngine;

namespace Character.Combat.Spells.Effect
{
    public class SpellMissile : PooledBehaviour
    {
        public event Action<SpellMissile> OnDestroyed;
        
        [SerializeField] private MovementController movementController;
        [SerializeField] private RotationController rotationController;
        [SerializeField] private DamageController damageController;
        
        public float LifeTimeMax { get; set; }

        private float _lifeTime;

        private Vector3 _dir;

        private bool _fired;

        private void OnEnable()
        {
            _lifeTime = 0f;
            _dir = Vector3.zero;
            _fired = false;
            damageController.AwaitDamage = false;
        }

        public void Fire(Vector3 dir, float speed, float damage)
        {
            _fired = true;
            _dir = dir;
            movementController.SetSpeed(speed);
            damageController.Damage = damage;
            damageController.AwaitDamage = true;
            transform.SetParent(null, true);
        }
        
        private void FixedUpdate()
        {
            _lifeTime += Time.fixedDeltaTime;
            if (_lifeTime >= LifeTimeMax)
            {
                Destroy();
            }
            
            UpdateMove();
        }

        private void UpdateMove()
        {
            if (_dir == Vector3.zero)
                return;
            
            movementController.AddMovementInput(_dir, Vector2.up);
        }

        private void Destroy()
        {
            OnDestroyed?.Invoke(this);
            Free(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_fired && _lifeTime >= 0.15f)
            {
                Destroy();
            }
        }
    }
}