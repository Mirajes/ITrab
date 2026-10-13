using UnityEngine;

namespace AtomSingleton
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                WalletManager.Instance.AddCoin(GameContext.Instance.CoinsPerCollect
                    + Inventory.Instance.MoreCoinsPerCollect.Count);
            }
        }
    }
}