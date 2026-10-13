using TMPro;
using UnityEngine;

namespace AtomSingleton
{
    public class UI_MagazinButton : MonoBehaviour
    {
        [SerializeField] private SO_A_Upgrade _upgradeToBuy;
        [SerializeField] private TMP_Text _upgradeCount_txt;
        [SerializeField] private TMP_Text _upgradeCost_txt;

        public void TryBuyUpgrade()
        {
            if (WalletManager.Instance.CoinCount >= _upgradeToBuy.CurrentCost)
            {
                WalletManager.Instance.RemoveCoin(_upgradeToBuy.CurrentCost);
                // Action?.Invoke
            }
            else
            {
                Debug.Log($"[{this.name}] - U cant afford it");
            }
        }
    }
}