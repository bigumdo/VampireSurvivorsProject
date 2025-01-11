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
        protected AnimParamSO _animParam;
        protected bool _isTriggerCall;

        protected AgentRenderer _renderer;
        protected AgentAnimationTrigger _animTrigger;

        public AgentState(Agent agent,AnimParamSO animParam)
        {
            _agent = agent;
            _animParam = animParam;
            _renderer = agent.GetCompo<AgentRenderer>();
            _animTrigger = agent.GetCompo<AgentAnimationTrigger>(true);
        }

        public virtual void Initialize(Agent agent)
        {
            _agent = agent;
        }

        public virtual void Enter()
        {
            _renderer.SetParam(_animParam, true);
            _isTriggerCall = false;
            _animTrigger.OnAnimationEndTrigger += AnimationEndTrigger;
        }

        public virtual void Update()
        {

        }

        public virtual void Exit()
        {
            _renderer.SetParam(_animParam, false);
            _animTrigger.OnAnimationEndTrigger -= AnimationEndTrigger;

        }

        public virtual void AnimationEndTrigger()
        {
            _isTriggerCall = true;
        }
    }
}
