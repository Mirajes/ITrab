using UnityEngine;

namespace SI
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        public void Init(Color color)
        {
            _renderer.color = color;
        }

        public void SelfDestroy()
        {
            Destroy(this.gameObject);
            // Если в будущем добавите пул: ObjectPool.Release(gameObject);
        }
    }
}