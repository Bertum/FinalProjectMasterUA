using System.IO;
using UnityEngine;

public class SaveLoadService
{
    private string filePath = Application.persistentDataPath + "/savegame.json";

    public void Save(PlayerData data)
    {
        string jsonToSave = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, jsonToSave);
    }

    public PlayerData Load()
    {
        PlayerData playerData = new PlayerData();
        if (CheckExistSave())
        {
            var jsonToLoad = File.ReadAllText(filePath);
            playerData = JsonUtility.FromJson<PlayerData>(jsonToLoad);
        }
        else
        {
            Debug.Log("Save game not found");
        }
        return playerData;
    }

    public bool CheckExistSave()
    {
        return File.Exists(filePath);
    }
}
