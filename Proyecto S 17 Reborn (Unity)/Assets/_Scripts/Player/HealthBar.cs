using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(health);
	}

    public void SetHealth(int health)
	{
		slider.value = health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

	[ContextMenu("Update Color")]
	public void UpdateColor()
    {
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

    private void LateUpdate()
    {
		transform.LookAt(Camera.main.transform);
		transform.Rotate(0, 180, 0);
    }

}
