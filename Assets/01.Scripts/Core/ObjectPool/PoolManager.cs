using BGD.Core;
using BGD.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        private Dictionary<PoolingType, Pool> _pools
                        = new Dictionary<PoolingType, Pool>();

        public PoolingTableSO listSO;

        private void Awake()
        {
            foreach (PoolingItemSO item in listSO.datas)
            {
                CreatePool(item);
            }
        }

        private void CreatePool(PoolingItemSO item)
        {
            var pool = new Pool(item.prefabObj, item.prefab.Type, transform, item.poolCount);
            _pools.Add(item.prefab.Type, pool);
        }


        public IPoolable Pop(PoolingType type)
        {
            if (_pools.ContainsKey(type) == false)
            {
                Debug.LogError($"Prefab does not exist on pool : {type.ToString()}");
                return null;
            }

            IPoolable item = _pools[type].Pop();
            item.ResetItem();
            return item;
        }

        public void Push(IPoolable obj, bool resetParent = false)
        {
            if (resetParent)
            {
                (obj as MonoBehaviour)?.gameObject.SetActive(false);
            }
            _pools[obj.Type].Push(obj);
        }
    }
}
