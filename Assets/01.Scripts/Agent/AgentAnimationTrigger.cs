using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentAnimationTrigger : MonoBehaviour, IAgentComponent
    {
        public event Action OnAnimationEndTrigger;
        public event Action OnAttackTrigger;

        protected Agent _agent;

        public void Initialize(Agent agnet)
        {
            _agent = agnet;
        }

        protected virtual void AnimationEnd() => OnAnimationEndTrigger?.Invoke();
        protected virtual void AttackTrigger() => OnAttackTrigger?.Invoke();
    }
}
