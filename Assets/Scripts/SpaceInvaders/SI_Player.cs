using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
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

    [SerializeField] private CharacterController _controller;
    [SerializeField] private SI_Bullet _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireSpeed;
    //private float _nextShotTime;

    private float _moveInput;
    private Vector3 _velocity;

    private void Update()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        _velocity.x = _moveInput * _moveSpeed;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void Shoot()
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
        Shoot();
    }

    public void Damage(int damage)
    {
        CurrentHealth -= damage;
    }
}

public class Enemy : MonoBehaviour, SI_IDamagable
{
    public void Damage(int damage)
    {
        throw new System.NotImplementedException();
    }
}

public class Bootstrap : MonoBehaviour 
{

}

public interface SI_IDamagable
{
    public void Damage(int damage);
}