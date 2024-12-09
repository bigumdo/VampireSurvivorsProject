using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentRenderer : MonoBehaviour, IAgentComponent
    {
        private Agent _agent;
        public float FacingDirection { get; private set; } = 1;

        public void Initialize(Agent agnet)
        {
            _agent = agnet;
        }

        #region Flip

        private void FlipControl()
        {
            
        }

        private void Flip()
        {

        }

        #endregion


    }
}
