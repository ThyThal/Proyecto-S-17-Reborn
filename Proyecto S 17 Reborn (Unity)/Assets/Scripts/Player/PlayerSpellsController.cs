using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;

public class PlayerSpellsController : MonoBehaviour
{
    [SerializeField] private PlayerSpellMeter _playerSpellMeter;
    [SerializeField] private PlayerShootingController _playerShootingController;
    [SerializeField] private Animator _animator;

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
    public bool DefensiveSpellReady() => _defensiveSpellCooldown <= 0 ? true : false;
    public bool OffensiveSpellReady() => _offensiveSpellCooldown <= 0 ? true : false;


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

                if (_starterAssetsInputs.useSpellOffensive && OffensiveSpellReady())
                {
                    UseSpellOffensive();
                }
            }

            if (_starterAssetsInputs.useSpellDefensive && _thirdPersonController.Grounded && DefensiveSpellReady()) // Does not use Aim.
            {
                UseSpellDefensive();
            }
        }


        ResetSpellsInput();
    }

    private void  UseSpellUtility()
    {
        //Debug.Log("[Spell] Use Utility!");
        

        if (_playerShootingController.CurrentBreakableObject != null)
        {
            transform.LookAt(_playerShootingController.AimingPoint);
            _starterAssetsInputs.StartedCastingSpell();
            _starterAssetsInputs.useSpellUtility = false;
            _playerShootingController.CurrentBreakableObject.DestroyObject();
            _playerSpellMeter.Current = 0;
            _utilitySpellCooldown = _utilitySpellOriginalCooldown;
            _utilityFillUI.StartCooldown();
            _animator.SetTrigger("ActivateSingle");

        }

        else if (_playerShootingController.CurrentEnemy != null)
        {
            transform.LookAt(_playerShootingController.AimingPoint);
            _starterAssetsInputs.StartedCastingSpell();
            _starterAssetsInputs.useSpellUtility = false;
            _playerShootingController.CurrentEnemy.TakeDamage(20);
            _playerSpellMeter.Current = 0;
            _utilitySpellCooldown = _utilitySpellOriginalCooldown;
            _utilityFillUI.StartCooldown();
            _animator.SetTrigger("ActivateSingle");
        }
    }
    private void UseSpellDefensive()
    {
        //Debug.Log("[Spell] Use Defensive!");
        transform.LookAt(_playerShootingController.AimingPoint);
        _starterAssetsInputs.aim = false;
        _starterAssetsInputs.useSpellDefensive = false;
        _starterAssetsInputs.StartedCastingSpell();
        GameObject spellPrefab = Instantiate(_defensiveSpellPrefab, _spellsHolder);
        _playerSpellMeter.Current = 0;
        _defensiveSpellCooldown = _defensiveSpellOriginalCooldown;
        _defensiveFillUI.StartCooldown();
        _animator.SetTrigger("ActivateShield");
    }
    private void UseSpellOffensive()
    {
        transform.LookAt(_playerShootingController.AimingPoint);
        //Debug.Log("[Spell] Use Offensive!");
        _starterAssetsInputs.useSpellOffensive = false;
        _playerSpellMeter.Current = 0;
        _offensiveSpellCooldown = _offensiveSpellOriginalCooldown;
        _starterAssetsInputs.StartedCastingSpell();
        _offensiveFillUI.StartCooldown();
        GameObject spellPrefab = Instantiate(_offensiveSpellPrefab, _playerShootingController.AimingPoint);
        spellPrefab.transform.position = _playerShootingController.AimingPoint.transform.position;
        _animator.SetTrigger("ActivateArea");
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
            float currentOffset = _defensiveSpellCooldown - 0;
            float maximumOffset = _defensiveSpellOriginalCooldown - 0;
            float fillAmount = currentOffset / maximumOffset;
            _defensiveFillUI.SetCooldown(fillAmount);
        }

        if (_offensiveSpellCooldown > 0)
        {
            _offensiveSpellCooldown -= Time.deltaTime;
            float currentOffset = _offensiveSpellCooldown - 0;
            float maximumOffset = _offensiveSpellOriginalCooldown - 0;
            float fillAmount = currentOffset / maximumOffset;
            _offensiveFillUI.SetCooldown(fillAmount);
        }
    }

    private void UpdateSpellsUI()
    {

    }
}