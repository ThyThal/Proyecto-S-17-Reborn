using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private bool _isActivated = false;
    [SerializeField] private ThirdPersonController _player;
    [SerializeField] private BossTurret _turret;
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _checkpoint;

    [SerializeField] private GameObject _bossDefense;
    [SerializeField] private ParticleSystem _gas;

    private Battery _playerBattery;
    private ThirdPersonController _playerController;

    [Header("Damage Player")]
    [SerializeField] private bool activateGas = false;
    [SerializeField] private float damageTimer = 1f;
    [SerializeField] private float originalDamageTimer;
    [SerializeField] private float batteryDamage;

    private void Awake()
    {
        _playerBattery = _player.GetComponent<Battery>();
        _playerController = _player.GetComponent<ThirdPersonController>();
    }

    private void Start()
    {
        originalDamageTimer = damageTimer;
        _gas.Stop();
    }

    public void DeactivateDefense()
    {
        _turret.isWeak = true;
        _bossDefense.SetActive(false);
        _gas.Play();
    }

    public void ActivateDefense()
    {
        _turret.isWeak = false;
        _bossDefense.SetActive(true);
        _gas.Stop();
    }

    public void KilledBoss()
    {
        _door.SetActive(false);
        _checkpoint.SetActive(true);
    }

    public void ActivateBoss()
    {
        _turret.Activate();
    }

    private void Update()
    {
        activateGas = _turret.isWeak;

        if (_turret.isWeak)
        {
            damageTimer -= Time.deltaTime;

            DamagePlayer();
        }
    }

    private void DamagePlayer()
    {
        if (damageTimer <= 0)
        {
            if (_playerController.IsProtected)
            {
                _playerBattery.DamageBattery(batteryDamage/3);
            }

            else
            {
               _playerBattery.DamageBattery(batteryDamage);
            }

            damageTimer = originalDamageTimer;
        }
    }
}
