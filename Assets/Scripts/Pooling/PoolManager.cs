using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Pooling
{
    public class PoolManager : MonoBehaviourSingleton<PoolManager>
    {
        [SerializeField] private List<Pool> pools;

        private Dictionary<EPool, Pool> _pools;

        protected override void Awake()
        {
            base.Awake();
            
            _pools = new Dictionary<EPool, Pool>();
            foreach (var pool in pools)
            {
                _pools.Add(pool.PoolType, pool);
            }
        }

        public Pool Get(EPool pool) => _pools[pool];
    }
}