using UnityEngine;

namespace SI
{
    public class VFXManager : MonoBehaviour
    {
        [SerializeField] private Transform _vfxObj;
        [SerializeField] private Explosion _explosionPrefab;

        private void OnEnable()
        {
            Enemy.DieVFX += OnEnemyExplode;
        }

        private void OnDisable()
        {
            Enemy.DieVFX -= OnEnemyExplode;
        }

        private void OnEnemyExplode(Vector3 position, Color color)
        {
            Explosion newExplode = Instantiate(_explosionPrefab, position, Quaternion.identity, _vfxObj);
            newExplode.Init(color);
        }
    }
}