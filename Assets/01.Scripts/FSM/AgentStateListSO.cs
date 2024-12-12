using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.FSM
{

    public enum FSMState
    {
        Idle,
        Dash,
        Move,
        Dead,
        Attack
    }


    [CreateAssetMenu(menuName = "SO/FSM/AgentStateList")]
    public class AgentStateListSO : ScriptableObject
    {

        public List<StateSO> states;
    }
}

