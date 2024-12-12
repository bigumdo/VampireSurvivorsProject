using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerIdleState : AgentState
    {
        private Player _player;
        public PlayerIdleState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
        }


        public override void Update()
        {
            base.Update();
            Vector2 input = _player.PlayerInputSO.InputDirection;
            if (input != Vector2.zero )
                _player.ChangeState(FSMState.Move);
        }
    }
}
