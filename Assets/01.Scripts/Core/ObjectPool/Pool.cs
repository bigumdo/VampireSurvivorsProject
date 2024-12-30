using BGD.ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BGD.ObjectPooling
{
    public class Pool
    {
        private Stack<IPoolable> _pool = new Stack<IPoolable>();
        private MonoBehaviour _prefab;
        private Transform _parent;

        private PoolingType _type;

        public Pool(MonoBehaviour prefab, PoolingType type, Transform parent, int count)
        {
            _prefab = prefab;
            _type = type;
            _parent = parent;
            for (int i = 0; i < count; i++)
            {
                MonoBehaviour obj = GameObject.Instantiate(_prefab, _parent);
                obj.gameObject.name = _type.ToString();
                obj.gameObject.SetActive(false);
                IPoolable poolable = obj as IPoolable;
                poolable.Type = _type;
                _pool.Push(poolable);
            }
        }

        public IPoolable Pop()
        {
            IPoolable poolable;

            if (_pool.Count <= 0)
            {
                MonoBehaviour newObj = GameObject.Instantiate(_prefab, _parent);
                newObj.gameObject.name = _type.ToString();
                poolable = newObj as IPoolable;
                poolable.Type = _type;
            }
            else
            {
                poolable = _pool.Pop();
            }

            (poolable as MonoBehaviour)?.gameObject.SetActive(true);
            return poolable;
        }


        public void Push(IPoolable obj)
        {
            IPoolable poolable = obj as IPoolable;
            _pool.Push(poolable);
        }
    }
}
