using BGD.Animators;
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
        [SerializeField] private StatSO _moveStat;
        [SerializeField] private float _moveSpeed = 5f;

        [SerializeField] private LayerMask _whatIsGround;

        [field: SerializeField] public bool IsGrounded { get; private set; }
        //public event Action<bool> OnGroundStateChanged;
        
        public Vector2 Velocity => _rbCompo.velocity;
        public bool CanManualMove { get; set; } = true; //Ű����� ������ ����
        public float SpeedMultiplier { get; set; } = 1f;

        private Rigidbody2D _rbCompo;
        private Agent _entity;
        private AgentRenderer _renderer;
        private AgentStat _stat;
        private Vector2 _movement;

        private Collider2D _collider;

        public void Initialize(Agent agent)
        {
            _entity = agent;
            _rbCompo = agent.GetComponent<Rigidbody2D>();
            _renderer = agent.GetCompo<AgentRenderer>();
            _stat = agent.GetCompo<AgentStat>();

            _collider = agent.GetComponent<Collider2D>();
        }

        public void AfterInit()
        {
            _stat.MoveSpeedStat.OnValueChange += HandleMoveSpeedChange;
            _moveSpeed = _stat.MoveSpeedStat.Value;
        }

        private void OnDestroy()
        {
            _stat.MoveSpeedStat.OnValueChange -= HandleMoveSpeedChange;
        }

        private void HandleMoveSpeedChange(StatSO stat, float current, float previous)
        {
            _moveSpeed = current;
        }

        public void AddForceToEntity(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
        {
            _rbCompo.AddForce(force, mode);
        }

        public void StopImmediately()
        {
            
            _rbCompo.velocity = Vector2.zero;
            _movement = Vector2.zero;
        }

        public void SetMovement(Vector2 movement) => _movement = movement;

        private void FixedUpdate()
        {
            MoveCharacter();
        }

        private void MoveCharacter()
        {
            if (CanManualMove)
                _rbCompo.velocity = _movement * _moveSpeed * SpeedMultiplier;

            _renderer.FlipController(_movement.x);
        }

        public void KnockBack(Vector2 force, float time)
        {
            CanManualMove = false;
            StopImmediately();
            AddForceToEntity(force);
            DOVirtual.DelayedCall(time, () => CanManualMove = true);
        }
    }
}
