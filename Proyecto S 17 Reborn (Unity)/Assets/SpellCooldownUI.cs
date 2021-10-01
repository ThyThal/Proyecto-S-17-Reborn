using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldownUI : MonoBehaviour
{
    [SerializeField] private GameObject _cooldownBlocker;
    [SerializeField] private Image _fill;


    public void StartCooldown()
    {
        _cooldownBlocker.SetActive(true);
    }

    public void StopCooldown()
    {
        _cooldownBlocker.SetActive(false);
    }

    public void SetCooldown(float amount)
    {
        if (amount > 0)
        {
            _fill.fillAmount = amount;
        }

        else
        {
            StopCooldown();
        }

    }
}
