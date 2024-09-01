using System.Collections.Generic;
using Character.Combat.Spells.Config;
using UnityEngine;

namespace Character.Entity
{
    [CreateAssetMenu(fileName = "EntityConfig", menuName = "BattleMages/EntityConfig", order = 0)]
    public class EntityConfig : ScriptableObject
    {
        public Sprite icon;
        public float maxHp;
        public float armor;
        public float movementSpeed;
        public float attackDamage;

        public List<SpellConfig> spells;
    }
}