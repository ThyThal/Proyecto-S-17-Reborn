using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [Header("Battery Information")]
    [SerializeField] private float _maximumBattery = 1000f;
    [SerializeField] private float _currentBattery = 0f;
    [SerializeField] private float _usageBattery = 0.1f;
    [SerializeField] private float _usageMultiplier = 1f;

    [Header("Helpers")]
    [SerializeField] private PlayerSpellMeter _batteryMeter;

    public float MaximumBattery => _maximumBattery;
    public float CurrentBattery => _currentBattery;
    private void Start()
    {
        _currentBattery = _maximumBattery;
    }

    private void Update()
    {
        if (_currentBattery > 0)
        {
            var batteryUsage = _usageBattery * _usageMultiplier;
            _currentBattery -= batteryUsage;

            UpdateFill();

        }
    }

    private void UpdateFill()
    {
        _batteryMeter.Current = (int)_currentBattery / 10;
    }

    public void DamageBattery(float amount)
    {
        _currentBattery -= amount;
        UpdateFill();
    }
}
