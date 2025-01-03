using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Combat
{
    public class DamageCaster :  BaseCaster
    {
        [SerializeField] protected int _damage;
        private float _listTime;

        public override void Cast(Collider2D[] colliders)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out AgentHealth health))
                {
                    health.ApplyDamage(_damage);
                    _listTime = Time.time;
                }
            }
        }
    }
}
