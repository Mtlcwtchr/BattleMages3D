using Character.Combat.Spells.Effect;
using UnityEngine;

namespace Character.Combat.Spells.Config
{
    [CreateAssetMenu(fileName = "HealConfig", menuName = "BattleMages/HealConfig", order = 0)]
    public class HealConfig : SpellConfig
    {
        public SpellEffect effect;
        public float healValue;
    }
}