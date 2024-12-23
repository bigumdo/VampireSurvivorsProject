using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Combat
{
    public abstract class BaseCaster :MonoBehaviour
    {
        public CastTypeEnum castType;
        public LayerMask targetLayer;
        public int castCnt;
        public virtual void Cast(Collider2D[] colliders)
        {

        }
    }
}
