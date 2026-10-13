using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace AtomSingleton
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private Coin _coin;
        [SerializeField] private float _respawnDelay = 15f;

        public void OnCoinCollected()
        {
            _coin.gameObject.SetActive(false);

            WalletManager.Instance.AddCoin(GameContext.Instance.CoinsPerCollect
                + Inventory.Instance.MoreCoinsPerCollect.Count);

            RespawnCoinAsync(this.destroyCancellationToken).Forget();
        }

        private async UniTaskVoid RespawnCoinAsync(CancellationToken token)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(_respawnDelay), cancellationToken: token);
            _coin.gameObject.SetActive(true);
        }
    }
}