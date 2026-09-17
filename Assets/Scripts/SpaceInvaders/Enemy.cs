using UnityEngine;

public class Enemy : MonoBehaviour, SI_IDamagable
{
    public int CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = value;
        }
    }
    [SerializeField] private int _maxHealth = 1;
    private int _currentHealth;

    public void Damage(int damage)
    {
        CurrentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // invoke give Score
        Destroy(this.gameObject);
    }
}