using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recharge : MonoBehaviour
{
    [SerializeField] private PlayerShootingController _player;
    [SerializeField] private Material _usedColor;
    [SerializeField] private Material _normalColor;

    private StarterAssetsInputs _playerInputs;
    [SerializeField] private bool _used = false;


    private void OnTriggerEnter(Collider other)
    {
        if (_used == false)
        {
            if (other.CompareTag("Player") && _player == null)
            {
                _player = other.GetComponent<PlayerShootingController>();
                _playerInputs = _player.GetComponent<StarterAssetsInputs>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_used == false)
        {
            if (other.CompareTag("Player") && _player != null)
            {
                _player = null;
            }
        }
    }

    private void Update()
    {
        if (_used == false && _player != null)
        {
            if (_playerInputs.attacking)
            {
                Debug.Log("Used");
                Use();
            }

            else
            {
                Debug.Log("Waiting...");
            }
        }
    }

    [ContextMenu("Heal Player")]
    private void Use()
    {
        if (_player != null && _used == false)
        {
            Battery battery = _player.GetComponent<Battery>();
            battery.HealBattery(battery.MaximumBattery / 4);
            var renderer = GetComponent<MeshRenderer>();

            Material[] matArray = renderer.materials;
            matArray[1] = _usedColor;
            renderer.materials = matArray;

            _used = true;
            this.enabled = false;
        }


    }
}
