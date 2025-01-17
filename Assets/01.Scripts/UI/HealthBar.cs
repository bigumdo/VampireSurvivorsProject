
using BGD.Agents;
using BGD.StatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BGD
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Agent _owner;
        [SerializeField] private Image _fillImage;
        private StatSO _hpStat;
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _owner.OnHitEvent.AddListener(HadleHitEvent);
            _owner.OnDeadEvent.AddListener(HadleDeadEvent);
            _hpStat = _owner.GetCompo<AgentStat>().HpStat;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnDestroy()
        {
            _owner.OnHitEvent.RemoveListener(HadleHitEvent);
            _owner.OnDeadEvent.RemoveListener(HadleDeadEvent);
        }

        private void HadleDeadEvent()
        {

        }

        private void HadleHitEvent()
        {
            _fillImage.fillAmount = _hpStat.MaxValue / _hpStat.Value;
        }
    }
}
