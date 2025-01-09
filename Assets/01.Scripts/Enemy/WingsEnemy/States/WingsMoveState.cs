using BGD.Agents;
using BGD.Animators;
using BGD.Combat;
using BGD.FSM;
using BGD.StatSystem;
using UnityEngine;

namespace BGD.Enemys
{
    public class WingsMoveState : AgentState
    {
        private WingsEnemy _enemy;
        protected EnemyMover _mover;
        private float _checkTime = 0;
        private float _atkCoolTime;
        public WingsMoveState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _enemy = agent as WingsEnemy;
            _mover = agent.GetCompo<EnemyMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _checkTime = Time.time;
            _enemy.GetCompo<AgentStat>().GetStat(_enemy.atkCoolTimeStat).OnValueChange += HandleAtkCoolTimeChange;
            _atkCoolTime = _enemy.GetCompo<AgentStat>().GetStat(_enemy.atkCoolTimeStat).Value;
        }

        public override void Exit()
        {
            _enemy.GetCompo<AgentStat>().GetStat(_enemy.atkCoolTimeStat).OnValueChange -= HandleAtkCoolTimeChange;
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
                _checkTime = Time.time;
                _enemy.GetCompo<Caster>().Cast(CastTypeEnum.Damge);
            }
        }
    }
}
