using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;

public class PlayerSpellsController : MonoBehaviour
{
    [SerializeField] private PlayerSpellMeter _playerSpellMeter;
    [SerializeField] private PlayerShootingController _playerShootingController;

    private StarterAssetsInputs _starterAssetsInputs;
    private ThirdPersonController _thirdPersonController;

    [Header("Spell Prefabs")]
    [SerializeField] private Transform _spellsHolder;

    [Header("Utility Spell")]
    [SerializeField] private GameObject _utilitySpellPrefab;
    [SerializeField] private SpellCooldownUI _utilityFillUI;
    [SerializeField] private float _utilitySpellCooldown = 10f;
    private float _utilitySpellOriginalCooldown;

    [Header("Defensive Spell")]
    [SerializeField] private GameObject _defensiveSpellPrefab;
    [SerializeField] private SpellCooldownUI _defensiveFillUI;
    [SerializeField] private float _defensiveSpellCooldown = 10f;
    private float _defensiveSpellOriginalCooldown;

    [Header("Offensive Spell")]
    [SerializeField] private GameObject _offensiveSpellPrefab;
    [SerializeField] private SpellCooldownUI _offensiveFillUI;
    [SerializeField] private float _offensiveSpellCooldown = 10f;
    private float _offensiveSpellOriginalCooldown;

    public bool UtilitySpellReady() => _utilitySpellCooldown <= 0 ? true : false;


    private void Awake()
    {
        _playerShootingController = GetComponent<PlayerShootingController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Start()
    {
        _utilitySpellOriginalCooldown = _utilitySpellCooldown;
        _defensiveSpellOriginalCooldown = _defensiveSpellCooldown;
        _offensiveSpellOriginalCooldown = _offensiveSpellCooldown;

        _utilitySpellCooldown = 0;
        _defensiveSpellCooldown = 0;
        _offensiveSpellCooldown = 0;
    }

    private void Update()
    {
        CheckSpellsCooldown();
        UpdateSpellsUI();

        if (_playerSpellMeter.CanUseSpell == true)
        {
            if (_starterAssetsInputs.aim == true) // Spell Thast Use Aim
            {
                if (_starterAssetsInputs.useSpellUtility && UtilitySpellReady())
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
        _utilitySpellCooldown = _utilitySpellOriginalCooldown;
        _utilityFillUI.StartCooldown();
    }
    private void UseSpellDefensive()
    {
        //Debug.Log("[Spell] Use Defensive!");
        _starterAssetsInputs.aim = false;
        _starterAssetsInputs.useSpellDefensive = false;
        _starterAssetsInputs.DisablePlayerActions();
        GameObject spellPrefab = Instantiate(_defensiveSpellPrefab, _spellsHolder);
        _playerSpellMeter.Current = 0;
        _defensiveSpellCooldown = _defensiveSpellOriginalCooldown;
        _defensiveFillUI.StartCooldown();
    }
    private void  UseSpellOffensive()
    {
        //Debug.Log("[Spell] Use Offensive!");
        _starterAssetsInputs.useSpellOffensive = false;
        _playerSpellMeter.Current = 0;
        _offensiveSpellCooldown = _offensiveSpellOriginalCooldown;
        _offensiveFillUI.StartCooldown();
    }

    private void ResetSpellsInput()
    {
        _starterAssetsInputs.useSpellDefensive = false;
        _starterAssetsInputs.useSpellOffensive = false;
        _starterAssetsInputs.useSpellUtility = false;
    }

    private void CheckSpellsCooldown()
    {
        if (_utilitySpellCooldown > 0)
        {
            _utilitySpellCooldown -= Time.deltaTime;
            float currentOffset = _utilitySpellCooldown - 0;
            float maximumOffset = _utilitySpellOriginalCooldown - 0;
            float fillAmount = currentOffset / maximumOffset;
            _utilityFillUI.SetCooldown(fillAmount);
        }

        if (_defensiveSpellCooldown > 0)
        {
            _defensiveSpellCooldown -= Time.deltaTime;
            float currentOffset = _utilitySpellCooldown - 0;
            float maximumOffset = _utilitySpellOriginalCooldown - 0;
            float fillAmount = currentOffset / maximumOffset;
            _defensiveFillUI.SetCooldown(fillAmount);
        }

        if (_offensiveSpellCooldown > 0)
        {
            _defensiveSpellCooldown -= Time.deltaTime;
            float currentOffset = _utilitySpellCooldown - 0;
            float maximumOffset = _utilitySpellOriginalCooldown - 0;
            float fillAmount = currentOffset / maximumOffset;
            _offensiveFillUI.SetCooldown(fillAmount);
        }
    }

    private void UpdateSpellsUI()
    {

    }
}