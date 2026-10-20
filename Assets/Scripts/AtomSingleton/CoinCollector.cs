using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace AtomSingleton
{
    public class CoinCollector : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private DecalProjector _decalProjector;
        [SerializeField] private float _baseRadius = 0.5f;

        private CancellationTokenSource _ctsUpdateUpgrader;

        private void Start()
        {
            _decalProjector.gameObject.SetActive(false);

            UI_StartMenu.Enter += OnEnter;
            UI_PauseMenu.Exit += OnExit;
        }

        private void OnDestroy()
        {
            UI_StartMenu.Enter -= OnEnter;
            UI_PauseMenu.Exit -= OnExit;
        }

        private void OnEnter()
        {
            _decalProjector.gameObject.SetActive(true);

            _ctsUpdateUpgrader = new();
            UpdateCollectorRadiusAsync(_ctsUpdateUpgrader.Token).Forget();
        }

        private void OnExit()
        {
            _decalProjector.gameObject.SetActive(false);

            StopCts();
        }

        private async UniTaskVoid UpdateCollectorRadiusAsync(CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();

                await UniTask.Delay(System.TimeSpan.FromSeconds(1), cancellationToken: token);
                _collider.radius = _baseRadius * (1 + (Inventory.Instance.RaiseCollectRadius.CurrentLevel));
                _decalProjector.size = new Vector3(_baseRadius * 2, _baseRadius * 2, 5f);
            }
        }

        private void StopCts()
        {
            if (_ctsUpdateUpgrader == null)
                return;

            _ctsUpdateUpgrader.Cancel();
            _ctsUpdateUpgrader.Dispose();
            _ctsUpdateUpgrader = null;
        }
    }
}