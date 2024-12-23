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

        public override void Cast(Collider2D[] colliders)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out AgentHealth health))
                {
                    health.ApplyDamage(_damage);
                }
            }
        }
    }
}
