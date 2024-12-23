using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BGD.Combat
{
    public enum CastTypeEnum
    {
        Damge
    }

    public class Caster : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private float _castRadius;
        [SerializeField] private Vector2 _castOffset;
        private Dictionary<CastTypeEnum, BaseCaster> _casters;
        private Collider2D[] castTargets;
        private BaseCaster _currentCast;

        public void Initialize(Agent agent)
        {
            _casters = new Dictionary<CastTypeEnum, BaseCaster>();
            BaseCaster[] casts = GetComponents<BaseCaster>();
            foreach (BaseCaster c in casts)
            {
                _casters.Add(c.castType, c);
            }
        }

        public void Cast(CastTypeEnum castType, int castCnt)
        {
            _currentCast = _casters.GetValueOrDefault(castType);
            Debug.Assert(_currentCast != null, $"{castType}cast없어 돌아가");

            castTargets = Physics2D.OverlapCircleAll((Vector2)transform.position + _castOffset, _castRadius
                ,_currentCast.targetLayer, 0, _currentCast.castCnt);

            if (castTargets.Length > 0)
            {
                _currentCast.Cast(castTargets);
            }
            else
                Debug.Log("주변에 감지된 물체가 없습니다.");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere((Vector2)transform.position + _castOffset,_castRadius);
        }
    }
}
