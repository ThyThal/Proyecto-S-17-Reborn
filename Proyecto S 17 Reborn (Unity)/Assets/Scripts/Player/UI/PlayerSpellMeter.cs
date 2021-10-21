using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpellMeter : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] [Range(0, 100)] private int _minimum;
    [SerializeField] [Range(0, 100)] private int _maximum;
    [SerializeField] [Range(0, 100)] private int _current;

    [Header("Components")]
    [SerializeField] private Image _mask;
    [SerializeField] private Slider _slider;
    [SerializeField] private Color _color;

    public int Current
    {
        get { return _current; }
        set
        {
            _current = Mathf.Clamp(value, _minimum, _maximum);
        }
    }
    public int Maximum { get { return _maximum; } }
    public bool CanUseSpell { get { return _current >= _maximum; } }

    private void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float currentOffset = _current - _minimum;
        float maximumOffset = _maximum - _minimum;
        float fillAmount = currentOffset / maximumOffset;
        _slider.value = fillAmount;
    }
}
