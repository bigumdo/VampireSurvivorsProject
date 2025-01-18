using BGD.Agents;
using BGD.Combat;
using BGD.StatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD
{
    public class AgentHealth : MonoBehaviour, IAgentComponent, IDamageable, IAfterInitable
    {
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        private Agent _agent;
        private AgentStat _stat;

        public void Initialize(Agent agent)
        {
            _agent = agent;
            _stat = _agent.GetCompo<AgentStat>();
        }
        public void AfterInit()
        {
            CurrentHealth = MaxHealth = _stat.HpStat.Value;
            _stat.HpStat.OnValueChange += HandleValueChange;
        }

        private void OnDestroy()
        {
            _stat.HpStat.OnValueChange -= HandleValueChange;
        }

        private void HandleValueChange(StatSO stat, float current, float previous)
        {
            MaxHealth = current;
            CurrentHealth = MaxHealth / CurrentHealth * current;
        }


        public void ApplyDamage(float damage)
        {
            CurrentHealth -= damage;
            _agent.OnHitEvent?.Invoke();
            if(CurrentHealth <= 0)
                _agent.OnDeadEvent?.Invoke();
        }


    }
}
