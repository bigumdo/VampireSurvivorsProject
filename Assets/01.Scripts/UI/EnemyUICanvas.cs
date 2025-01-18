using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.UI
{
    public class EnemyUICanvas : MonoBehaviour, IAgentComponent
    {
        private Agent _agent;
        private RectTransform _rectTrm;
        private AgentRenderer _renderer;

        public void Initialize(Agent agent)
        {
            _agent = agent;
            _renderer = agent.GetCompo<AgentRenderer>();
        }

        private void Awake()
        {
            //_rectTrm = transform as RectTransform;
        }

        void LateUpdate()
        {
            Debug.Log(transform.parent.transform.rotation);
            //Vector3 rotation = transform.parent.rotation
            //transform.rotation = Quaternion.Euler();
        }
    }
}
