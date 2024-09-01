using UnityEngine;

namespace Character.Combat
{
    public class DamageController : MonoBehaviour
    {
        [SerializeField] private float damage;
        
        public float Damage { get; set; }
        
        public bool AwaitDamage { get; set; }

        private void Awake()
        {
            Damage = damage;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!AwaitDamage)
                return;

            if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                AwaitDamage = false;
                damageable.Damage(Damage);
            }
        }
    }
}