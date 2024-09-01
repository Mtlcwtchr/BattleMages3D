using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] private EPool poolType;
        [SerializeField] private GameObject template;
        [Header("Capacity")]
        [SerializeField] private int initCapacity;
        [SerializeField] private int capacityIncrease;
        [SerializeField] private int maxCapacity;

        public EPool PoolType => poolType;

        private int _currentCapacity;
        
        private Stack<GameObject> _cachedObjects;
        private HashSet<GameObject> _busyObjects;

        private void Awake()
        {
            _cachedObjects = new Stack<GameObject>();
            _busyObjects = new HashSet<GameObject>();
            CreateInstances(initCapacity);
        }

        private void CreateInstances(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                if (_currentCapacity >= maxCapacity)
                    return;
                
                var go = Instantiate(template, transform);
                go.SetActive(false);
                _cachedObjects.Push(go);
                _currentCapacity++;
            }
        }

        public T Get<T>() where T : PooledBehaviour
        {
            if (_cachedObjects.Count < 1)
            {
                CreateInstances(capacityIncrease);
            }

            if (_cachedObjects.TryPop(out var obj))
            {
                if (obj.TryGetComponent<T>(out var go))
                {
                    go.Get();
                    go.transform.SetParent(null);
                    go.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                    _busyObjects.Add(obj);
                    return go;
                }

                return null;
            }

            return null;
        }

        public void Free<T>(T t) where T : PooledBehaviour
        {
            var go = t.gameObject;
            if (!_busyObjects.Contains(go))
                return;
            
            go.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            go.transform.SetParent(transform, false);
            t.Free();
            _busyObjects.Remove(go);
            _cachedObjects.Push(go);
        }
    }
}