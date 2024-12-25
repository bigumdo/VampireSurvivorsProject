using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace BGD.Combat
{
    public class DamageCaster :  BaseCaster
    {
        [SerializeField] protected int _damage;
        [SerializeField] protected float _attackCoolTime;
        private float _listTime;

        public override void Cast(Collider2D[] colliders)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out AgentHealth health))
                {
                    if(_listTime + _attackCoolTime > Time.time || _listTime == 0)
                    {
                        health.ApplyDamage(_damage);
                        _listTime = Time.time;
                    }
                }
            }
        }
    }
}
