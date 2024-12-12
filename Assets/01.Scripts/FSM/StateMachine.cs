using BGD.Agents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace BGD.FSM
{
    public class StateMachine 
    {
        public AgentState CurrentState { get; private set; }
        private Dictionary<FSMState, AgentState> _states;

        public StateMachine(AgentStateListSO fsmStates,Agent agent)
        {
            _states = new Dictionary<FSMState, AgentState>();

            foreach (StateSO state in fsmStates.states)
            {
                try
                {
                    Type t = Type.GetType(state.className);
                    var agentState = Activator.CreateInstance(t, agent, state.animParam) as AgentState;
                    _states.Add(state.stateName, agentState);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{state.className} loading Error, Message : {ex.Message}");
                }
            }
        }

        public void Initialize(FSMState startState)
        {
            CurrentState = GetState(startState);
            CurrentState.Enter();
        }

        public void ChangeState(FSMState changeState)
        {
            CurrentState.Exit();
            CurrentState = GetState(changeState);
            Debug.Assert(CurrentState != null,$"{changeState}State없어 돌아가");
            CurrentState.Enter();
        }

        public void UpdateState()
        {
            CurrentState.Update();
        }

        public AgentState GetState(FSMState state) => _states.GetValueOrDefault(state);

    }
}
