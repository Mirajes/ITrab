using UnityEngine;

namespace AtomSingleton
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SaveManager _saveManager = new();
        [SerializeField] private Player _player;
        private Initializer _initializer = new();

        private void Start()
        {
            WalletManager.CoinChange += _saveManager.OnCoinChange;

            _saveManager.LoadCoins();
            _saveManager.LoadUpgrades();

            _initializer.InitInputs(_player);

            WalletManager.Instance.CoinChangeUpdate();
        }

        private void OnDestroy()
        {
            WalletManager.CoinChange -= _saveManager.OnCoinChange;

            _initializer.DeinitInputs(_player);
        }

        private void OnApplicationQuit()
        {
            _saveManager.SaveUpgrades();
        }
    }
}