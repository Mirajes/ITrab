using System.Collections;
using UnityEngine;

public class SI_Bullet : MonoBehaviour
{
    [SerializeField] private float _shotSpeed = 1f;
    [SerializeField] private float _lifeTime = 1f;

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
            this.transform.position += this.transform.up * _shotSpeed * Time.deltaTime;
            alivedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(this.gameObject);
        yield return null;
    }
}
