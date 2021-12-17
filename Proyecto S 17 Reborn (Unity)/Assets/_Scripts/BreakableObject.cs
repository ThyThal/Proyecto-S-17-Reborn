using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _selectedMaterial;
    [SerializeField] private BossBattery bossBattery;
    [SerializeField] private bool isBattery = false;

    private bool _isSelected = false;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultMaterial = _meshRenderer.material;
    }

    public void SelectObject()
    {
        if(_isSelected == false)
        {
            _meshRenderer.material = _selectedMaterial;
            _isSelected = true;
        }
    }

    public void UnselectObject()
    {
        if (_isSelected == true && _meshRenderer != null)
        {
            _meshRenderer.material = _defaultMaterial;
            _isSelected = false;
        }
    }

    public void DestroyObject()
    {
        if (isBattery == true)
        {
            bossBattery.Die();
        }

        Destroy(this.gameObject);
    }
}
