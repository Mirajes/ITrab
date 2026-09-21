using System;
using UnityEngine;

namespace SI
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] private int _maxHealth = 1;
        [SerializeField] private float _stepSize = 0.3f;
        [SerializeField] private EnemyBullet _bulletPrefab;
        private int _currentHealth;
        public int CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                _currentHealth = value;
            }
        }
        [SerializeField] private int _scoreToGive = 1;

        public static event Action<int> Die;

        private void OnEnable()
        {
            GameManager.Step += OnStep;
        }

        private void OnDisable()
        {
            GameManager.Step -= OnStep;
        }

        public void Damage(int damage)
        {
            CurrentHealth -= damage;
            if (_currentHealth <= 0)
            {
                OnDie();
            }
        }

        public void Shoot()
        {
            A_Bullet newBullet = Instantiate(_bulletPrefab, this.transform.position, this.transform.rotation);
            newBullet.Init();
        }

        public void MoveTo(Vector3 newPosition)
        {
            this.transform.position = newPosition;
        }

        private void OnStep()
        {
            MoveTo(new Vector3(this.transform.position.x,
                this.transform.position.y - _stepSize
                ));
        }

        private void OnDie()
        {
            this.gameObject.SetActive(false);
            Die?.Invoke(_scoreToGive);
        }
    }
}