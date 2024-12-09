using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.FSM
{
    public class StateMachine 
    {
        public AgentStateSO CurrentState { get; private set; }
        private Dictionary<FSMState, AgentStateSO> _states;

        public StateMachine(AgentStateListSO fsmStates,Agent agent)
        {
            _states = new Dictionary<FSMState, AgentStateSO>();
        }

    }
}
