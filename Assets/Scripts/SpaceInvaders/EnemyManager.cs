using System.Collections.Generic;
using UnityEngine;

namespace SI
{
    public class EnemyManager : MonoBehaviour
    {
        [Header("SpawnSettings")]
        [SerializeField] private Transform _spawnArea;
        [SerializeField] private List<Enemy> _rowOrderToSpawn;
        [SerializeField] private int _enemyToSpawn = 55;

        [Header("Core")]
        [SerializeField] private List<Enemy> _aliveEnemy;
        [SerializeField] private Transform _enemyFolder;

        [Header("Shoot")]
        [SerializeField] private float _shootChance = 0.2f;
        [SerializeField] private float _shootChanceRaisePerDie = 0.03f;
        private float _shootChanceCap = 5f;

        private void Start()
        {
            SpawnEnemies();

            SideWall.EnemyTouch += OnEnemyTouch;
            GameManager.PlayerWin += OnPlayerWin;
            GameManager.Step += OnStep;
            Enemy.Die += OnDie;
        }

        private void OnDestroy()
        {
            SideWall.EnemyTouch -= OnEnemyTouch;
            GameManager.PlayerWin -= OnPlayerWin;
            GameManager.Step -= OnStep;
            Enemy.Die -= OnDie;
        }

        private void OnPlayerWin()
        {
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
                int randomEnemyIndex = Random.Range(0, _aliveEnemy.Count);
                Enemy shooterEnemy = _aliveEnemy[randomEnemyIndex];
                shooterEnemy.Shoot();
            }
        }

        private void OnEnemyTouch()
        {
            ChangeMoveDirection();
        }

        private void OnDie(Enemy enemy)
        {
            if (_aliveEnemy.Contains(enemy))
                _aliveEnemy.Remove(enemy);
            else
                Debug.Log($"[EnemyManager] - no enemy in alive list -> {enemy}");
        }

        private void ChangeMoveDirection()
        {
            foreach (Enemy enemy in _aliveEnemy)
            {
                enemy.WeNeedToGoDown();
                enemy.ChangeDirection();
            }
        }

        private void SpawnEnemies()
        {
            int rowCount = _rowOrderToSpawn.Count;
            int enemyInRowRaw = Mathf.CeilToInt((float)_enemyToSpawn / rowCount); // no remaining

            Vector2 areaSize = new Vector2(_spawnArea.lossyScale.x, _spawnArea.lossyScale.y);
            Vector3 areaCenter = _spawnArea.position;

            float distanceBetweenEnemyX = enemyInRowRaw > 1
                ? areaSize.x / (enemyInRowRaw - 1) : 0;
            float distanceBetweenEnemyY = rowCount > 1
                ? areaSize.y / (rowCount - 1) : 0;

            float startX = areaCenter.x - (areaSize.x / 2f);
            float startY = areaCenter.y + (areaSize.y / 2f);

            if (enemyInRowRaw == 1) startX = areaCenter.x;
            if (rowCount == 1) startY = areaCenter.y;

            int spawnedCount = 0;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < enemyInRowRaw; j++)
                {
                    if (spawnedCount >= _enemyToSpawn) break;

                    float currentXPos = startX + (j * distanceBetweenEnemyX);
                    float currentYPos = startY - (i * distanceBetweenEnemyY);

                    Vector3 spawnPosition = new Vector3(currentXPos,
                        currentYPos,
                        areaCenter.z);

                    Enemy newEnemy = Instantiate(_rowOrderToSpawn[i],
                        spawnPosition,
                        Quaternion.identity,
                        _enemyFolder
                        );

                    _aliveEnemy.Add(newEnemy);
                    spawnedCount++;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (_spawnArea == null) return;

            Gizmos.color = new Color(0f, 0.5f, 0.5f, 0.1f);
            Gizmos.DrawCube(_spawnArea.position, _spawnArea.lossyScale);
        }
    }
}

// not working correctly
//private void SpawnEnemies()
//{
//    int rowCount = _rowOrderToSpawn.Count;
//    int enemyInRowRaw = _enemyToSpawn / rowCount;
//    int remainingEnemyCount = _enemyToSpawn % rowCount;
//    int evenDivider = enemyInRowRaw % 2 == 0 ? 2 : 3;

//    float distanceBetweenEnemyX = _spawnArea.localScale.x / enemyInRowRaw;
//    float distanceBetweenEnemyY = _spawnArea.localScale.y / rowCount;
//    float currentXPos = -_spawnArea.localScale.x / evenDivider;
//    float currentYPos = _spawnArea.position.y + (_spawnArea.localScale.y / 2);

//    for (int i = 0; i < rowCount; i++)
//    {
//        for (int spawnEnemyIndex = 0; spawnEnemyIndex < enemyInRowRaw; spawnEnemyIndex++)
//        {

//            Vector3 position = new Vector3(currentXPos, currentYPos);

//            Enemy newEnemy = Instantiate(_rowOrderToSpawn[i],
//                position,
//                Quaternion.identity,
//                _enemyFolder
//                );

//            _aliveEnemy.Add(newEnemy);

//            currentXPos += distanceBetweenEnemyX;
//        }

//        currentXPos = -_spawnArea.localScale.x / evenDivider;
//        currentYPos -= distanceBetweenEnemyY;
//    }

//    if (remainingEnemyCount <= 0) return;

//    evenDivider = enemyInRowRaw % 2 == 0 ? 2 : 3;
//    Enemy enemyPrefab = _rowOrderToSpawn[_rowOrderToSpawn.Count - 1];
//    distanceBetweenEnemyX = _spawnArea.localScale.x / remainingEnemyCount;
//    currentXPos = -_spawnArea.localScale.x / evenDivider;

//    for (int remainingEnemyIndex = 0; remainingEnemyIndex < remainingEnemyCount; remainingEnemyIndex++)
//    {
//        Vector3 position = new Vector3(currentXPos, distanceBetweenEnemyY);
//        Enemy newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity, _enemyFolder);
//        _aliveEnemy.Add(newEnemy);

//        currentXPos += distanceBetweenEnemyX;
//    }
//}