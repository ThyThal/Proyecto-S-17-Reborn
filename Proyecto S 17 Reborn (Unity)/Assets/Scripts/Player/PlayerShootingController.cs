using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using StarterAssets;
using IndieMarc.EnemyVision;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private GameObject _currentAimObject;
    [SerializeField] private CinemachineVirtualCamera _normalCamera;
    [SerializeField] private CinemachineVirtualCamera _aimingCamera;
    [SerializeField] private LayerMask _aimingLayers;
    [SerializeField] private int _breakableLayers;
    [SerializeField] private int _enemyLayers;

    [SerializeField] private GameObject _aimingPoint;

    [SerializeField] private float _normalSensitivity = 1f;
    [SerializeField] private float _aimingSensitivity = 0.3f;

    private StarterAssetsInputs _starterAssetsInputs;
    private ThirdPersonController _thirdPersonController;
    [SerializeField] private BreakableObject _currentBreakableObject;
    [SerializeField] private Enemy _currentEnemy;

    [Header("Spell Controller")]
    [SerializeField] private PlayerSpellMeter _playerSpellMeter;
    [SerializeField] private int _spellRecoverMinimum = 20;
    [SerializeField] private int _spellRecoverMaximum = 40;

    public BreakableObject CurrentBreakableObject => _currentBreakableObject;
    public Enemy CurrentEnemy => _currentEnemy;
    public Transform AimingPoint => _aimingPoint.transform;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _breakableLayers = LayerMask.NameToLayer("Breakable Object");
        _enemyLayers = LayerMask.NameToLayer("Enemy");

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
            if (_currentBreakableObject != null) 
            {
                _currentBreakableObject.UnselectObject();
                _currentBreakableObject = null;
            }
           
        }

        Vector2 screenCenterPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000f, _aimingLayers))
        {
            _currentAimObject = raycastHit.transform.gameObject;
            _aimingPoint.transform.position = raycastHit.point;
            if (_starterAssetsInputs.aim == true)
            {
                if (_currentAimObject.layer == _breakableLayers)
                {
                    var currentSelectedObject = _currentAimObject.GetComponent<BreakableObject>();

                    if (_currentBreakableObject == null)
                    {
                        currentSelectedObject.SelectObject();
                        _currentBreakableObject = currentSelectedObject;
                    }

                    else
                    {
                        if (currentSelectedObject != _currentBreakableObject)
                        {
                            _currentBreakableObject.UnselectObject();
                            currentSelectedObject.SelectObject(); // Highlight.
                            _currentBreakableObject = currentSelectedObject;
                        }
                    }
                }
                else
                {
                    _currentBreakableObject?.UnselectObject();
                    _currentBreakableObject = null;
                }

                if (_currentAimObject.layer == _enemyLayers)
                {
                    var currentSelectedObject = _currentAimObject.GetComponent<Enemy>();
                    _currentEnemy = currentSelectedObject;
                }

                else
                {
                    _currentEnemy = null;
                }
            }
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
