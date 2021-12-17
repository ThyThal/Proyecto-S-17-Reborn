using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool newLevel;
    [SerializeField] private int levelIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (newLevel == true)
            {               
                GameManager.Instance.NewLevel(levelIndex);
            }

            else
            {
                GameManager.Instance.Win();
            }
        }
    }
}
