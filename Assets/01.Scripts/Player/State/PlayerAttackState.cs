using System.Collections;
using System.Collections.Generic;
using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerAttackState : AgentState
    {
        private Player _player;
        private AgentMover _mover;

        private float _lastAttackTime;
        private float _attackDelay;
        public PlayerAttackState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
            _mover = agent.GetCompo<AgentMover>();
        }

        public override void Enter()
        {
            if (_lastAttackTime + _attackDelay < Time.time)
            {
                base.Enter();
                
            }
        }

        public override void Exit()
        {
            _lastAttackTime = Time.time;
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if(_isTriggerCall)
                _player.ChangeState(FSMState.Idle);
            Vector2 input = _player.PlayerInputSO.InputDirection;
            _mover.SetMovement(input);
        }
    }
}
