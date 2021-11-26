using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _thirdPersonController;
    private float _batteryAmount = 1000f;
    private int _spellAmount;
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
        SceneManager.LoadScene(3);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Win()
    {
        SceneManager.LoadScene(4);
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadPlayer(ThirdPersonController player)
    {
        _thirdPersonController = player;
    }

    public void LoadPlayerData()
    {
        _thirdPersonController.GetComponent<Battery>().OverrideBattery(_batteryAmount);
        _thirdPersonController.GetComponent<PlayerSpellMeter>().Current = _spellAmount;
    }
}
