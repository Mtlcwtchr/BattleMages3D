using UnityEngine;

namespace Pooling
{
    public class PooledBehaviour : MonoBehaviour
    {
        [SerializeField] private EPool type;

        public EPool Type => type;

        public virtual void Get()
        {
            gameObject.SetActive(true);
        }

        public virtual void Free()
        {
            gameObject.SetActive(false);
        }
        
        public static T Get<T>(EPool type) where T : PooledBehaviour
        {
            return PoolManager.Instance.Get(type).Get<T>();
        }

        public static void Free<T>(T obj) where T : PooledBehaviour
        {
            PoolManager.Instance.Get(obj.Type).Free(obj);
        }
    }
}