using BGD.Agents;
using BGD.StatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerAttack : MonoBehaviour, IAgentComponent, IAfterInitable
    {
        [SerializeField] private StatSO _atkCoolTimeStat;
        private Player _player;
        private float _atkCoolTime;
        private float _checkTime;

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
        }
        public void AfterInit()
        {
            _player.GetCompo<AgentStat>().GetStat(_atkCoolTimeStat).OnValueChange += HandleAtkCoolTimeChange;
            _atkCoolTime = _atkCoolTimeStat.Value;
        }

        private void OnDestroy()
        {
            _player.GetCompo<AgentStat>().GetStat(_atkCoolTimeStat).OnValueChange -= HandleAtkCoolTimeChange;
        }

        private void HandleAtkCoolTimeChange(StatSO stat, float current, float previous)
        {
            _atkCoolTime = stat.Value;
        }

        private void Update()
        {
            _checkTime += Time.deltaTime;
            if(_checkTime >= _atkCoolTime)
            {
                _checkTime -= _atkCoolTime;
                Debug.Log("Atk");
            }
        }
    }
}
