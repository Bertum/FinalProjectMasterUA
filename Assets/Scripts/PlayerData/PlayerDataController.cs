using System;
using UnityEngine;

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
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = PlayerData.Position;
        player.transform.rotation = PlayerData.Rotation;
    }

    public void Save()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerData.Position = player.transform.position;
            PlayerData.Rotation = player.transform.rotation;
        }
        saveLoadService.Save(PlayerData);
    }

    internal void RemoveEventAt(Vector3 position, Quaternion rotation) {
        for(int i = 0; i < PlayerData.MapData.Count; i++) {
            if(PlayerData.MapData[i].Position == position && PlayerData.MapData[i].Rotation == rotation) {
                PlayerData.MapData.Remove(PlayerData.MapData[i]);
                saveLoadService.Save(PlayerData);
            }
        }
    }
}
