using Character.Combat.Spells.Config;

namespace Character.Combat.Spells
{
    public interface ISpell
    {
        public SpellConfig Config { get; }
        
        public void BeginCast(CharacterController caster);
        public void EndCast(CharacterController caster);
    }
}