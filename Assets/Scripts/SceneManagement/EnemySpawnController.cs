using System.Collections.Generic;
using Character;
using Pooling;
using UnityEngine;
using Utilities;

namespace SceneManagement
{
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField] private List<CharacterConfig> enemies;
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private int maxEnemies;
        [SerializeField] private float spawnCooldown;

        private CharacterModel _target;
        private HashSet<Enemy> _enemiesOnScene;

        private float _nextSpawnTime;

        private void Awake()
        {
            _enemiesOnScene = new HashSet<Enemy>();
        }

        private void Update()
        {
            if (_target == null)
                return;
            
            if (_enemiesOnScene.Count < maxEnemies)
            {
                SpawnEnemy();
            }
        }
        
        public void SetTarget(CharacterModel player)
        {
            _target = player;
        }

        private void SpawnEnemy()
        {
            if (_nextSpawnTime > Time.time)
                return;

            _nextSpawnTime = Time.time + spawnCooldown;
            
            var enemy = enemies.RandomElement();
            var spawn = spawnPoints.RandomElement();

            var enemyInstance = PooledBehaviour.Get<Enemy>(enemy.type);
            var model = new CharacterModel(enemy);
            
            enemyInstance.Init(model);
            enemyInstance.transform.SetPositionAndRotation(spawn.position, spawn.rotation);
            enemyInstance.SetTarget(_target);
            
            _enemiesOnScene.Add(enemyInstance);
            
            enemyInstance.OnFreed += EnemyFreed;
        }

        private void EnemyFreed(Enemy enemy)
        {
            enemy.OnFreed -= EnemyFreed;

            _enemiesOnScene.Remove(enemy);
        }
    }
}