using BGD.Animators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.FSM
{
    [CreateAssetMenu(menuName = "SO/FSM/AgnetStateSO")]
    public class AgentStateSO : ScriptableObject
    {
        public string StateName;
        public string className;
        public AnimParamSO animParam;

    }
}

