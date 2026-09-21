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

        public void Damage(int damage)
        {
            CurrentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            this.gameObject.SetActive(false);
            GameManager.ChangeScore?.Invoke(_scoreToGive);
        }

        private void Shoot()
        {

        }

        private void OnStep()
        {

        }
    }
}