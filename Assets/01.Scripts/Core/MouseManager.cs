using BGD.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Core.Manager
{
    public class MouseManager : MonoSingleton<MouseManager>

    {
        private Player _player;
        public Vector2 MouseDir { get; private set; }

        public void SetMousePoint(bool isMousePoint)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }

        private void SetMouseDir()
        {
            Vector2 playerPos = _player.transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MouseDir = (mousePos - playerPos).normalized;

            //float angle = Mathf.Atan2(mousePos.y - playerPos.y, mousePos.x - playerPos.x) * Mathf.Rad2Deg;

            //transform.rotation = Quaternion.Euler(0, 0, angle);
            //Vector2 mouseDir = (mousePos - playerPos).normalized;
            //MouseDir = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;

            //transform.position = MouseDir * _arrowDistance;
            //transform.rota(MouseDir);
        }
    }
}
