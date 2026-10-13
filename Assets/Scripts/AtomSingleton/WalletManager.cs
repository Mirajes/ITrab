using Alchemy.Inspector;
using System;
using UnityEngine;

namespace AtomSingleton
{
    public class WalletManager : Singleton<WalletManager>
    {
        [SerializeField] private int _coinCount = 0;
        public int CoinCount => _coinCount;

        public static event Action<int> CoinChange;

        [Button]
        public void AddCoin(int coinToAdd)
        {
            _coinCount += coinToAdd;
            CoinChange?.Invoke(_coinCount);
        }
        [Button]
        public void RemoveCoin(int coinToRemove)
        {
            _coinCount -= coinToRemove;
            CoinChange?.Invoke(_coinCount);
        }
    }
}