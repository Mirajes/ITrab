using System.Collections.Generic;
using UnityEngine;

namespace SI
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies = new();

        private void Start()
        {
            SideWall.EnemyTouch += OnEnemyTouch;
        }

        private void OnDestroy()
        {
            SideWall.EnemyTouch -= OnEnemyTouch;
        }

        private void OnEnemyTouch()
        {
            ChangeMoveDirection();
        }

        private void ChangeMoveDirection()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.WeNeedToGoDown();
                enemy.ChangeDirection();
            }
        }

        // enemySpawn()
    }
}