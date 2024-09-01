namespace Character
{
    public class HealthDamageReceiver : DamageReceiver
    {
        private CharacterData _data;

        public HealthDamageReceiver(CharacterData data)
        {
            _data = data;
        }
        
        protected override float DamageInternal(float damage)
        {
            _data.Hp -= damage;
            return 0;
        }
    }
}