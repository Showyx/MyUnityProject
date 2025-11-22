using System;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _healthRegen;

    public Action OnDeath;
    public Action<float, float> OnHealthChanged;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
}
