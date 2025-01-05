using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerBaseState : AgentState
    {
        public PlayerBaseState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
        }

    }
}
