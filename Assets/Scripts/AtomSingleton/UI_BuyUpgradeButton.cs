using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AtomSingleton
{
    public class UI_BuyUpgradeButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SO_Upgrade _upgradeToBuy;

        [SerializeField] private TMP_Text _upgradeName_txt;
        [SerializeField] private TMP_Text _upgradeDescription_txt;
        [SerializeField] private TMP_Text _currentLevel_txt;
        [SerializeField] private TMP_Text _cost_txt;

        public void Start()
        {
            if (_upgradeToBuy == null) return;

            _upgradeName_txt.text = _upgradeToBuy.Name;
            _upgradeDescription_txt.text = _upgradeToBuy.Description;
            _button.onClick.AddListener(Buy);

            UpdateMe();
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveListener(Buy);
        }

        private void UpdateMe()
        {
            if (_upgradeToBuy.IsMaxLevel)
            {
                _currentLevel_txt.text = "MAX LEVEL";
                _cost_txt.text = string.Empty;
                _button.interactable = false;
            }
            else
            {
                _currentLevel_txt.text = $"LEVEL: {_upgradeToBuy.CurrentLevel}";
                _cost_txt.text = $"{_upgradeToBuy.CurrentCost}";
            }
        }

        private void Buy()
        {
            if (WalletManager.Instance.CoinCount < _upgradeToBuy.CurrentCost) return;

            WalletManager.Instance.RemoveCoin(_upgradeToBuy.CurrentCost);
            _upgradeToBuy.LevelUp(); // no save :O

            UpdateMe();
        }
    }
}