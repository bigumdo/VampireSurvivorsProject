using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace BGD.Agents
{
    public class Agent : MonoBehaviour
    {
        public bool IsDead { get; set; }
        public bool CanMove { get; set; } = true;

        protected Dictionary<Type, IAgentComponent> _components;
        public UnityEvent OnDeadEvent;
        public UnityEvent OnHitEvent;

        protected virtual void Awake()
        {
            _components = new Dictionary<Type, IAgentComponent>();

            GetComponentsInChildren<IAgentComponent>(true).ToList()
                .ForEach(x => _components.Add(x.GetType(), x));
            InitializeComponenets();
            AfterInitComponents();
        }

        private void InitializeComponenets()
        {
            _components.Values.ToList().ForEach(x => x.Initialize(this));
        }

        private void AfterInitComponents()
        {
            _components.Values.ToList().ForEach(x =>
            {
                if (x is IAfterInitable afterInit )
                {
                    afterInit.AfterInit();
                }
            });
            OnDeadEvent.AddListener(HandleDeadEvent);
            OnHitEvent.AddListener(HandleHitEvent);
        }

        public T GetCompo<T>(bool isDerived = false) where T : IAgentComponent
        {
            // T 타입이면서 IAgentComponent를 구현했는지 확인하고 확인 했다면 component로 out함
            if (_components.TryGetValue(typeof(T), out IAgentComponent component))
            {
                return (T)component;
            }

            //파생된 클래스도 찾을 것인지 체크 했는지 확인
            if (isDerived == false)
                return default;

            //부모클래스를 상속받아 만든 클래스인지 확인하고 맞다면 있다면 Type에 저장
            Type findType = _components.Keys.FirstOrDefault(t => t.IsSubclassOf(typeof(T)));

            //부모클래스를 상속받은 클래스를 부모클래스 형으로 반환
            if (findType != null)
                return (T)_components[findType];

            return default;
        }

        private void OnDestroy()
        {
            OnDeadEvent.RemoveListener(HandleDeadEvent);
            OnHitEvent.RemoveListener(HandleHitEvent);
        }

        public virtual void HandleHitEvent()
        {

        }

        public virtual void HandleDeadEvent()
        {

        }
    }
}
