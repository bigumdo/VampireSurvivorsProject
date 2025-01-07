using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BGD.Combat
{
    public enum CastTypeEnum // CastŸ��
    {
        Damge
    }

    public class Caster : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private Vector2 _castOffset; // ���� ������ġ�� �����ϱ� ����offset
        private Dictionary<CastTypeEnum, BaseCaster> _casters; //Cast�� ����Dictionary
        private Collider2D[] castTargets; // ������ ������Ʈ�� Collider�� ��� ����
        [SerializeField] private BaseCaster _currentCast;//���� �������� Caster�� ��� ���� ����

        public void Initialize(Agent agent)
        {
            _casters = new Dictionary<CastTypeEnum, BaseCaster>();
            BaseCaster[] casts = GetComponents<BaseCaster>();//������ ĳ��Ʈ ������ ������
            foreach (BaseCaster c in casts)
            {
                _casters.Add(c.castType, c); // Enum���� cast�����ϱ� ���� �߰�
            }
        }

        public void Cast(CastTypeEnum castType)//���ϴ� ĳ��Ʈ Ÿ�԰� �󸶳� üũ������ �޴´�.
        {
            _currentCast = _casters.GetValueOrDefault(castType);//Ÿ�Կ� �´� Cast�� ���� �´�.
            Debug.Assert(_currentCast != null, $"{castType}cast���� ���ư�"); // CurrentCast�� Null�ƴ϶�� ����

            castTargets = Physics2D.OverlapCircleAll((Vector2)transform.position + _castOffset, _currentCast.castRange
                , _currentCast.targetLayer, 0, _currentCast.castCnt);//cat������ �°� OverapCircleAllüũ

            if (castTargets.Length > 0)
            {
                _currentCast.Cast(castTargets);//üũ�� ��ü�� �ִٸ� ���� cast���� collider[]������ �ѱ��.
                //Debug.Log("����");
            }
            //else
                //Debug.Log("�ֺ��� ������ ��ü�� �����ϴ�.");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if(_currentCast != null ) 
                Gizmos.DrawWireSphere((Vector2)transform.position + _castOffset, _currentCast.castRange);
        }
    }
}
