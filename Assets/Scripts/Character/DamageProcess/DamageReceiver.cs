namespace Character
{
    public abstract class DamageReceiver : IDamageReceiver
    {
        public IDamageReceiver Next { get; private set; }
        
        public void Damage(float damage)
        {
            damage = DamageInternal(damage);
            Next?.Damage(damage);
        }

        public IDamageReceiver SetNext(IDamageReceiver next)
        {
            Next = next;
            return Next;
        }

        protected abstract float DamageInternal(float damage);
    }
}