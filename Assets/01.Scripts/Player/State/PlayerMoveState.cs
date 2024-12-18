using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerMoveState : PlayerGroundState
    {
        public PlayerMoveState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
        }

        public override void Update()
        {
            base.Update();
            Vector2 input = _player.PlayerInputSO.InputDirection;
            _mover.SetMovement(input);
            if (input == Vector2.zero) 
                _player.ChangeState(FSMState.Idle);
        }
    }
}
