using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.ObjectPooling
{
    [CreateAssetMenu(menuName = "SO/Pool/PoolItem")]
    public class PoolingItemSO : ScriptableObject
    {
        public string enumName;
        public string poolingName;
        public string description;
        public int poolCount;
        public MonoBehaviour prefabObj;
        public IPoolable prefab;

        private void OnValidate()
        {
            if (prefabObj != null)
            {
                if (prefabObj is IPoolable poolable)
                {
                    if (enumName == poolable.Type.ToString())
                    {
                        prefab = poolable;
                    }
                    else
                    {
                        prefabObj = null;
                        Debug.LogWarning("Type mismatch!");
                    }
                }
                else
                {
                    prefabObj = null;
                }
            }
        }
    }
}
