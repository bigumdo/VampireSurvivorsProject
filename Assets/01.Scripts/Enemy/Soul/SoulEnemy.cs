using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Enemys
{
    public class SoulEnemy : BaseEnemy
    {
        private AgentHealth _healthCompo;
        private AgentMover _mover;

        protected override void Awake()
        {
            base.Awake();
            _healthCompo = GetCompo<AgentHealth>();
            _mover = GetCompo<AgentMover>();
        }

        private void Start()
        {
            _stateMachine.Initialize(FSM.FSMState.Idle);
        }
    }
}
