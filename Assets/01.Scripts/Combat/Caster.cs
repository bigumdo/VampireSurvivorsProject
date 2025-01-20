using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BGD.Combat
{
    public enum CastTypeEnum // CastŸ��
    {
        Damge,
        AttackRnage,
        PlayerAttack
    }

    public class Caster : MonoBehaviour, IAgentComponent
    {
        private Agent _agent;
        private Dictionary<CastTypeEnum, BaseCaster> _casters; //Cast�� ����Dictionary
        private Collider2D[] castTargets; // ������ ������Ʈ�� Collider�� ��� ����
        private AgentRenderer _agentRenderer;
        private Vector2 _agentDir;
        [SerializeField] private BaseCaster _currentCast;//���� �������� Caster�� ��� ���� ����

        public void Initialize(Agent agent)
        {
            _agent = agent;
            _agentRenderer = agent.GetCompo<AgentRenderer>();
            _casters = new Dictionary<CastTypeEnum, BaseCaster>();
            BaseCaster[] casts = GetComponents<BaseCaster>();//������ ĳ��Ʈ ������ ������
            foreach (BaseCaster c in casts)
            {
                c.Initialize(agent);
                _casters.Add(c.castType, c); // Enum���� cast�����ϱ� ���� �߰�
            }
        }

        public bool Cast(CastTypeEnum castType)//���ϴ� ĳ��Ʈ Ÿ�԰� �󸶳� üũ������ �޴´�.
        {
            _currentCast = _casters.GetValueOrDefault(castType);//Ÿ�Կ� �´� Cast�� ���� �´�.
            Debug.Assert(_currentCast != null, $"{castType}cast���� ���ư�"); // CurrentCast�� Null�ƴ϶�� ����

            _agentDir = new Vector2(_currentCast.castOffset.x * _agentRenderer.FacingDirection, _currentCast.castOffset.y);
            castTargets = Physics2D.OverlapCircleAll((Vector2)transform.position + _agentDir, _currentCast.castRange
                , _currentCast.targetLayer, 0, _currentCast.castCnt);//cat������ �°� OverapCircleAllüũ
            if (castTargets.Length > 0)
            {
                return _currentCast.Cast(castTargets);//üũ�� ��ü�� �ִٸ� ���� cast���� collider[]������ �ѱ��.
                //Debug.Log("����");
            }
            return false;
            //else
                //Debug.Log("�ֺ��� ������ ��ü�� �����ϴ�.");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if(_currentCast != null ) 
                Gizmos.DrawWireSphere((Vector2)transform.position + _agentDir, _currentCast.castRange);
        }
    }
}
