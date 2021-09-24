using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _normalCamera;
    [SerializeField] private CinemachineVirtualCamera _aimingCamera;

    [SerializeField] private float _normalSensitivity = 1f;
    [SerializeField] private float _aimingSensitivity = 0.3f;

    private StarterAssetsInputs _starterAssetsInputs;
    private ThirdPersonController _thirdPersonController;

    [Header("Spell Controller")]
    [SerializeField] private PlayerSpellMeter _playerSpellMeter;
    [SerializeField] private int _spellRecoverMinimum = 20;
    [SerializeField] private int _spellRecoverMaximum = 40;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        if (_starterAssetsInputs.attacking)
        {
            Attack();
        }

        if (_starterAssetsInputs.aim)
        {
            _aimingCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(_aimingSensitivity);
        }

        else
        {
            _aimingCamera.gameObject.SetActive(false);
            _thirdPersonController.SetSensitivity(_normalSensitivity);
        }
    }

    private void Attack()
    {
        //Debug.Log("[Attack] Use Attack");
        int spellRecoverAmount = Random.Range(_spellRecoverMinimum, _spellRecoverMaximum);
        _playerSpellMeter.Current += spellRecoverAmount;
        //Debug.Log(spellRecoverAmount);

        _starterAssetsInputs.attacking = false;
    }
}
