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
    public class AgentMover : MonoBehaviour
    {
        [Header("Move stats")]
        [SerializeField] private StatSO _moveStat;
        [SerializeField] private float _moveSpeed = 5f;

        [SerializeField] private Transform _groundChecker, _wallChecker;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _groundCheckWidth, _checkDistance, _wallCheckWidth;


        [Header("AnimParams")]
        [SerializeField] private AnimParamSO _moveParam, _ySpeedParam;

        [field: SerializeField] public bool IsGrounded { get; private set; }
        public event Action<bool> OnGroundStateChanged;

        public Vector2 Velocity => _rbCompo.velocity;
        public bool CanManualMove { get; set; } = true; //키보드로 움직임 가능
        public float SpeedMultiplier { get; set; } = 1f;
        public float LimitYSpeed { get; set; } = 40f;
        private float _originalGravity;

        private Rigidbody2D _rbCompo;
        private Agent _entity;
        private AgentRenderer _renderer;
        private AgentStat _stat;
        private float _xMovement;

        private Collider2D _collider;

        public void Initialize(Agent agent)
        {
            _entity = agent;
            _rbCompo = agent.GetComponent<Rigidbody2D>();
            _renderer = agent.GetCompo<AgentRenderer>();
            _stat = agent.GetCompo<AgentStat>();

            _originalGravity = _rbCompo.gravityScale;

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

        public void StopImmediately(bool isYAxisToo = false)
        {
            if (isYAxisToo)
                _rbCompo.velocity = Vector2.zero;
            else
                _rbCompo.velocity = new Vector2(0,_rbCompo.velocity.y);
            _xMovement = 0;
        }

        public void SetMovement(float xMovement) => _xMovement = xMovement;

        private void FixedUpdate()
        {
            CheckGround();
            MoveCharacter();

            _rbCompo.velocity = new Vector2(_rbCompo.velocity.x,Mathf.Clamp(_rbCompo.velocity.y, -LimitYSpeed, LimitYSpeed));
        }

        public void CheckGround()
        {
            Physics2D.SyncTransforms(); //트랜스폼 모양과 현재 물리모양 일치

            bool before = IsGrounded;
            Vector2 boxSize = new Vector2(_groundCheckWidth, 0.05f);
            IsGrounded = Physics2D.BoxCast(_groundChecker.position, boxSize, 0f,
                Vector2.down, _checkDistance, _whatIsGround);

            if (IsGrounded != before)
            {
                OnGroundStateChanged?.Invoke(IsGrounded);
            }
        }

        private void MoveCharacter()
        {
            if (CanManualMove)
                _rbCompo.velocity = new Vector2(_xMovement * _moveSpeed * SpeedMultiplier,_rbCompo.velocity.y);

            _renderer.FlipController(_xMovement);

            _renderer.SetParam(_moveParam, Mathf.Abs(_xMovement) > 0);
            _renderer.SetParam(_ySpeedParam, Velocity.y);
        }

        public void SetGravityMultiplier(float value)
            => _rbCompo.gravityScale = _originalGravity * value;

        public bool IsWallDetected()
            => Physics2D.Raycast(_wallChecker.position,
                Vector2.right * _renderer.FacingDirection,
                _wallCheckWidth, _whatIsGround);

        public bool CheckColliderInFront(Vector2 direction, ref float distance)
        {
            Vector2 center = _collider.bounds.center;
            Vector2 size = _collider.bounds.size;
            size.y -= 0.3f; //빼는 이유는 안빼면 바닥하고 충돌해.

            var hit = Physics2D.BoxCast(center, size, 0f, direction, distance, _whatIsGround);
            if (hit)
                distance = hit.distance;

            return hit;
        }

        public void KnockBack(Vector2 force, float time)
        {
            CanManualMove = false;
            StopImmediately(true);
            AddForceToEntity(force);
            DOVirtual.DelayedCall(time, () => CanManualMove = true);
        }

        private void OnDrawGizmos()
        {
            if (_groundChecker == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_groundChecker.position - new Vector3(0, _checkDistance * 0.5f),
                new Vector3(_groundCheckWidth, _checkDistance, 1f));

            if (_wallChecker == null) return;

            Gizmos.DrawLine(_wallChecker.position, _wallChecker.position + new Vector3(_wallCheckWidth, 0, 0));

        }
    }
}
