﻿using Character.Combat.Spells.Config;
using Character.Combat.Spells.Effect;
using Pooling;
using UnityEngine;

namespace Character.Combat.Spells
{
    public class FrostBolt : ISpell
    {
        private FrostBoltConfig _config;

        public SpellConfig Config => _config;

        private SpellMissile _missile;
        
        public FrostBolt(SpellConfig config)
        {
            _config = config as FrostBoltConfig;
        }
        
        public void BeginCast(CharacterController caster)
        {
            _missile = PooledBehaviour.Get<SpellMissile>(_config.missile.Type);
            _missile.transform.rotation = Quaternion.LookRotation(Vector3.up);
            _missile.transform.SetParent(caster.SpellCastRoot, false);
            _missile.LifeTimeMax = _config.maxLifeTime;
        }

        public void EndCast(CharacterController caster)
        {
            if (_missile == null)
                return;

            _missile.Fire(caster.AttackDir, _config.speed, _config.damage);
            _missile = null;
        }
    }
}