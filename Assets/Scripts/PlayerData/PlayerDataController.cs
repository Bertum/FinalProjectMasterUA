﻿using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    private SaveLoadService saveLoadService;
    [HideInInspector]
    public PlayerData PlayerData;
    private UIController uiController;

    private void Awake()
    {
        PlayerData = new PlayerData();
        saveLoadService = new SaveLoadService();
        uiController = FindObjectOfType<UIController>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void Load()
    {
        PlayerData = saveLoadService.Load();
        uiController.ResourcesChanged(PlayerData);
    }

    public void Save()
    {
        var playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerData.Position = playerTransform.position;
        PlayerData.Rotation = playerTransform.rotation;

        saveLoadService.Save(PlayerData);
    }
}
