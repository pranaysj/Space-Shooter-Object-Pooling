using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicCuration.Enemy
{
    public class EnemyPool
    {
        private EnemyView enemyPrefab;
        private EnemyData enemyData;
        private List<PooledEnemy> pooledEnemies = new List<PooledEnemy>();

        public EnemyPool(EnemyView enemyPrefab, EnemyData enemyData)
        {
            this.enemyPrefab = enemyPrefab;
            this.enemyData = enemyData;
        }
        public EnemyController GetEnemy()
        {
            if (pooledEnemies.Count > 0)
            {
                PooledEnemy pooledEnemy = pooledEnemies.FirstOrDefault(pe => !pe.inUsed);
                if (pooledEnemy != null)
                {
                    pooledEnemy.inUsed = true;
                    return pooledEnemy.enemy;
                }
            }
            return CreateNewEnemy();
        }

        private EnemyController CreateNewEnemy()
        {
            PooledEnemy newPooledEnemy = new PooledEnemy();
            newPooledEnemy.enemy = CreateEnemy();
            newPooledEnemy.inUsed = true;
            pooledEnemies.Add(newPooledEnemy);
            return newPooledEnemy.enemy;
        }

        private EnemyController CreateEnemy()
        {
            return new EnemyController(enemyPrefab, enemyData);
        }

        public void ReturnEnemy(EnemyController enemy)
        {
            PooledEnemy pooledEnemy = pooledEnemies.Find(pe => pe.enemy.Equals(enemy));
            UnityEngine.Debug.Log("Returning enemy to pool: " + (pooledEnemy != null));
            pooledEnemy.inUsed = false;
        }

        public class PooledEnemy
        {
            public EnemyController enemy;
            public bool inUsed;
        }
    }


}
