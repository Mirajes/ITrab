using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AtomSingleton
{
    public class Player : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private CharacterController _controller;

        [Header("Settings")]
        [SerializeField] private float _moveSpeed_Base = 2f;
        [SerializeField] private float _accelerationSpeedMultiplierMax = 3f;
        [SerializeField] private float _rotateSLerpSpeed_Base = 5f;
        [SerializeField] private float _jumpPower = 5f;
        private Vector3 _velocity;
        private bool _isNeedToMove = false;
        private bool _isNeedToRotate = false;

        private List<float> _rotateList = new();
        private float _rotateDegrees = 90f;
        private float _moveInput;
        private float _rotateInput;

        private void Update()
        {
            
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<float>();
        }

        public void OnRotateInput(InputAction.CallbackContext context)
        {
            _rotateInput = context.ReadValue<float>();
        }

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            Jump();
        }

        private void Move()
        {
            if (!_isNeedToMove) return;


        }

        private void Rotate()
        {

        }

        private void Jump()
        {

        }
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private Initializer _initializer = new();

        private void Start()
        {
            _initializer.InitInputs(_player);
        }

        private void OnDestroy()
        {
            _initializer.DeinitInputs(_player);
        }

        private void ResetProgress() { }
    }

    public class Initializer
    {
        public void InitInputs(Player player)
        {

        }

        public void DeinitInputs(Player player)
        {

        }
    }

    public class WalletManager : Singleton<WalletManager>
    {
        private int _coinCount = 0;

        public static event Action<int> CoinChange;

        public void AddCoin(int coinToAdd)
        {
            _coinCount += coinToAdd;
            CoinChange?.Invoke(_coinCount);
        } 
        public void RemoveCoin(int coinToRemove)
        {
            _coinCount -= coinToRemove;
            CoinChange?.Invoke(_coinCount);
        }
    }

    public class Inventory : Singleton<Inventory>
    {

    }

    public class UIManager : MonoBehaviour
    {
        private void Start()
        {
            WalletManager.CoinChange += OnCoinChange;
        }

        private void OnDestroy()
        {
            WalletManager.CoinChange -= OnCoinChange;
        }

        private void OnCoinChange(int coinAmount)
        {

        }
    }
}