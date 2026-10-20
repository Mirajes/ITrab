using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AtomSingleton
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private CharacterController _controller;

        [Header("Move")]
        [SerializeField] private float _moveSpeed = 2f;

        [Header("Acceleration")]
        [SerializeField] private float _accelerationMultMin = 1f;
        [SerializeField] private float _accelerationMultMax = 3f;
        [SerializeField] private float _accelerationRaiseMult = 2f;
        private float _accelerationMult = 1f;

        [Header("Rotate")]
        [SerializeField] private float _rotateLerpSpeed = 5f;

        [Header("Jump")]
        [SerializeField] private float _jumpPower = 5f;
        private Vector3 _velocity;
        private bool _isNeedToMove = false;
        private bool _isRotating = false;

        [SerializeField] private List<float> _rotateList = new();
        private float _rotateDegrees = 90f;
        private float _targetRotateDegrees;
        private float _moveInput;
        private float _rotateInput;
        private bool _isGrounded => _controller.isGrounded;

        private void Update()
        {
            SetGravity();
            HandleMove();
            HandleRotate();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!(Mathf.Abs(hit.normal.y) < 0.1f))
                return;

            _isNeedToMove = false;
            _accelerationMult = _accelerationMultMin;
            _velocity = Vector3.zero;
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            if (_isNeedToMove)
                return;

            _moveInput = context.ReadValue<float>();

            _isNeedToMove = true;
        }

        public void OnRotateInput(InputAction.CallbackContext context)
        {
            if (!context.started)
                return;

            _rotateInput = context.ReadValue<float>();
            _rotateList.Add(_rotateDegrees * _rotateInput);
        }

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            Jump();
        }

        private void HandleMove()
        {
            if (!_isNeedToMove)
            {
                //Vector3 gravityVelocity = new Vector3(0f, _velocity.y, 0f);
                _controller.Move(_velocity * Time.deltaTime);
            }
            else
            {
                Vector3 moveDirection = transform.forward * _moveInput * _moveSpeed * _accelerationMult;
                _velocity.x = moveDirection.x;
                _velocity.z = moveDirection.z;

                _controller.Move(_velocity * Time.deltaTime);

                _accelerationMult += Time.deltaTime * _accelerationRaiseMult;
                _accelerationMult = Mathf.Clamp(_accelerationMult, _accelerationMultMin, _accelerationMultMax * (1 + (1 * Inventory.Instance.SpeedUp.CurrentLevel)));
            }

        }

        private void HandleRotate() // bad movement
        {
            if (_rotateList.Count == 0 && !_isRotating) return;

            if (!_isRotating)
            {
                _targetRotateDegrees = transform.eulerAngles.y + _rotateList[0];
                _isRotating = true;
            }
            else
            {
                Quaternion targetRotation = Quaternion.Euler(0f, _targetRotateDegrees, 0f);

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    _rotateLerpSpeed * _rotateDegrees * Time.deltaTime * (1 + (0.1f * Inventory.Instance.RotateSpeedUp.CurrentLevel)) // + upgrade
                    );

                if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
                {
                    transform.rotation = targetRotation;

                    _isRotating = false;
                    _rotateList.RemoveAt(0);
                }
            }
        }

        private void Jump()
        {
            if (!_isGrounded) return;

            _velocity.y = _jumpPower;
        }

        private void SetGravity()
        {
            if (_velocity.y < 0 && _isGrounded)
            {
                _velocity.y = -1;
            }
            else
            {
                _velocity.y += Physics.gravity.y * Time.deltaTime;
            }
        }

        public void ForceTransform(Transform target)
        {
            _isNeedToMove = false;
            _isRotating = false;
            _velocity = Vector3.zero;
            _accelerationMult = _accelerationMultMin;
            _moveInput = 0;
            _rotateList.Clear();

            _controller.enabled = false;

            this.transform.SetPositionAndRotation(target.position, target.rotation);
            _controller.enabled = true;
        }
    }
}