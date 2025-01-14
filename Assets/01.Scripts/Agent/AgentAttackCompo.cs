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
        private Caster _caster;
        private Agent _agent;
        private EnemyAnimationTrigger _animTrigger;
        private AgentStat _statCompo;

        public void Initialize(Agent agent)
        {
            _agent = agent;
            _caster = agent.GetCompo<Caster>();
            _statCompo = agent.GetCompo<AgentStat>();
            _animTrigger = agent.GetCompo<EnemyAnimationTrigger>(true);
        }
        public void AfterInit()
        {
            _damageStat = _statCompo.GetStat(_damageStat);
            _damageStat.OnValueChange += HandleAttackDamageChange;
            _animTrigger.OnAttackTrigger += HandleAttackTrigger;
        }

        private void OnDestroy()
        {
            _damageStat.OnValueChange -= HandleAttackDamageChange;
            _animTrigger.OnAttackTrigger -= HandleAttackTrigger;
        }

        private void HandleAttackTrigger()
        {
            _caster.Cast(CastTypeEnum.Damge);
        }

        private void HandleAttackDamageChange(StatSO stat, float current, float previous)
        {
            
        }
    }
}
