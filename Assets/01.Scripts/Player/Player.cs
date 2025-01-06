using BGD.Agents;
using BGD.FSM;
using BGD.StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Players
{
    public class Player : Agent
    {
        public AgentStateListSO playerFSM;
        [field:SerializeField] public PlayerInputSO PlayerInputSO { get; set; }
        public StatSO atkCoolTimeStat;
        private StateMachine _stateMachine;


        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new StateMachine(playerFSM,this);
        }

        private void Start()
        {
            _stateMachine.Initialize(FSMState.Idle);
        }

        private void Update()
        {
            _stateMachine.UpdateState();
        }

        public void ChangeState(FSMState state) => _stateMachine.ChangeState(state);
    }
}
