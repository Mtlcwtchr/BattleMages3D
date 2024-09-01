using System;

namespace Character.Animation.Behaviour
{
    public interface IBehaviour
    {
        public event Action OnEnter;
        public event Action OnExit;
    }
}