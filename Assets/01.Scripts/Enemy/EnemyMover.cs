using BGD.Agents;
using BGD.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Enemys
{
    public class EnemyMover : AgentMover
    {
        private BaseEnemy _enemy;

        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            _enemy = agent as BaseEnemy;
        }

        protected override void MoveCharacter()
        {
            int dir = _enemy.playerManager.Player.transform.position.x - transform.position.x > 0.5f ? 1 : -1;
            _enemy.GetCompo<AgentRenderer>().FlipController(dir);
            if ((_enemy.playerManager.Player.transform.position - transform.position).sqrMagnitude > 0.5f)
            {
                Vector2 dirs = (_enemy.playerManager.Player.transform.position - transform.position).normalized;
                _rbCompo.velocity = dirs * _moveSpeed;
            }
            else
                StopImmediately();
            _enemy.GetCompo<Caster>().Cast(CastTypeEnum.Damge);
        }

    }
}
