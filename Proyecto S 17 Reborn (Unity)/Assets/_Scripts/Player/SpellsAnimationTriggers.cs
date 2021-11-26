using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class SpellsAnimationTriggers : MonoBehaviour
{
    [SerializeField] private PlayerShootingController playerShooting;
    [SerializeField] private StarterAssetsInputs assetsInputs;
    public void OnSpellFinished()
    {
        assetsInputs.FinishedCastingSpell();
    }
}
