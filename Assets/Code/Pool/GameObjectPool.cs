using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.Pool
{
    public class GameObjectPool<T> where T : Component
    {
        private readonly Queue<T> objectPool;
        private readonly T prefab;
        private const int RefillCount = 15;
        private int countObject;
        public GameObjectPool(T prefab, int initialSize = 10)
        {
            countObject = 0;
            this.prefab = prefab;

            objectPool = new Queue<T>(initialSize);
            FillPool(initialSize);
        }
        
        private void FillPool(int fillCount)
        {
            for (int i = 0; i < fillCount; i++)
            {
                countObject++;
                T obj = Object.Instantiate(prefab);
                obj.name = prefab.name +  countObject;
                
                ReturnObject(obj);
            }
        }

        public T GetObject()
        {
            if (objectPool.Count <= 0)
            {
                FillPool(RefillCount);
            }

            var obj = objectPool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }
}