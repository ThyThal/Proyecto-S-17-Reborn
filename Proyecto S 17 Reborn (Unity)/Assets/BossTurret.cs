using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class BossTurret : MonoBehaviour
{
    [SerializeField] private GameObject _turretCannon;
    [SerializeField] private ThirdPersonController _player;

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
        //_turretCannon.transform.Look

        var lookPos = _player.transform.position - _turretCannon.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        _turretCannon.transform.rotation = Quaternion.Slerp(_turretCannon.transform.rotation, rotation, Time.deltaTime * 1f);
    }
}
