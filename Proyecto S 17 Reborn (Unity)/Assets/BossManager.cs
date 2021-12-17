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

    public void DeactivateDefense()
    {
        _bossDefense.SetActive(false);
    }

    public void ActivateDefense()
    {
        _bossDefense.SetActive(true);
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

}
