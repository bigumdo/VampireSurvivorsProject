using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentAnimationTrigger : MonoBehaviour, IAgentComponent
    {
        private Agent _agent;


        public void Initialize(Agent agnet)
        {
            _agent = agnet;
        }
    }
}
