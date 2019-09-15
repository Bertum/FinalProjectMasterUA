using System.Collections.Generic;
using UnityEngine;

public class ProceduralOpenWorld : MonoBehaviour
{

    public static int BOTTOM_Y = 0;
    public static int SURFACE_Y = 3;
    public static float GRID_UPPER_LIMIT = 2900f;
    public static float GRID_BOTTOM_LIMIT = 100f;

    private GameObject seaWrapper;
    private GameObject bottomWrapper;
    private GameObject mapWrapper;
    private GameObject eventsWrapper;
    private GameObject seaPrefab;
    private GameObject sandPrefab;
    private GameObject islandPrefab;
    private GameObject decorationPrefab;
    private GameObject playerShipPrefab;

    private List<Island> islandArray;
    private PlayerDataController playerDataController;
    private SaveLoadService saveLoadService;

    // Start is called before the first frame update
    void Start()
    {
        saveLoadService = new SaveLoadService();
        playerDataController = FindObjectOfType<PlayerDataController>().GetComponent<PlayerDataController>();
        if (PlayerPrefs.GetInt(Constants.NEWGAME) == 1)
        {
            seaWrapper = GameObject.FindGameObjectWithTag("SeaWrapper");
            seaPrefab = Resources.Load("Prefabs/Procedural/OceanTile") as GameObject;
            loadWater();

            bottomWrapper = GameObject.FindGameObjectWithTag("BottomWrapper");
            sandPrefab = Resources.Load("Prefabs/Procedural/SandTile") as GameObject;
            decorationPrefab = Resources.Load("Prefabs/Procedural/BottomDecoration1") as GameObject;
            loadBottom();

            mapWrapper = GameObject.FindGameObjectWithTag("MapWrapper");
            islandArray = new List<Island>();
            generateIslands(30);
            islandPrefab = Resources.Load("Prefabs/Procedural/Island1") as GameObject;
            loadIslands();

            playerShipPrefab = Resources.Load("Prefabs/Procedural/PlayerShip") as GameObject;
            loadPlayerShipPrefab();

            eventsWrapper = GameObject.FindGameObjectWithTag("EventsWrapper");
            PlayerPrefs.SetInt(Constants.NEWGAME, 0);
            playerDataController.Save();
        }
        else
        {
            playerDataController.Load();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //addEvent();
    }

    // LoadSceneMode a 10X10 water prefabs
    void loadWater()
    {
        int fixedOffset = 30;
        int waterWidth = 30;
        for (int x = 0; x < 100; x++)
        {
            for (int z = 0; z < 100; z++)
            {
                GameObject tile = GameObject.Instantiate(seaPrefab, new Vector3((x * waterWidth) + fixedOffset, SURFACE_Y, z * waterWidth), Quaternion.identity);
                tile.transform.parent = seaWrapper.transform;
                playerDataController.PlayerData.MapData.Add(new MapData()
                {
                    PrefabName = seaPrefab.name,
                    Position = tile.transform.position,
                    Rotation = tile.transform.rotation
                });
            }
        }
    }

    // LoadSceneMode a 10X10 water prefabs
    void loadBottom()
    {
        float sandWidth = 4.45f;
        for (int x = 0; x < 680; x++)
        {
            for (int z = 0; z < 680; z++)
            {
                GameObject tile = GameObject.Instantiate(sandPrefab, new Vector3(x * sandWidth, BOTTOM_Y, z * sandWidth), Quaternion.identity);
                tile.transform.parent = bottomWrapper.transform;
                playerDataController.PlayerData.MapData.Add(new MapData
                {
                    PrefabName = sandPrefab.name,
                    Position = tile.transform.position,
                    Rotation = tile.transform.rotation
                });
                if (Random.Range(1, 25) == 1)
                {
                    GameObject decoration = GameObject.Instantiate(decorationPrefab, new Vector3(x * sandWidth, BOTTOM_Y, z * sandWidth), Quaternion.identity);
                    decoration.transform.parent = bottomWrapper.transform;
                    playerDataController.PlayerData.MapData.Add(new MapData
                    {
                        PrefabName = decorationPrefab.name,
                        Position = decoration.transform.position,
                        Rotation = decoration.transform.rotation
                    });
                }
            }
        }
    }

    void loadIslands()
    {
        for (int i = 0; i < islandArray.Count; i++)
        {
            GameObject island = GameObject.Instantiate(islandPrefab, new Vector3(((Island)islandArray[i]).getX(), ((Island)islandArray[i]).getY(), ((Island)islandArray[i]).getZ()), Quaternion.identity);
            island.transform.parent = mapWrapper.transform;
            playerDataController.PlayerData.MapData.Add(new MapData
            {
                PrefabName = islandPrefab.name,
                Position = island.transform.position,
                Rotation = island.transform.rotation
            });
        }
    }

    void generateIslands(int numberOfIslands)
    {
        for (int i = 1; i <= numberOfIslands; i++)
        {
            Island island = new Island(Random.Range(GRID_BOTTOM_LIMIT, GRID_UPPER_LIMIT), Random.Range(GRID_BOTTOM_LIMIT, GRID_UPPER_LIMIT), 1);
            islandArray.Add(island);
        }
    }

    // Loads the player boat
    void loadPlayerShipPrefab()
    {
        GameObject playerShip = GameObject.Instantiate(playerShipPrefab, new Vector3(100, SURFACE_Y, 100), Quaternion.identity);
        playerShip.transform.parent = seaWrapper.transform;
        playerDataController.PlayerData.MapData.Add(new MapData
        {
            PrefabName = playerShipPrefab.name,
            Position = playerShip.transform.position,
            Rotation = playerShip.transform.rotation
        });
    }

}
