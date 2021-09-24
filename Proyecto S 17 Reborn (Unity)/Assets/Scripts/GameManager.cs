using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

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


}
