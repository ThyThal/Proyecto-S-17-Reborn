using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class BossTurret : MonoBehaviour
{
    [SerializeField] private GameObject _turretCannon;
    [SerializeField] private ThirdPersonController _player;
    [SerializeField] private BossManager _bossManager;

    [Header("Health")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float _maxLife = 100f;
    [SerializeField] private float _currentHealth;
    public float CurrentHealth => _currentHealth;

    public bool isWeak = false;

    // DAMAGE
    public virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        healthBar.SetHealth((int)CurrentHealth);

        if (CurrentHealth <= 0) { Die(); }

        else
        {
            _bossManager.ActivateDefense();
        }
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
        _bossManager.KilledBoss();
    }


    public void SetPlayer(ThirdPersonController player)
    {
        _player = player;
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        var lookPos = _player.transform.position - _turretCannon.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        _turretCannon.transform.rotation = Quaternion.Slerp(_turretCannon.transform.rotation, rotation, Time.deltaTime * 1f);
    }

    public void Activate()
    {

    }
}
