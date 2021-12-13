using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattery : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float _maxLife = 100f;
    [SerializeField] private float _currentHealth;
    public float CurrentHealth => _currentHealth;

    // DAMAGE
    public virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        healthBar.SetHealth((int)CurrentHealth);
        if (CurrentHealth <= 0) { Die(); }
    }
    public void Heal(float amount)
    {
        _currentHealth = Mathf.Clamp(amount, 0, _maxLife);
        healthBar.SetHealth((int)CurrentHealth);
    }
    public void Die()
    {
        healthBar.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
