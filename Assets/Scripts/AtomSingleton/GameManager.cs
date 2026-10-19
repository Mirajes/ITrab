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
            _player.transform.SetPositionAndRotation(_playerTransform_Game.position, _playerTransform_Game.rotation);

            _cameraManager.MoveToTarget(_cameraTransform_Game, () =>
            {
                _cameraManager.Camera.transform.SetParent(_player.transform);
                _initializer.InputMap.Player_Atom.Enable();
            });
        }

        private void OnExit()
        {
            _initializer.InputMap.Player_Atom.Disable();
            _player.transform.SetPositionAndRotation(_playerTransform_Menu.position, _playerTransform_Menu.rotation);

            _cameraManager.Camera.transform.SetParent(null);
            _cameraManager.MoveToTarget(_cameraTransform_Menu);
        }

        public void OnPauseInput(InputAction.CallbackContext context)
        {
            _uiManager.SwitchPause();
        }
    }
}