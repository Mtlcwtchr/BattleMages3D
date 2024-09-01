using Pooling;
using UnityEngine;

namespace Character.Combat.Spells.Effect
{
    public class SpellEffect : PooledBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        
        public void Fire()
        {
            particles.Play();
        }
    }
}