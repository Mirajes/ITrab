using UnityEngine;

public class Enemy : MonoBehaviour, SI_IDamagable
{
    [SerializeField] private int _maxHealth = 1;
    [SerializeField] private float _stepSize = 0.3f;
    private int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = value;
        }
    }
    [SerializeField] private int _scoreToGive = 1;

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
        this.gameObject.SetActive(false);
        SI_GameManager.ChangeScore?.Invoke(_scoreToGive);
    }
}