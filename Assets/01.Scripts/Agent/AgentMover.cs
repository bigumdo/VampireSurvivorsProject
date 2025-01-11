using BGD.Animators;
using BGD.Core.Manager;
using BGD.FSM;
using BGD.StatSystem;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentMover : MonoBehaviour,IAgentComponent,IAfterInitable
    {
        [Header("Move stats")]
        [SerializeField] protected StatSO _moveStat;
        protected float _moveSpeed = 5f;

        public Vector2 Velocity => _rbCompo.velocity;
        public bool CanManualMove { get; set; } = true; //Ű����� ������ ����
        public float SpeedMultiplier { get; set; } = 1f;

        protected Rigidbody2D _rbCompo;
        protected Agent _entity;
        protected AgentRenderer _renderer;
        protected AgentStat _stat;
        protected Vector2 _movement;

        protected Collider2D _collider;

        public virtual void Initialize(Agent agent)
        {
            _entity = agent;
            _rbCompo = agent.GetComponent<Rigidbody2D>();
            _renderer = agent.GetCompo<AgentRenderer>();
            _stat = agent.GetCompo<AgentStat>();

            _collider = agent.GetComponent<Collider2D>();
        }

        public virtual void AfterInit()
        {
            _stat.MoveSpeedStat.OnValueChange += HandleMoveSpeedChange;
            _moveSpeed = _stat.MoveSpeedStat.Value;
        }

        protected virtual void OnDestroy()
        {
            _stat.MoveSpeedStat.OnValueChange -= HandleMoveSpeedChange;
        }

        protected virtual void HandleMoveSpeedChange(StatSO stat, float current, float previous)
        {
            _moveSpeed = stat.Value;
        }

        public void AddForceToEntity(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
        {
            _rbCompo.AddForce(force, mode);
        }

        public void StopImmediately()
        {
            
            _rbCompo.velocity = Vector2.zero;
        }

        public void SetMovement(Vector2 movement) => _movement = movement;

        private void FixedUpdate()
        {
            MoveCharacter();
        }

        protected virtual void MoveCharacter()
        {
            if (CanManualMove)
            {
                _rbCompo.velocity = _movement * _moveSpeed * SpeedMultiplier;
            }

            _renderer.FlipController(_movement.x);
        }

        public virtual void KnockBack(Vector2 force, float time)
        {
            CanManualMove = false;
            StopImmediately();
            AddForceToEntity(force);
            DOVirtual.DelayedCall(time, () => CanManualMove = true);
        }
    }
}
