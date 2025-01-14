using BGD.Agents;
using BGD.Animators;
using BGD.Combat;
using BGD.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Enemys
{
    public class SkeletonMoveState : AgentState
    {
        private SkeletonEnemy _enemy;
        public SkeletonMoveState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _enemy = agent as SkeletonEnemy;
        }

        public override void Update()
        {
            base.Update();
            if(_enemy.Cast(CastTypeEnum.AttackRnage))
            {
                _enemy.ChangeState(FSMState.Attack);
            }
        }
    }
}
