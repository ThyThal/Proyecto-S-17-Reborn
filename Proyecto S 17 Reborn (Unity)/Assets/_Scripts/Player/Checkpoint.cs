using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool win;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (win)
            {
                GameManager.Instance.Win();
            }

            else
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}
