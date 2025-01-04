using BGD.Agents;
using BGD.Combat;
using BGD.Core.Manager;
using BGD.FSM;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace BGD.Enemys
{
    public class BaseEnemy : Agent
    {
        public PlayerManager playerManager;
        protected Caster _caster;
        protected AgentRenderer _renderer;
        protected StateMachine _stateMachine;

        public AgentStateListSO enemyFSM;

        protected override void Awake()
        {
            base.Awake();
            _renderer = GetCompo<AgentRenderer>();
            _stateMachine = new StateMachine(enemyFSM,this);
            _caster = GetCompo<Caster>();
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }

        public void ChangeState(FSMState newState)
        {
            _stateMachine.ChangeState(newState);
        }

        public void Cast(CastTypeEnum castType)
        {
            _caster.Cast(castType);
        }

        public AgentState GetState(FSMState state)
            => _stateMachine.GetState(state);

    }
}
