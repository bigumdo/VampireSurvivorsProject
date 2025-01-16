using BGD.Combat;
using BGD.Enemys;
using BGD.StatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentAttackCompo : MonoBehaviour, IAgentComponent, IAfterInitable
    {
        [SerializeField] private StatSO _damageStat;
        [SerializeField] private List<AttackDataSO> _attackDatas;
        private Caster _caster;
        private Agent _agent;
        private EnemyAnimationTrigger _animTrigger;
        private AgentStat _statCompo;
        private AttackDataSO _currentAttackData;
        private Dictionary<string, AttackDataSO> _atkDictionary;

        public void Initialize(Agent agent)
        {
            _agent = agent;
            _caster = agent.GetCompo<Caster>();
            _statCompo = agent.GetCompo<AgentStat>();
            _animTrigger = agent.GetCompo<EnemyAnimationTrigger>(true);

            _atkDictionary = new Dictionary<string, AttackDataSO>();
            _attackDatas.ForEach(data => _atkDictionary.Add(data.attackName, data));
        }
        public void AfterInit()
        {
            _damageStat = _statCompo.GetStat(_damageStat);
            _animTrigger.OnAttackTrigger += HandleAttackTrigger;
        }

        private void OnDestroy()
        {
            _animTrigger.OnAttackTrigger -= HandleAttackTrigger;
        }

        private void HandleAttackTrigger()
        {
            _caster.Cast(CastTypeEnum.Damge);
        }

        public AttackDataSO GetAttackData(string KeyName)
        {
            AttackDataSO returnData = _atkDictionary.GetValueOrDefault(KeyName);
            Debug.Assert(returnData != null, $"{KeyName} attack data not found");
            return returnData;

        }

        public void SetAttackData(AttackDataSO atkData)
        {
            _currentAttackData = atkData;
        }
    }
}
