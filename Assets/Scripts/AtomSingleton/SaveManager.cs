using Alchemy.Inspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AtomSingleton
{
    [Serializable]
    public class SaveManager
    {
        [SerializeField] private List<SO_Upgrade> _upgrades;
        private const string SAVEKEY_UPGRADE_PREFIX = "Upgrade_";
        private const string SAVEKEY_COIN = "Coins_Count";

        public void LoadCoins()
        {
            if (PlayerPrefs.HasKey(SAVEKEY_COIN))
            {
                WalletManager.Instance.OnLoad(PlayerPrefs.GetInt(SAVEKEY_COIN));
            }
            else
            {
                WalletManager.Instance.OnLoad(0);
            }
        }

        public void LoadUpgrades()
        {
            foreach (SO_Upgrade upgrade in _upgrades)
            {
                string saveKey = SAVEKEY_UPGRADE_PREFIX + upgrade.ID;

                if (PlayerPrefs.HasKey(saveKey))
                {
                    // ??
                    upgrade.OnLoad(PlayerPrefs.GetInt(saveKey));
                }
                else
                {
                    upgrade.Reset();
                }
            }
        }

        public void SaveUpgrades()
        {
            foreach (SO_Upgrade upgrade in _upgrades)
            {
                if (string.IsNullOrEmpty(upgrade.ID))
                {
                    Debug.LogWarning($"NO ID -> {upgrade.name}");
                    continue;
                }

                string saveKey = SAVEKEY_UPGRADE_PREFIX + upgrade.ID;
                PlayerPrefs.SetInt(saveKey, upgrade.CurrentLevel);
            }

            PlayerPrefs.Save();
        }

        [Button]
        public void ResetUpgrades()
        {
            foreach (SO_Upgrade upgrade in _upgrades)
            {
                string saveKey = SAVEKEY_UPGRADE_PREFIX + upgrade.ID;
                PlayerPrefs.DeleteKey(saveKey);
                upgrade.Reset();
            }

            PlayerPrefs.Save();
            Debug.Log("[SaveManager] -- Restart game to reset upgrades");
        }

        [Button]
        public void ResetCoins()
        {
            WalletManager.Instance.OnLoad(0);
            WalletManager.Instance.CoinChangeUpdate();

            PlayerPrefs.DeleteKey(SAVEKEY_COIN);
            PlayerPrefs.Save();
        }

        public void OnCoinChange(int coinCount)
        {
            PlayerPrefs.SetInt(SAVEKEY_COIN, coinCount);
            PlayerPrefs.Save();
        }
    }
}