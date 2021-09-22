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

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        if (_starterAssetsInputs.aim)
        {
            _aimingCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(_aimingSensitivity);

            if (_starterAssetsInputs.attacking)
            {

            }
        }

        else
        {
            _aimingCamera.gameObject.SetActive(false);
            _thirdPersonController.SetSensitivity(_normalSensitivity);
        }
    }

    private void Attack()
    {
        Debug.Log("[Attack] Use Attack");
    }
}
