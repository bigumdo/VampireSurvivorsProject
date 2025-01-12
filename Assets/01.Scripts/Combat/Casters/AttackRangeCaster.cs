using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Combat
{
    public class AttackRangeCaster : BaseCaster
    {
        public override bool Cast(Collider2D[] colliders)
        {
            return true;
        }
    }
}
