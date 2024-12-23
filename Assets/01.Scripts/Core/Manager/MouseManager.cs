using BGD.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Core.Manager
{
    public class MouseManager : MonoSingleton<MouseManager>

    {
        private Player _player;
        public Vector2 MousePos { get; private set; }
        public float MouseDir { get; private set; }
        //public float 

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
        }

        private void LateUpdate()
        {
            SetMouseDir();
        }

        public void SetMousePoint(bool isMousePoint)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }

        private void SetMouseDir()
        {
            Vector2 playerPos = _player.transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MousePos = (mousePos - playerPos).normalized;
            MouseDir = mousePos.x > 0 ? 1 : -1;
        }
    }
}
