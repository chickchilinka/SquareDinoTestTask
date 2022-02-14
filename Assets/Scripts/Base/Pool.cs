using UnityEngine;
using System.Collections.Generic;

namespace Base
{
    public abstract class Pool<T>:MonoBehaviour where T:IPoolable
    {
        private List<T> pooled;

        protected void Awake() {
            pooled = new List<T>();
        }

        public T CreateOrGetObject() {
            foreach(T p in pooled) {
                if (!p.PoolableActive()) {
                    p.PoolableReset();
                    return p;
                }
            }
            pooled.Add(Create());
            return pooled[pooled.Count-1];
        }

        public abstract T Create();
    }
}
