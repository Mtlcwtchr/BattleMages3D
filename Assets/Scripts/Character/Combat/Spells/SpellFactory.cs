using Character.Combat.Spells.Config;

namespace Character.Combat.Spells
{
    public static class SpellFactory
    {
        public static ISpell Create(SpellConfig config)
        {
            return config.type switch
            {
                ESpell.Fireball => new Fireball(config),
                ESpell.FrostBolt => new FrostBolt(config),
                ESpell.Heal => new Heal(config),
                _ => null,
            };
        }
    }
}