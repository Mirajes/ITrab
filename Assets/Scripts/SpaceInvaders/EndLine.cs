using System;
using UnityEngine;

namespace SI
{
    public class EndLine : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyMask;

        public static event Action EnemyTouch;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (((1 << collision.gameObject.layer) & _enemyMask) == 0)
                return;

            EnemyTouch?.Invoke();
        }
    }
}