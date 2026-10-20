using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace AtomSingleton
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _coin;
        [SerializeField] private float _respawnDelay = 15f;

        private void OnCoinCollected()
        {
            _coin.gameObject.SetActive(false);

            WalletManager.Instance.AddCoin(GameContext.Instance.CoinsPerCollect
                + Inventory.Instance.MoreCoinPerCollect.CurrentLevel);

            RespawnCoinAsync(this.destroyCancellationToken).Forget();
        }

        private async UniTaskVoid RespawnCoinAsync(CancellationToken token)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(_respawnDelay - Inventory.Instance.ReduceCoinSpawnDelay.CurrentLevel), cancellationToken: token); // -1 sec per level
            _coin.gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CoinCollector"))
                OnCoinCollected();
        }
    }
}