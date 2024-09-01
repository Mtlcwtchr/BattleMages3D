using System.Collections.Generic;
using Character.Combat.Spells.Config;
using Pooling;
using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "BattleMages/CharacterConfig", order = 0)]
    public class CharacterConfig : ScriptableObject
    {
        public EPool type;
        public Sprite icon;
        public float maxHp;
        public float armor;
        public float movementSpeed;
        public float attackDamage;

        public List<SpellConfig> spells;
    }
}