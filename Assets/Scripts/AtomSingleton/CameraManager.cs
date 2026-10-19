using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace AtomSingleton
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _target;
        [SerializeField] private float _duration;

        public Camera Camera => _camera;

        public void MoveToTarget(Transform target, Action onComplete = null)
        {
            _target = target;
            MoveToTargetAsync(onComplete).Forget();
        }

        private async UniTaskVoid MoveToTargetAsync(Action onComplete)
        {
            if (_target == null) return;

            // ВАЖНО: Двигаем именно _camera.transform, а не transform менеджера!
            Transform cameraTransform = _camera.transform;

            Vector3 startPosition = cameraTransform.position;
            Quaternion startRotation = cameraTransform.rotation;
            float elapsedTime = 0f;

            var cancelToken = this.GetCancellationTokenOnDestroy();
            while (elapsedTime < _duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / _duration;

                cameraTransform.position = Vector3.Lerp(startPosition, _target.position, t);
                cameraTransform.rotation = Quaternion.Lerp(startRotation, _target.rotation, t);

                await UniTask.Yield(PlayerLoopTiming.Update, cancelToken);
            }

            cameraTransform.position = _target.position;
            cameraTransform.rotation = _target.rotation;

            // Вызываем колбэк (привязку к родителю), если он был передан
            onComplete?.Invoke();
        }
    }
}