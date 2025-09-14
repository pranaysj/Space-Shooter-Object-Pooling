using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CosmicCuration.Enemy.EnemyPool;

namespace CosmicCuration.Utilities
{
    public class GenericObjectPool<T> where T : class
    {
        private List<PooledItem<T>> pooledItem = new List<PooledItem<T>>();
        
        public T GetItem()
        {
            if(pooledItem.Count > 0)
            {
                PooledItem<T> item = pooledItem.Find(i => !i.isUsed);

                if(item != null)
                {
                    item.isUsed = true;
                    return item.Item;
                }
            }

            return CreateNewPooledItem();
        }

        private T CreateNewPooledItem()
        {
            PooledItem<T> newItem = new PooledItem<T>();

            newItem.Item = CreateItem();
            newItem.isUsed = true;
            pooledItem.Add(newItem);
            return newItem.Item;
        }

        protected virtual T CreateItem()
        {
            throw new NotImplementedException("CreateItem not implemented");
        }

        public class PooledItem<T>
        {
            public T Item;
            public bool isUsed;
        }

    }
}
