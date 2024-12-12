using BGD.Animators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.FSM
{
    [CreateAssetMenu(menuName = "SO/FSM/StateSO")]
    public class StateSO : ScriptableObject
    {
        public FSMState stateName;
        public string className;
        public AnimParamSO animParam;

    }
}

