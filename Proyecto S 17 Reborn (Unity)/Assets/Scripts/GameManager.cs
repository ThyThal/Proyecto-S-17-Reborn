using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _thirdPersonController;
    public ThirdPersonController Player => _thirdPersonController;

    #region // Singleton Instance
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }
    }
    #endregion // Singleton Instance

    public void GameOver()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Game Over!");
    }

    public void LoadPlayer(ThirdPersonController player)
    {
        _thirdPersonController = player;
    }
}
