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
        var borderPrefab1 = Resources.Load("Prefabs/Procedural/Border1") as GameObject;
        var borderPrefab2 = Resources.Load("Prefabs/Procedural/Border2") as GameObject;
        var borderPrefab3 = Resources.Load("Prefabs/Procedural/Border3") as GameObject;
        var borderPrefab4 = Resources.Load("Prefabs/Procedural/Border4") as GameObject;
        var battleEventHolderPrefab = Resources.Load("Prefabs/Procedural/BattleEventHolder") as GameObject;
        var wreckageEventHolderPrefab = Resources.Load("Prefabs/Procedural/WreckageEventHolder") as GameObject;
        prefabsToLoad.Add("OceanTile", seaPrefab);
        prefabsToLoad.Add("SandTile", sandPrefab);
        prefabsToLoad.Add("BottomDecoration1", decorationPrefab);
        prefabsToLoad.Add("Island1", island1Prefab);
        prefabsToLoad.Add("Island2", island2Prefab);
        prefabsToLoad.Add("Island3", island3Prefab);
        prefabsToLoad.Add("PlayerShip", playerShipPrefab);
        prefabsToLoad.Add("Border1", borderPrefab1);
        prefabsToLoad.Add("Border2", borderPrefab2);
        prefabsToLoad.Add("Border3", borderPrefab3);
        prefabsToLoad.Add("Border4", borderPrefab4);
        prefabsToLoad.Add("BattleEventHolder", battleEventHolderPrefab);
        prefabsToLoad.Add("WreckageEventHolder", wreckageEventHolderPrefab);
    }
}
