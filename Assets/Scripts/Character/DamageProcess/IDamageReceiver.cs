namespace Character
{
    public interface IDamageReceiver
    {
        public IDamageReceiver Next { get; }
        
        public void Damage(float damage);

        public IDamageReceiver SetNext(IDamageReceiver next);
    }
}