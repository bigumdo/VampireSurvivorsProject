using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Combat
{
    public abstract class BaseCaster :MonoBehaviour
    {
        protected Agent _agent;
        public CastTypeEnum castType;//ĳ��Ʈ Ÿ��
        public LayerMask targetLayer;//� ���� ĳ��Ʈ �� ���ΰ�
        public int castCnt;//�󸶳� ĳ��Ʈ�� ���ΰ�
        public Vector2 castOffset;
        public float castRange;
        public abstract bool Cast(Collider2D[] colliders); //��ӹ޾Ƽ� ���������ϵ��� ����
        public virtual void Initialize(Agent agent)
        {
            _agent = agent;
        }
    }
}
