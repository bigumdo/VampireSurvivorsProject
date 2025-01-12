using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Enemys
{
    public class SkeletonAttackState : AgentState
    {
        private SkeletonEnemy _enemy;
        private EnemyMover _move;
        public SkeletonAttackState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _enemy = agent as SkeletonEnemy;
            _move = agent.GetCompo<EnemyMover>();
        }

        public override void Enter()
        {
            _move.CanMove = false;
            base.Enter();
        }

        public override void Exit()
        {
            _move.CanMove = true;
            base.Exit();
        }

        public override void AnimationEndTrigger()
        {
            base.AnimationEndTrigger();
            _enemy.ChangeState(FSMState.Move);
        }

    }
}
