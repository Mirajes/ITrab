using UnityEngine;

public class MBh_Trap : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.Damage(_damage);
        }
        
    }
}

//[RequireComponent(typeof(CharacterController))]
//public class MBh_Player : MonoBehaviour
//{
//    [SerializeField] private CharacterController _controller;
//    [SerializeField] private Camera _camera;

//    [SerializeField] private float _moveSpeed = 3f;
//    [SerializeField] private float _jumpPower = 5f;
//    private bool _isJumping;
//    [SerializeField] private float _gravityMultiplier = 1f;
//    private Vector3 
//    public bool IsGrounded => _controller.isGrounded;

//    [SerializeField] private float _health = 100f;

//    private void Awake()
//    {
        
//    }

//    private void SetGravity()
//    {
//        if (IsGrounded && )
//    }
//    private void HandleMove()
//    {

//    }
//    private void HandleRotate()
//    {

//    }

//    public void OnMoveInput(InputAction.CallbackContext context)
//    {

//    }
//    public void OnRotateInput(InputAction.CallbackContext context)
//    {

//    }

//    public void OnJumpInput(InputAction.CallbackContext context)
//    {

//    }
//}