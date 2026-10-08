using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SI
{
    public class Player : MonoBehaviour, IDamagable
    {
        public bool IsAlive => _currentHealth > 0;
        public int CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                _currentHealth = value;
                HealthUpdate?.Invoke(_currentHealth);
            }
        }
        private int _currentHealth = 3;
        [SerializeField] private int _maxHealth = 3;

        [SerializeField] private LevelBounds _levelBounds;

        [SerializeField] private float _moveSpeed;

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite _sprite0;
        [SerializeField] private Sprite _sprite1;
        private int _currentSprite = 0;

        [SerializeField] private PlayerBullet _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private int _shootInterval = 4;
        private int _stepsWithoutShoot = 0;
        //[SerializeField] private float _fireSpeed = 0.3f;
        //private float _nextShotTime;
        private float _moveInput;

        public static event Action Shoot;
        public static event Action<int> HealthUpdate;

        private void Start()
        {
            CurrentHealth = _maxHealth;

            Shoot += OnShoot;
            GameManager.StartGame += OnStartGame;
            GameManager.Step += OnStep;
        }

        private void OnDestroy()
        {
            Shoot -= OnShoot;
            GameManager.StartGame -= OnStartGame;
            GameManager.Step -= OnStep;
        }

        //private void FixedUpdate()
        //{
        //    HandleMove();
        //}

        private void OnStep()
        {
            HandleMove();
            SpriteChange();

            _stepsWithoutShoot++;
        }

        private void HandleMove()
        {
            float moveX = this.transform.position.x
                + _moveInput * _moveSpeed * Time.fixedDeltaTime;

            moveX = Mathf.Clamp(moveX,
                _levelBounds.MinX,
                _levelBounds.MaxX
                );

            this.transform.position = new Vector2(moveX, this.transform.position.y);
        }

        private void OnShoot()
        {
            PlayerBullet newBullet = Instantiate(_bulletPrefab,
                _firePoint.position,
                _firePoint.rotation
                );
            newBullet.Init();
        }

        private void OnStartGame()
        {
            _moveInput = 1;
            HealthUpdate?.Invoke(_currentHealth);
        }

        private void SpriteChange() // TODO: improve this
        {
            if (_currentSprite == 0)
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

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<float>();
        }

        public void OnShootInput(InputAction.CallbackContext context)
        {
            if (_stepsWithoutShoot >= _shootInterval)
            {
                _stepsWithoutShoot = 0;
                Shoot?.Invoke();
            }

            //if (_nextShotTime <= Time.time)
            //{
            //    Shoot?.Invoke();
            //    _nextShotTime = Time.time + _fireSpeed;
            //}
        }

        public void Damage(int damage)
        {
            CurrentHealth -= damage;
        }
    }

    /*
     * 
    no rigidBody, only math and screenLimits
    private void Update()
    {
        // Двигаем объект напрямую через transform
        float newX = transform.position.x + moveInputX * speed * Time.deltaTime;

        // Ограничиваем координаты жестко в коде
        newX = Mathf.Clamp(newX, -screenLimitX, screenLimitX);

        transform.position = new Vector2(newX, transform.position.y);
    }

    */
}
