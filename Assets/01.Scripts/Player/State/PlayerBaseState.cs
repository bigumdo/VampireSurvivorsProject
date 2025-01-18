using BGD.Agents;
using BGD.Animators;
using BGD.Combat;
using BGD.FSM;
using BGD.StatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerBaseState : AgentState
    {
        protected Player _player;
        protected PlayerMover _mover;
        private float _checkTime = 0;
        private float _atkCoolTime;
        public PlayerBaseState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
            _mover = agent.GetCompo<PlayerMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _checkTime = Time.time;
            _player.GetCompo<AgentStat>().GetStat(_player.atkCoolTimeStat).OnValueChange += HandleAtkCoolTimeChange;
            _atkCoolTime = _player.GetCompo<AgentStat>().GetStat(_player.atkCoolTimeStat).Value;
        }

        public override void Exit()
        {
            _player.GetCompo<AgentStat>().GetStat(_player.atkCoolTimeStat).OnValueChange -= HandleAtkCoolTimeChange;
            base.Exit();
        }

        private void HandleAtkCoolTimeChange(StatSO stat, float current, float previous)
        {
            _atkCoolTime = current;
        }

        public override void Update()
        {
            base.Update();
            if (_atkCoolTime + _checkTime < Time.time)
            {
                Debug.Log("AttackPlaye");
                _checkTime = Time.time;
                _player.GetCompo<Caster>().Cast(CastTypeEnum.Damge);
            }
        }


    }
}
