using UnityEngine;

namespace Core
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        private static T _instance;
        public static T Instance => _instance;

        protected virtual void Awake()
        {
            _instance = (T)this;
        }

        private void OnDestroy()
        {
            _instance = null;
        }
    }
}