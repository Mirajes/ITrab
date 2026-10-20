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

        }

        private void OnExit()
        {

        }

        private async UniTaskVoid UpdateCollectorRadiusAsync(CancellationToken token)
        {
            
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