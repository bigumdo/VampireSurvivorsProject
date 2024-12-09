using BGD.Agents;
using BGD.Animators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.FSM
{
    public class AgentState 
    {
        protected Agent _agent;
        protected AnimParamSO _aninmParam;
        protected bool _isTriggerCall;

        protected AgentRenderer _renderer;
        protected AgentAnimationTrigger _animTrigger;

        public virtual void Initialize(Agent agent)
        {
            _agent = agent;
        }

        public virtual void Enter()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Exit()
        {

        }

        public virtual void AnimationEndTrigger()
        {

        }
    }
}
