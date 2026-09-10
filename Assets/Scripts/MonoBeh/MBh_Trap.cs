using UnityEngine;

public class MBh_Trap : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.Damage(_damage);
        }
        
    }
}
