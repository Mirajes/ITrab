using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MBh_Player : MonoBehaviour, IDamagable
{
    [SerializeField] private CharacterController _controller;
    private InputSystem_Actions _inputMap;

    [SerializeField] private float _moveSpeed = 3f;
    private Vector2 _moveInput;
    [SerializeField] private float _jumpPower = 5f;
    private bool _isJumping;
    [SerializeField] private float _gravityMultiplier = 1f;
    private Vector3 _velocity;
    public bool IsGrounded => _controller.isGrounded;

    [SerializeField] private float _health = 100f;

    private void Awake()
    {
        _inputMap = new();
        _inputMap.Player.Move.performed += OnMoveInput;
        _inputMap.Player.Move.canceled += OnMoveInput;
        _inputMap.Enable();
    }

    private void OnDestroy()
    {
        _inputMap.Disable();
        _inputMap.Player.Move.performed -= OnMoveInput;
        _inputMap.Player.Move.canceled -= OnMoveInput;
        _inputMap.Dispose();
    }

    private void Update()
    {
        SetGravity();
        HandleMove();
    }

    private void SetGravity()
    {
        if (IsGrounded && _velocity.y < 0)
        {
            _velocity.y = -1;
        }
        else
        {
            _velocity.y = _gravityMultiplier * Physics.gravity.y * Time.deltaTime;
        }
    }
    private void HandleMove()
    {
        _velocity.x = _moveInput.x * _moveSpeed;
        _velocity.z = _moveInput.y * _moveSpeed;

        _controller.Move(_velocity * Time.deltaTime);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void Damage(float damageAmount)
    {
        _health -= damageAmount;
    }

    public void Heal(float healAmount)
    {
        _health += healAmount;
    }
}