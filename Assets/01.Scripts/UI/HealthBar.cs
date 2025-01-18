
using BGD.Agents;
using BGD.StatSystem;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BGD.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Agent _owner;
        [SerializeField] private Image _fillImage;
        private StatSO _hpStat;
        private CanvasGroup _canvasGroup;
        private AgentHealth _health;


        private void Start()
        {
            _hpStat = _owner.GetCompo<AgentStat>().HpStat;
            _health = _owner.GetCompo<AgentHealth>();
            _canvasGroup = GetComponent<CanvasGroup>();

            _owner.OnDeadEvent.AddListener(HadleDeadEvent);
            _owner.OnHitEvent.AddListener(HadleHitEvent);
        }

        private void OnDestroy()
        {
            _owner.OnHitEvent.RemoveListener(HadleHitEvent);
            _owner.OnDeadEvent.RemoveListener(HadleDeadEvent);
        }

        private void HadleDeadEvent()
        {
            _canvasGroup.DOFade(0, 1);
        }

        private void HadleHitEvent()
        {
            _fillImage.fillAmount = _health.CurrentHealth / _health.MaxHealth;
        }
    }
}
