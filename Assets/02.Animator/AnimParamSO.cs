using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Animators
{
    [CreateAssetMenu(menuName ="SO/Anim/ParamSO")]
    public class AnimParamSO : ScriptableObject
    {
        public string paramName;
        public int hashValue;

        private void OnValidate()
        {
            hashValue = Animator.StringToHash(paramName);
        }
    }
}
