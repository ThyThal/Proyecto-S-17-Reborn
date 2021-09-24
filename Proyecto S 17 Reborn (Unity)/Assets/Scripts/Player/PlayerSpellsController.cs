using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerSpellsController : MonoBehaviour
{
    [SerializeField] private PlayerSpellMeter _playerSpellMeter;

    private StarterAssetsInputs _starterAssetsInputs;
    private ThirdPersonController _thirdPersonController;


    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        if (_playerSpellMeter.CanUseSpell == true)
        {
            if (_starterAssetsInputs.aim == true) // Spell Thast Use Aim
            {
                if (_starterAssetsInputs.useSpellUtility)
                {
                    UseSpellUtility();
                }

                if (_starterAssetsInputs.useSpellOffensive)
                {
                    UseSpellOffensive();
                }
            }

            if (_starterAssetsInputs.useSpellDefensive) // Does not use Aim.
            {
                UseSpellDefensive();
            }
        }


        ResetSpellsInput();
    }

    private void  UseSpellUtility()
    {
        Debug.Log("[Spell] Use Utility");
        _starterAssetsInputs.useSpellUtility = false;
        _playerSpellMeter.Current = 0;
    }
    private void UseSpellDefensive()
    {
        Debug.Log("[Spell] Use Defensive");
        _starterAssetsInputs.useSpellDefensive = false;
        _playerSpellMeter.Current = 0;
    }
    private void  UseSpellOffensive()
    {
        Debug.Log("[Spell] Use Offensive");
        _starterAssetsInputs.useSpellOffensive = false;
        _playerSpellMeter.Current = 0;
    }

    private void ResetSpellsInput()
    {
        _starterAssetsInputs.useSpellDefensive = false;
        _starterAssetsInputs.useSpellOffensive = false;
        _starterAssetsInputs.useSpellUtility = false;
    }
}