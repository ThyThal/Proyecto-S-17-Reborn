using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemy : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float _maxLife = 100f;
    [SerializeField] private float _currentHealth;

    [Header("Patrol Waypoints")]
    [SerializeField] private Transform _currentWaypoint;
    [SerializeField] private List<Transform> _patrolWaypoints;
    private int currentIndexWaypoint = 0;
    public float CurrentHealth => _currentHealth;

    private void Start()
    {
        _currentWaypoint = _patrolWaypoints[0];
    }











    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            currentIndexWaypoint++;
            _currentWaypoint = _patrolWaypoints[currentIndexWaypoint];
        }
    }

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
