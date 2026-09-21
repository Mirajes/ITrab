using UnityEngine;

namespace SI
{
    public abstract class A_Bullet : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyMask;
        [SerializeField] private float _lifeDistance = 12f;
        [SerializeField] private float _moveDistance = 0.3f;
        [SerializeField] private float _bulletSize = 0.1f;
        [SerializeField] private int _damage = 1;
        [SerializeField] protected MoveDirection _moveDirection;
        private float _alivedDistance;

        //[SerializeField] private float _lifeTime = 3f;
        //[SerializeField] private float _shotSpeed = 5f;
        //private Coroutine _lifeRoutine;

        public virtual void Init()
        {
            GameManager.Step += OnStep;

            //_lifeRoutine = StartCoroutine(LifeTimeRoutine());
        }

        private void OnDestroy()
        {
            GameManager.Step -= OnStep;

            //StopCoroutine(_lifeRoutine);
        }

        protected enum MoveDirection
        {
            Up = 1,
            Down = -1,
        }

        private void OnStep()
        {
            Vector2 nextPos = new Vector2(
                transform.position.x,
                transform.position.y + _moveDistance * (int)_moveDirection
                );

            _alivedDistance += Mathf.Abs(transform.position.y - nextPos.y);

            transform.position = nextPos;
            Debug.Log("I MOVING");
            CheckHit();

            if (_alivedDistance >= _lifeDistance)
                Destroy(this.gameObject);
        }

        private void CheckHit()
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position,
                new Vector2(_bulletSize, _bulletSize),
                0f,
                transform.up,
                0f,
                _enemyMask
                );


            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent<IDamagable>(out IDamagable IDamagable))
                {
                    IDamagable.Damage(_damage);
                    Destroy(this.gameObject);
                }
            }

        }

        //private IEnumerator LifeTimeRoutine()
        //{
        //    float alivedTime = 0f;

        //    while (alivedTime < _lifeTime)
        //    {
        //        CheckHit();

        //        _moveDistance = _shotSpeed * Time.deltaTime;
        //        this.transform.position += this.transform.up * _moveDistance;
        //        alivedTime += Time.deltaTime;

        //        yield return null;
        //    }

        //    Destroy(this.gameObject);
        //    yield return null;
        //}
    }

}



