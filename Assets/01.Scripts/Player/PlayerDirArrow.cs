using BGD.Agents;
using BGD.Core.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BGD.Players

{
    public class PlayerDirArrow : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private float _arrowDistance;
        private Player _player;

        public void Initialize(Agent agnet)
        {
            _player = agnet as Player;

        }

        private void Start()
        {
            MouseManager.Instance.SetMousePoint(false);
        }
        private void LateUpdate()
        {
            SetMouseDir();

        }
        private void SetMouseDir()
        {
            Vector2 mousePos = MouseManager.Instance.MouseDir;
            Vector2 playerPos = _player.transform.position;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            if(Mathf.Abs(angle -transform.rotation.z)>5)
            {
                transform.rotation = Quaternion.Euler(0, 0, angle);
                //Vector2 mouseDir = (mousePos - playerPos).normalized;
                //MouseDir = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;

                transform.position = (mousePos * _arrowDistance) + (Vector2)_player.transform.position;
                //transform.rota(MouseDir);
            }
        }
    }
}
