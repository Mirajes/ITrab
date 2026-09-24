using System;
using UnityEngine;

namespace SI
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        [Header("Anim")]
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite _sprite0;
        [SerializeField] private Sprite _sprite1;
        private int _currentSprite = 0;

        [Header("Die")]
        [SerializeField] private Color _bodyColor = Color.red;
        [SerializeField] private int _scoreToGive = 1;


        [Header("Combat")]
        [SerializeField] private int _maxHealth = 1;
        [SerializeField] private float _stepSizeX = 0.3f;
        [SerializeField] private float _stepSizeY = 1f;
        [SerializeField] private EnemyBullet _bulletPrefab;
        private bool _isNeedToMoveDown = false;
        private Direction _directionX = Direction.Right;
        private int _currentHealth;
        public int CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                _currentHealth = value;
            }
        }

        public static event Action Die;
        public static event Action<int> DieScore;
        public static event Action<Vector3, Color> DieExplosion;

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

        public void WeNeedToGoDown() { _isNeedToMoveDown = true; }
        public void ChangeDirection()
        {
            if (_directionX == Direction.Left)
                _directionX = Direction.Right;
            else
                _directionX = Direction.Left;
        }

        private void OnStep()
        {
            Move();
            ChangeSprite();
        }

        private void OnDie()
        {
            this.gameObject.SetActive(false);
            Die?.Invoke();
            DieScore?.Invoke(_scoreToGive);
            DieExplosion?.Invoke(this.transform.position, _bodyColor);
        }

        private void Move()
        {
            Vector2 newPosition = Vector2.zero;

            if (_isNeedToMoveDown)
            {
                _isNeedToMoveDown = false;
                newPosition = new Vector2(
                        this.transform.position.x,
                        this.transform.position.y - _stepSizeY
                        );
            }
            else
            {
                newPosition = new Vector2(
                    this.transform.position.x + (_stepSizeX * (int)_directionX),
                    this.transform.position.y
                    );
            }

            this.transform.position = newPosition;
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

        enum Direction
        {
            Left = -1,
            Right = 1,
        }
    }
}