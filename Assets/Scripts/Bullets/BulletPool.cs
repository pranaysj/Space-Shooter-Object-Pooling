using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicCuration.Bullets
{
    public class BulletPool
    {
        private BulletView bulletView;
        private BulletScriptableObject bulletScriptableObject;
        private List<PooledBullet> pooledBullets = new List<PooledBullet>();

        public BulletPool(BulletView bulletView, BulletScriptableObject bulletScriptableObject)
        {
            this.bulletView = bulletView;
            this.bulletScriptableObject = bulletScriptableObject;
        }

        public BulletController GetBullet()
        {
            if (pooledBullets.Count > 0)
            {
                PooledBullet pooledBullet = pooledBullets.Find(b => !b.InUsed);
                if (pooledBullet != null)
                {
                    pooledBullet.InUsed = true;
                    return pooledBullet.bullet;
                }
            }
            return CreateNewPooledBullet();
        }

        public void ReturnBullet(BulletController returnBullet)
        {
            PooledBullet pooledBullet = pooledBullets.Find(b => b.bullet.Equals(returnBullet));
            pooledBullet.InUsed = false;
        }

        private BulletController CreateNewPooledBullet()
        {
            PooledBullet pooledBullet = new PooledBullet();
            pooledBullet.bullet = new BulletController(bulletView, bulletScriptableObject);
            pooledBullet.InUsed = true;
            pooledBullets.Add(pooledBullet);
            return pooledBullet.bullet;
        }

        private class PooledBullet
        {
            public BulletController bullet;
            public bool InUsed;
        }       

    }
}
