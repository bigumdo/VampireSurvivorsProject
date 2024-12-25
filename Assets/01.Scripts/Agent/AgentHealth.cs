using BGD.Agents;
using BGD.Combat;
using BGD.StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD
{
    public class AgentHealth : MonoBehaviour, IAgentComponent, IDamageable, IAfterInitable
    {

        private float _currentHealth;
        private Agent _agent;
        private AgentStat _stat;
        public void Initialize(Agent agent)
        {
            _agent = agent;
            _stat = _agent.GetCompo<AgentStat>();
        }
        public void AfterInit()
        {
            _currentHealth = _stat.HpStat.Value;

        }

        public void ApplyDamage(float damage)
        {
            _currentHealth -= damage;
        }


    }
}
