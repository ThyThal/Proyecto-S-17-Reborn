using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerSpellsController : MonoBehaviour
{
    [SerializeField] private PlayerSpellMeter _playerSpellMeter;
    [SerializeField] private PlayerShootingController _playerShootingController;

    private StarterAssetsInputs _starterAssetsInputs;
    private ThirdPersonController _thirdPersonController;

    [Header("Spell Prefabs")]
    [SerializeField] private Transform _spellsHolder;
    [SerializeField] private GameObject _spellDefensivePrefab;
    [SerializeField] private GameObject _spellOfensivePrefab;
    [SerializeField] private GameObject _spellUtilityPrefab;


    private void Awake()
    {
        _playerShootingController = GetComponent<PlayerShootingController>();
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

            if (_starterAssetsInputs.useSpellDefensive && _thirdPersonController.Grounded) // Does not use Aim.
            {
                UseSpellDefensive();
            }
        }


        ResetSpellsInput();
    }

    private void  UseSpellUtility()
    {
        //Debug.Log("[Spell] Use Utility!");
        _starterAssetsInputs.useSpellUtility = false;
        _playerShootingController.CurrentBreakableObject.DestroyObject();
        _playerSpellMeter.Current = 0;
    }
    private void UseSpellDefensive()
    {
        //Debug.Log("[Spell] Use Defensive!");
        _starterAssetsInputs.aim = false;
        _starterAssetsInputs.useSpellDefensive = false;
        _starterAssetsInputs.DisablePlayerActions();
        GameObject spellPrefab = Instantiate(_spellDefensivePrefab, _spellsHolder);
        _playerSpellMeter.Current = 0;
    }
    private void  UseSpellOffensive()
    {
        //Debug.Log("[Spell] Use Offensive!");
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