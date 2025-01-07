using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BGD.Combat
{
    public enum CastTypeEnum // Cast타입
    {
        Damge
    }

    public class Caster : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private Vector2 _castOffset; // 범위 시작위치를 조정하기 위한offset
        private Dictionary<CastTypeEnum, BaseCaster> _casters; //Cast할 종류Dictionary
        private Collider2D[] castTargets; // 감지한 오브젝트의 Collider를 담는 변수
        [SerializeField] private BaseCaster _currentCast;//현재 감자히는 Caster를 담아 놓는 변수

        public void Initialize(Agent agent)
        {
            _casters = new Dictionary<CastTypeEnum, BaseCaster>();
            BaseCaster[] casts = GetComponents<BaseCaster>();//감지할 캐스트 종류를 가져옴
            foreach (BaseCaster c in casts)
            {
                _casters.Add(c.castType, c); // Enum으로 cast구결하기 위해 추가
            }
        }

        public void Cast(CastTypeEnum castType)//원하는 캐스트 타입과 얼마나 체크할지를 받는다.
        {
            _currentCast = _casters.GetValueOrDefault(castType);//타입에 맞는 Cast를 갖고 온다.
            Debug.Assert(_currentCast != null, $"{castType}cast없어 돌아가"); // CurrentCast가 Null아니라면 실행

            castTargets = Physics2D.OverlapCircleAll((Vector2)transform.position + _castOffset, _currentCast.castRange
                , _currentCast.targetLayer, 0, _currentCast.castCnt);//cat설정에 맞게 OverapCircleAll체크

            if (castTargets.Length > 0)
            {
                _currentCast.Cast(castTargets);//체크된 객체가 있다면 현재 cast에게 collider[]변수를 넘긴다.
                //Debug.Log("감지");
            }
            //else
                //Debug.Log("주변에 감지된 물체가 없습니다.");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if(_currentCast != null ) 
                Gizmos.DrawWireSphere((Vector2)transform.position + _castOffset, _currentCast.castRange);
        }
    }
}
