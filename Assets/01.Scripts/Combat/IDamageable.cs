using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Combat
{
    public interface IDamageable
    {
        public void ApplyDamage(float damage);
    }
}
