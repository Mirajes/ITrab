using TMPro;
using UnityEngine;

namespace AtomSingleton
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinAmount_txt;

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
            _coinAmount_txt.text = coinAmount.ToString();
        }
    }
}