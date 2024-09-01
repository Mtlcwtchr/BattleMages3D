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
            for (var i = 0; i < pools.Count; i++)
            {
                _pools.Add(pools[i].PoolType, pools[i]);
            }
        }

        public Pool Get(EPool pool) => _pools[pool];
    }
}