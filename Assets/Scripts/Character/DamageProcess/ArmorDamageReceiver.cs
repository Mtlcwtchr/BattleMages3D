namespace Character
{
    public class ArmorDamageReceiver : DamageReceiver
    {
        private readonly float _damageModifier;

        public ArmorDamageReceiver(float armor)
        {
            _damageModifier = 1.0f / armor;
        }
        
        protected override float DamageInternal(float damage)
        {
            return damage * _damageModifier;
        }
    }
}