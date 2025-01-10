using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Combat
{
    public class PlayerDamageCaster : BaseCaster
    {
        [SerializeField] protected int _damage;

        public override bool Cast(Collider2D[] colliders)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out AgentHealth health))
                {
                    //나중에 범위로 데미지 들어가게 할꺼임
                    health.ApplyDamage(_damage);
                }
            }
            return false;
        }
    }
}
