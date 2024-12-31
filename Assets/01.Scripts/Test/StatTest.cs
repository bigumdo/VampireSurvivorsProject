using BGD.Players;
using BGD.StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BGD.Test
{
    public class StatTest : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private StatSO _testStat;
        [SerializeField] private float _testValue = 3f;
        private void Update()
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                _player.GetCompo<AgentStat>().GetStat(_testStat).AddModifier("Test1", _testValue);
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                _player.GetCompo<AgentStat>().GetStat(_testStat).RemoveModifier("Test1");
            }
        }
    }
}
