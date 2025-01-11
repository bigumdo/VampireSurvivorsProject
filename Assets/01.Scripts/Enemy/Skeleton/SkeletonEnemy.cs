using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Enemys
{
    public class SkeletonEnemy : BaseEnemy
    {

        protected override  void Awake()
        {
            base.Awake();

        }
        private void Start()
        {
            _stateMachine.Initialize(FSM.FSMState.Move);

        }
    }
}
