using System;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [field: SerializeField] public PlayerFarmData PlayerFarmData { get; private set; }
    [field: SerializeField] public PlayerCommon PlayerCommon { get; private set; }
    public Action OnGameFocus = default;
    private bool _isGamePause = false;

    public static PlayerDataManager Instance = default;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
    
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveData();
            _isGamePause = true;
        }
        else
        {
            if (_isGamePause)
            {
                _isGamePause = false;
                OnGameFocus?.Invoke();
            }
        }
    }
    
    private void LoadData()
    {
        PlayerCommon.Load();
        PlayerFarmData.Load();
    }

    public void SaveData()
    {
        PlayerFarmData.Save();
        PlayerCommon.Save();
    }
}
