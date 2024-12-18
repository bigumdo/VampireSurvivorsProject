
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
        protected AgentMover _mover;
        public PlayerGroundState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
            _mover = agent.GetCompo<AgentMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInputSO.AttackEvent += HandleAttackEvent;
        }

        public override void Exit()
        {
            _player.PlayerInputSO.AttackEvent -= HandleAttackEvent;
            base.Exit();
        }

        private void HandleAttackEvent()
        {
            _player.ChangeState(FSMState.Attack);
        }
    }
}
