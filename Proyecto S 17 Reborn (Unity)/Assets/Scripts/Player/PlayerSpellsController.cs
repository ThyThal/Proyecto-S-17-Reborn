using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerSpellsController : MonoBehaviour
{
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
            if (_starterAssetsInputs.useSpellUtility)
            {
                UseSpellUtility();
            }

            if (_starterAssetsInputs.useSpellDefensive)
            {
                UseSpellDefensive();
            }

            if (_starterAssetsInputs.useSpellOffensive)
            {
                UseSpellOffensive();
            }
        }
    }

    private void  UseSpellUtility()
    {
        Debug.Log("[Spell] Use Utility");
        _starterAssetsInputs.useSpellUtility = false;
    }
    private void UseSpellDefensive()
    {
        Debug.Log("[Spell] Use Defensive");
        _starterAssetsInputs.useSpellDefensive = false;
    }
    private void  UseSpellOffensive()
    {
        Debug.Log("[Spell] Use Offensive");
        _starterAssetsInputs.useSpellOffensive = false;
    }
}
