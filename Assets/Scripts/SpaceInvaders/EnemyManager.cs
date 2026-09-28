using System.Collections.Generic;
using UnityEngine;

namespace SI
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies = new();
        [SerializeField] private List<Enemy> _aliveEnemies = new();
        [SerializeField] private float _shootChance = 0.3f;
        [SerializeField] private float _shootChanceRaisePerDie = 0.03f;
        private float _shootChanceCap = 1f;

        private void Start()
        {
            SideWall.EnemyTouch += OnEnemyTouch;
            GameManager.Step += OnStep;
        }

        private void OnDestroy()
        {
            SideWall.EnemyTouch -= OnEnemyTouch;
            GameManager.Step -= OnStep;
        }

        private void OnStep()
        {
            DeciseToShoot();
        }

        private void DeciseToShoot()
        {
            float procChance = Random.Range(0f, _shootChanceCap);
            if (procChance <= _shootChance)
            {
                int randomEnemyIndex = Random.Range(0, _enemies.Count);

            }
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