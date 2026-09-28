using System;
using UnityEngine;

namespace SI
{
    // InputSystem.onAnyButtonPress.CallOnce(control => OnAnyKeyPressed(control))

    public class SideWall : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyMask;
        private bool _debouceCheck;

        [SerializeField] private float _wallSize = 0.5f;
        public float WallSize => _wallSize;

        public static event Action EnemyTouch;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (((1 << collision.gameObject.layer) & _enemyMask) == 0) // сдвиг?
                return;

            if (!_debouceCheck)
            {
                _debouceCheck = true;
                EnemyTouch?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _debouceCheck = false;
        }
    }
}