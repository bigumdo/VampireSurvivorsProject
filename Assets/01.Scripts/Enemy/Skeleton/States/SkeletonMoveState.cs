using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Enemys
{
    public class SkeletonMoveState : AgentState
    {

        public SkeletonMoveState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
        }

        public override void Update()
        {
            base.Update();

        }
    }
}
