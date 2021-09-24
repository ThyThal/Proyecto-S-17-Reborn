using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class SpellShield : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _thirdPersonController;
    [SerializeField] private KeyCode _spellCancel = KeyCode.Space;
    [SerializeField] private bool isProtected = false;

    private void Awake()
    {
        _thirdPersonController = GameManager.Instance.Player;
    }

    private void Start()
    {
        CreateShield();
        ExplodeShield();        
    }

    private void Update()
    {
        if (_thirdPersonController.PlayerInputs.cancelSpell == true)
        {
            _thirdPersonController.PlayerInputs.cancelSpell = false;
            ExplodeShield();
            //_starterAssetsInputs.EnablePlayerActions();
        }
    }
    
    private void CreateShield()
    {
        isProtected = true;
    }

    private void ExplodeShield()
    {
        if (isProtected)
        {           
            Destroy(this.gameObject, 5f);
        }
    }

    private void OnDestroy()
    {
        _thirdPersonController.PlayerInputs.EnablePlayerActions();
    }
}
