using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Enemys
{
    public class SkeletonDeadState : AgentState
    {
        public SkeletonDeadState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        { 
        }

    }
}
