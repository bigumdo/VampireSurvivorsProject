
using System.Collections;
using System.Collections.Generic;
using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerGroundState : AgentState
    {
        protected Player _player;
        protected PlayerMover _mover;
        public PlayerGroundState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
            _mover = agent.GetCompo<PlayerMover>();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
