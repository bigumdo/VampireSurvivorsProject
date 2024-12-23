using BGD.Combat;
using BGD.StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD
{
    public class AgentHealth : MonoBehaviour, IDamageable
    {

        [SerializeField] private StatSO _hp;
        private float _currentHealth;
        private void Awake()
        {
            _currentHealth = _hp.Value;
        }

        public void ApplyDamage(float damage)
        {
            _currentHealth -= damage;
        }
    }
}
