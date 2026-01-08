using System;
using UnityEngine;


public class EntityHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    private float _currentHealth;
    [SerializeField] private float _healthRegen;

    public Action OnDeath;
    public Action<float, float> OnHealthChanged;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        InvokeRepeating(nameof(HandleHealthRegen), 1f, 1f);
    }

    public void LoseHealth(float healthLost)
    {
        _currentHealth -= healthLost;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    private void HandleHealthRegen()
    {
        _currentHealth = Mathf.Clamp(_currentHealth + _maxHealth * _healthRegen, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void Death()
    {
        OnDeath?.Invoke();
    }
}
