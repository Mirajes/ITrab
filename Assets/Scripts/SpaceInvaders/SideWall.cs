using System;
using UnityEngine;

namespace SI
{
    // InputSystem.onAnyButtonPress.CallOnce(control => OnAnyKeyPressed(control))

    public class SideWall : MonoBehaviour
    {
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private LayerMask _enemyMask;
        private Collider[] _hitColliders = new Collider[8];
        private Vector2 _checkSize;

        [SerializeField] private float _wallSize = 0.5f;
        public float WallSize => _wallSize;

        public static event Action EnemyTouch;

        private void Start()
        {
            _checkSize = new Vector2(_levelBounds.Height, _wallSize);

            GameManager.Step += OnStep;
        }

        private void OnDestroy()
        {
            GameManager.Step -= OnStep;
        }

        private void OnStep()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.contactMask != _enemyMask) return;

            EnemyTouch?.Invoke();
        }
    }
}