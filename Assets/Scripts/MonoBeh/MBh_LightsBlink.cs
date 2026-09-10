using UnityEngine;

public class MBh_LightsBlink : MonoBehaviour
{
    [Header("CORE")]
    [SerializeField] private Light _pointLight;
    [SerializeField] private AnimationCurve _curve;

    [Header("Light Settings")]
    [SerializeField] private Color _lightColor = Color.yellow;
    [SerializeField] private float _maxLightIntensity = 10f;
    [SerializeField] private float _lightSpeed = 1f;

    private void Update()
    {
        BlinkLight();
    }

    private void OnEnable()
    {
        _pointLight.color = _lightColor;
    }

    private void BlinkLight()
    {
        _pointLight.intensity = _curve.Evaluate(Time.time * _lightSpeed) * _maxLightIntensity;
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