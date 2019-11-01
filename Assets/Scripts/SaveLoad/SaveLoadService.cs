using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadService
{
    private readonly string filePath = Application.persistentDataPath + "/savegame.json";
    private Dictionary<string, GameObject> prefabsToLoad;

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
            GetPrefabs();
            foreach (var mapData in playerData.MapData)
            {
                GameObject.Instantiate(prefabsToLoad[mapData.PrefabName], mapData.Position, mapData.Rotation);
            }
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

    private void GetPrefabs()
    {
        prefabsToLoad = new Dictionary<string, GameObject>();
        var seaPrefab = Resources.Load("Prefabs/Procedural/OceanTile") as GameObject;
        var sandPrefab = Resources.Load("Prefabs/Procedural/SandTile") as GameObject;
        var decorationPrefab = Resources.Load("Prefabs/Procedural/BottomDecoration1") as GameObject;
        var island1Prefab = Resources.Load("Prefabs/Procedural/Island1") as GameObject;
        var island2Prefab = Resources.Load("Prefabs/Procedural/Island2") as GameObject;
        var island3Prefab = Resources.Load("Prefabs/Procedural/Island1") as GameObject;
        var playerShipPrefab = Resources.Load("Prefabs/Procedural/PlayerShip") as GameObject;
        var borderPrefab = Resources.Load("Prefabs/Procedural/Border") as GameObject;
        prefabsToLoad.Add("OceanTile", seaPrefab);
        prefabsToLoad.Add("SandTile", sandPrefab);
        prefabsToLoad.Add("BottomDecoration1", decorationPrefab);
        prefabsToLoad.Add("Island1", island1Prefab);
        prefabsToLoad.Add("Island2", island2Prefab);
        prefabsToLoad.Add("Island3", island3Prefab);
        prefabsToLoad.Add("PlayerShip", playerShipPrefab);
        prefabsToLoad.Add("Border", borderPrefab);
    }
}
