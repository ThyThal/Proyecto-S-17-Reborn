using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool newLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (newLevel)
            {
                GameManager.Instance.NewLevel();
            }

            else
            {
                GameManager.Instance.Win();
            }
        }
    }
}
