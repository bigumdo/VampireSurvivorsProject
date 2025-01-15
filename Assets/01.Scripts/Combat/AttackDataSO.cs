using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Combat
{
    [CreateAssetMenu(menuName ="SO/Combat/AttackDataSO")]
    public class AttackDataSO : ScriptableObject
    {
        public string attackName;
        public Vector2 knockBackForce;
        public float damageMultiplier;
        public float damageIncrease;
        public bool isPowerAttack;

        public float cameraShakePower;
        public float cameraShakeDuration;

        private void OnValidate()
        {
            attackName = this.name;
        }
    }
}
