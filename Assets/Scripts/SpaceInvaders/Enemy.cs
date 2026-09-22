using System;
using UnityEngine;

namespace SI
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite _sprite0;
        [SerializeField] private Sprite _sprite1;
        private int _currentSprite = 0;

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
            //Move();
            ChangeSprite();
        }

        private void OnDie()
        {
            this.gameObject.SetActive(false);
            Die?.Invoke(_scoreToGive);
        }

        private void Move()
        {
            MoveTo(new Vector3(this.transform.position.x,
                this.transform.position.y - _stepSize
                ));
        }

        private void ChangeSprite()
        {
            if (_currentSprite == 0) // TODO: improve this
            {
                _currentSprite = 1;
                _renderer.sprite = _sprite1;
            }
            else
            {
                _currentSprite = 0;
                _renderer.sprite = _sprite0;
            }
        }
    }
}