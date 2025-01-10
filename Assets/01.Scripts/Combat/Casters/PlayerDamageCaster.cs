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
                    //���߿� ������ ������ ���� �Ҳ���
                    health.ApplyDamage(_damage);
                }
            }
            return false;
        }
    }
}
