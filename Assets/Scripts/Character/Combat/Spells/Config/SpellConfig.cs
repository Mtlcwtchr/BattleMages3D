using UnityEngine;

namespace Character.Combat.Spells.Config
{
    public abstract class SpellConfig : ScriptableObject
    {
        public ESpell type;

        public Sprite icon;
    }
}