using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Combat
{
    public abstract class BaseCaster :MonoBehaviour
    {
        public CastTypeEnum castType;//ĳ��Ʈ Ÿ��
        public LayerMask targetLayer;//� ���� ĳ��Ʈ �� ���ΰ�
        public int castCnt;//�󸶳� ĳ��Ʈ�� ���ΰ�
        public abstract void Cast(Collider2D[] colliders); //��ӹ޾Ƽ� ���������ϵ��� ����
    }
}
