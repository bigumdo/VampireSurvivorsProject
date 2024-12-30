using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.ObjectPooling
{
    public interface IPoolable
    {
        public PoolingType Type { get; set; }
        public void ResetItem();
    }
}
