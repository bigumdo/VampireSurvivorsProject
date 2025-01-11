using BGD.Animators;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentRenderer : MonoBehaviour, IAgentComponent
    {
        public float FacingDirection { get; private set; } = 1;

        private Agent _agent;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        public void Initialize(Agent agnet)
        {
            _agent = agnet;
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetParam(AnimParamSO param, bool value) => _animator.SetBool(param.hashValue, value);
        public void SetParam(AnimParamSO param, float value) => _animator.SetFloat(param.hashValue, value);
        public void SetParam(AnimParamSO param, int value) => _animator.SetInteger(param.hashValue,value);
        public void SetParam(AnimParamSO param) => _animator.SetTrigger(param.hashValue);

        public void FadeSprite(float time, Action CompleteCallback = null)
        {
            _spriteRenderer.DOFade(0, time).OnComplete(() => CompleteCallback?.Invoke());
        }

        #region Flip

        public void FlipController(float xMovement)
        {
            if (Mathf.Abs(FacingDirection + xMovement) < 0.5f)
                Flip();
        }

        public void Flip()
        {
            FacingDirection *= -1;
            _agent.transform.Rotate(0, 180, 0);
            //_spriteRenderer.flipX = FacingDirection <0 ? true: false;

        }

        #endregion


    }
}
