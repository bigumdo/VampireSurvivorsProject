using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Combat
{
    public abstract class BaseCaster :MonoBehaviour
    {
        public CastTypeEnum castType;//캐스트 타입
        public LayerMask targetLayer;//어떤 것을 캐스트 할 것인가
        public int castCnt;//얼마나 캐스트할 것인가
        public abstract void Cast(Collider2D[] colliders); //상속받아서 구현가능하도록 설게
    }
}
