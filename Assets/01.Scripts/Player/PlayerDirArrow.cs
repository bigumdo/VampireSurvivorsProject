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
            Vector2 mousePos = MouseManager.Instance.MousePos;
            Vector2 playerPos = _player.transform.position;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position = (mousePos * _arrowDistance) + (Vector2)_player.transform.position;
        }
    }
}
