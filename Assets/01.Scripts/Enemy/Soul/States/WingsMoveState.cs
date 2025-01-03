using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Enemys
{
    public class WingsMoveState : AgentState
    {
        private WingsEnemy _wingsEnemy;
        public WingsMoveState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _wingsEnemy = agent as WingsEnemy;
        }

    }
}
