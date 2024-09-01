using Character.Combat.Spells.Config;
using Character.Combat.Spells.Effect;
using Pooling;
using UnityEngine;

namespace Character.Combat.Spells
{
    public class Heal : ISpell
    {
        private HealConfig _config;

        public SpellConfig Config => _config;

        private SpellEffect _effect;
        
        public Heal(SpellConfig config)
        {
            _config = config as HealConfig;
        }
        
        public void BeginCast(CharacterController caster)
        {
            _effect = PooledBehaviour.Get<SpellEffect>(_config.effect.Type);
            _effect.transform.position = Vector3.zero;
            _effect.transform.SetParent(caster.transform, false);
            _effect.Fire();
        }

        public void EndCast(CharacterController caster)
        {
            if (_effect != null)
            {
                PooledBehaviour.Free(_effect);
                _effect = null;
            }

            caster.Entity.Hp += _config.healValue;
        }
    }
}