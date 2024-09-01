using Character.Combat.Spells.Config;
using Character.Combat.Spells.Effect;
using Pooling;
using UnityEngine;

namespace Character.Combat.Spells
{
    public class Fireball : ISpell
    {
        private FireballConfig _config;

        public SpellConfig Config => _config;

        private SpellMissile _missile;
        
        public Fireball(SpellConfig config)
        {
            _config = config as FireballConfig;
        }
        
        public void BeginCast(CharacterController caster)
        {
            Debug.Log("Begin cast");
            _missile = PooledBehaviour.Get<SpellMissile>(_config.missile.Type);
            _missile.transform.SetParent(caster.SpellCastRoot, false);
            _missile.LifeTimeMax = _config.maxLifeTime;
            _missile.OnDestroyed += MissileDestroyed;
        }

        public void EndCast(CharacterController caster)
        {
            Debug.Log("End cast");
            if (_missile == null)
                return;

            _missile.Fire(caster.AttackDir, _config.speed, _config.damage);
            _missile = null;
        }

        private void MissileDestroyed(SpellMissile missile)
        {
            missile.OnDestroyed -= MissileDestroyed;

            var angle = 360f / _config.fragmentsCount;
            for (int i = 0; i < _config.fragmentsCount; ++i)
            {
                var dir = Quaternion.Euler(0, i * angle, 0) * Vector3.forward;
                var fragment = PooledBehaviour.Get<SpellMissile>(missile.Type);
                fragment.transform.position = missile.transform.position;
                fragment.LifeTimeMax = _config.fragmentMaxLifeTime;
                fragment.Fire(dir, _config.speed, _config.damage);
            }
        }
    }
}