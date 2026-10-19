using TMPro;
using UnityEngine;

namespace AtomSingleton
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinAmount_txt;
        [SerializeField] private UI_StartMenu _startMenu;
        [SerializeField] private UI_PauseMenu _pauseMenu;

        private void Start()
        {
            UI_StartMenu.Enter += OnEnter;
            UI_PauseMenu.Exit += OnExit;
            WalletManager.CoinChange += OnCoinChange;

            _startMenu.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            UI_StartMenu.Enter -= OnEnter;
            UI_PauseMenu.Exit -= OnExit;
            WalletManager.CoinChange -= OnCoinChange;
        }

        private void OnCoinChange(int coinAmount)
        {
            _coinAmount_txt.text = coinAmount.ToString();
        }

        public void SwitchPause()
        {
            _pauseMenu.gameObject.SetActive(!_pauseMenu.gameObject.activeSelf);
        }

        private void OnEnter()
        {
            _startMenu.gameObject.SetActive(false);
            _pauseMenu.gameObject.SetActive(false);
        }

        private void OnExit()
        {
            _startMenu.gameObject.SetActive(true);
            _pauseMenu.gameObject.SetActive(false);
        }
    }
}