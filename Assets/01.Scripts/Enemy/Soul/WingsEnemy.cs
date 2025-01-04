using BGD.Agents;
using BGD.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Enemys
{
    public class WingsEnemy : BaseEnemy, IPoolable
    {
        private AgentHealth _healthCompo;
        private AgentMover _mover;
        

        [field:SerializeField]public PoolingType Type { get; set; }

        public void ResetItem()
        {
        }

        protected override void Awake()
        {
            base.Awake();
            _healthCompo = GetCompo<AgentHealth>();
            _mover = GetCompo<AgentMover>();
        }


        private void Start()
        {
            _stateMachine.Initialize(FSM.FSMState.Move);
        }
    }
}
