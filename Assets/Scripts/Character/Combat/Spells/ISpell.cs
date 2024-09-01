using Character.Combat.Spells.Config;

namespace Character.Combat.Spells
{
    public interface ISpell
    {
        public SpellConfig Config { get; }
        
        public void BeginCast(CharacterModel caster);
        public void EndCast(CharacterModel caster);
    }
}