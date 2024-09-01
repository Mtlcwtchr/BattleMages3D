using Character.Combat.Spells.Effect;
using UnityEngine;

namespace Character.Combat.Spells.Config
{
    [CreateAssetMenu(fileName = "FrostBoltConfig", menuName = "BattleMages/FrostBoltConfig", order = 0)]
    public class FrostBoltConfig : SpellConfig
    {
        public SpellMissile missile;
        public float damage;
        public float speed;
        public float maxLifeTime;
    }
}