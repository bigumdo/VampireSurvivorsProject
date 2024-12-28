using System.Collections.Generic;
using UnityEngine;

namespace BGD.ObjectPooling
{
    public class PoolingTableSO : ScriptableObject
    {
        public List<PoolingItemSO> datas = new List<PoolingItemSO>();
    }
}
