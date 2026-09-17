using System.Collections;
using UnityEngine;

public class SI_Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private float _shotSpeed = 5f;
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] private float _bulletSize = 0.15f;
    [SerializeField] private int _damage = 1;
    private float _moveDistance;

    private Coroutine _lifeRoutine;

    private void OnDestroy()
    {
        StopCoroutine(_lifeRoutine);
    }

    public void Init()
    {
        _lifeRoutine = StartCoroutine(LifeTimeRoutine());
    }

    private IEnumerator LifeTimeRoutine()
    {
        float alivedTime = 0f;

        while (alivedTime < _lifeTime)
        {
            CheckHit();

            _moveDistance = _shotSpeed * Time.deltaTime;
            this.transform.position += this.transform.up * _moveDistance;
            alivedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(this.gameObject);
        yield return null;
    }

    private void CheckHit()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position,
            new Vector2(_bulletSize, _bulletSize),
            0f,
            transform.up,
            _moveDistance,
            _enemyMask
            );

        if (hit.collider == null)
        {
            Debug.Log("no");
        }
        else
        {
            Debug.Log("hit");
            if (hit.collider.TryGetComponent<SI_IDamagable>(out SI_IDamagable IDamagable))
            {
                IDamagable.Damage(_damage);
            }
        }

    }
}
