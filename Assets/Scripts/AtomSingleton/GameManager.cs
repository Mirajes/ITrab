using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AtomSingleton
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SaveManager _saveManager = new();
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private Player _player;
        [SerializeField] private CameraManager _cameraManager;

        [Header("Menu")]
        [SerializeField] private Transform _cameraTransform_Menu;
        [SerializeField] private Transform _playerTransform_Menu;
        [Header("Game")]
        [SerializeField] private Transform _cameraTransform_Game;
        [SerializeField] private Transform _playerTransform_Game;

        private CancellationTokenSource _ctsEarningCoin;

        private Initializer _initializer = new();

        private void Start()
        {
            UI_StartMenu.Enter += OnEnter;
            UI_PauseMenu.Exit += OnExit;
            WalletManager.CoinChange += _saveManager.OnCoinChange;

            _saveManager.LoadCoins();
            _saveManager.LoadUpgrades();

            _cameraManager.Camera.transform.SetParent(null);
            _player.transform.SetPositionAndRotation(_playerTransform_Menu.position, _playerTransform_Menu.rotation);
            _cameraManager.MoveToTarget(_cameraTransform_Menu);

            _initializer.Init();
            _initializer.InitInputs(this);
            _initializer.InitInputs(_player);
            _initializer.InputMap.AtomGame.Enable();

            WalletManager.Instance.CoinChangeUpdate();
        }

        private void OnDestroy()
        {
            UI_StartMenu.Enter -= OnEnter;
            UI_PauseMenu.Exit -= OnExit;
            WalletManager.CoinChange -= _saveManager.OnCoinChange;

            _initializer.DeInitInputs(this);
            _initializer.DeInitInputs(_player);
            _initializer.DeInit();
        }

        private void OnApplicationQuit()
        {
            _saveManager.SaveUpgrades();
        }

        private void OnEnter()
        {
            // тупой контроллер блокирует телепортацию игрока
            _player.ForceTransform(_playerTransform_Game);

            _cameraManager.MoveToTarget(_cameraTransform_Game, () =>
            {
                _cameraManager.Camera.transform.SetParent(_player.transform);
                _initializer.InputMap.AtomPlayer.Enable();
            });

            _ctsEarningCoin = new CancellationTokenSource();
            CoinEarningAsync(_ctsEarningCoin.Token).Forget();
        }

        private void OnExit()
        {
            StopEarning();
            _initializer.InputMap.AtomPlayer.Disable();

            // тупой контроллер блокирует телепортацию игрока #2 
            // вообще можно вынести это в метод который будет сам перемещать игрока в позицию
            _player.ForceTransform(_playerTransform_Menu);

            _cameraManager.Camera.transform.SetParent(null);
            _cameraManager.MoveToTarget(_cameraTransform_Menu);
        }

        public void OnPauseInput(InputAction.CallbackContext context)
        {
            _uiManager.SwitchPause();
        }

        private async UniTaskVoid CoinEarningAsync(CancellationToken token)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(1), cancellationToken: token);
            WalletManager.Instance.AddCoin(Inventory.Instance.PassiveCoinEarning.CurrentLevel);
        }

        private void StopEarning()
        {
            if (_ctsEarningCoin == null) 
                return;

            _ctsEarningCoin.Cancel();
            _ctsEarningCoin.Dispose();
            _ctsEarningCoin = null;
        }
    }
}