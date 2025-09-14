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
        private List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();
        
        public T GetItem()
        {
            if(pooledItems.Count > 0)
            {
                PooledItem<T> item = pooledItems.Find(i => !i.isUsed);

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
            pooledItems.Add(newItem);
            return newItem.Item;
        }

        protected virtual T CreateItem()
        {
            throw new NotImplementedException("CreateItem not implemented");
        }

        protected void ReturnItem(T item)
        {
            PooledItem<T> pooleditem = pooledItems.Find(i => i.Item.Equals(item));
            pooleditem.isUsed = false;
        }

        public class PooledItem<T>
        {
            public T Item;
            public bool isUsed;
        }

    }
}
