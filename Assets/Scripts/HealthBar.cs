using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _hpBarFill;
    [SerializeField] private EntityHealth _playerHealth;

    private void OnEnable()
    {
        _playerHealth.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        _hpBarFill.fillAmount = currentHealth / maxHealth;
    }
}
