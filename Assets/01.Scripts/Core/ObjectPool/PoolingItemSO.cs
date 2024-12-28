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
        public IPoolable prefab;

        private void OnValidate()
        {
            if (prefab != null)
            {
                if (enumName != prefab.Type.ToString())
                {
                    prefab = null;
                    Debug.LogWarning("Type mismatch!");
                }
            }
        }
    }
}
