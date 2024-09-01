using Character.Combat.Spells.Effect;
using UnityEngine;

namespace Character.Combat.Spells.Config
{
    [CreateAssetMenu(fileName = "FireballConfig", menuName = "BattleMages/FireballConfig", order = 0)]
    public class FireballConfig : SpellConfig
    {
        public SpellMissile missile;
        public float damage;
        public float speed;
        public float maxLifeTime;
        public float fragmentsCount;
        public float fragmentMaxLifeTime;
    }
}