using UnityEngine;

namespace AtomSingleton
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private CoinSpawner _coinSpawner;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                _coinSpawner.OnCoinCollected();
        }
    }
}