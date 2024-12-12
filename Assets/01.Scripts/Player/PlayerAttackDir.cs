using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerAttackDir : MonoBehaviour, IAgentComponent
    {
        private Player _player;
        public Vector2 MouseDir { get; private set; }

        public void Initialize(Agent agnet)
        {
            throw new System.NotImplementedException();
        }

        private void Update()
        {
            SetMouseDir();

        }
        private void SetMouseDir()
        {
            Vector2 playerPos = transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MouseDir = (mousePos - playerPos).normalized;
            //Vector2 mouseDir = (mousePos - playerPos).normalized;
            //MouseDir = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, MouseDir * 2);
        }
    }
}
