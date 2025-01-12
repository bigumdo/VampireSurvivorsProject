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
            // T Ÿ���̸鼭 IAgentComponent�� �����ߴ��� Ȯ���ϰ� Ȯ�� �ߴٸ� component�� out��
            if (_components.TryGetValue(typeof(T), out IAgentComponent component))
            {
                return (T)component;
            }

            //�Ļ��� Ŭ������ ã�� ������ üũ �ߴ��� Ȯ��
            if (isDerived == false)
                return default;

            //�θ�Ŭ������ ��ӹ޾� ���� Ŭ�������� Ȯ���ϰ� �´ٸ� �ִٸ� Type�� ����
            Type findType = _components.Keys.FirstOrDefault(t => t.IsSubclassOf(typeof(T)));

            //�θ�Ŭ������ ��ӹ��� Ŭ������ �θ�Ŭ���� ������ ��ȯ
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
