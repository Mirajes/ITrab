using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SI_Player : MonoBehaviour, SI_IDamagable
{
    public bool IsAlive => _currentHealth > 0;
    public int CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = value;
            // invoke UI
        }
    }
    private int _currentHealth = 3;
    private int _maxHealth = 3;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _screenSizeX = 4f;

    [SerializeField] private SI_Bullet _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireSpeed = 0.3f;
    private float _nextShotTime;

    public static event Action Shoot;

    private float _moveInput;

    private void Start()
    {
        Shoot += OnShoot;
        SI_GameManager.Step += HandleMove;
    }

    private void OnDestroy()
    {
        Shoot -= OnShoot;
        SI_GameManager.Step -= HandleMove;
    }

    private void HandleMove()
    {
        float moveX = this.transform.position.x
            + _moveInput * _moveSpeed * Time.fixedDeltaTime;

        moveX = Mathf.Clamp(moveX, -_screenSizeX, _screenSizeX);

        this.transform.position = new Vector2(moveX, this.transform.position.y);
    }

    private void OnShoot()
    {
        SI_Bullet newBullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        newBullet.Init();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<float>();
    }

    public void OnShootInput(InputAction.CallbackContext context)
    {
        if (_nextShotTime <= Time.time)
        {
            Shoot?.Invoke();
            _nextShotTime = Time.time + _fireSpeed;
        }
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