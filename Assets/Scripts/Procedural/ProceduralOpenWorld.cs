using System;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralOpenWorld : MonoBehaviour
{

    public static int BOTTOM_Y = 0;
    public static int SURFACE_Y = 3;
    public static float GRID_UPPER_LIMIT = 1400;
    public static float GRID_BOTTOM_LIMIT = 100f;

    private GameObject seaWrapper;
    private GameObject bottomWrapper;
    private GameObject mapWrapper;
    private GameObject eventsWrapper;
    private GameObject seaPrefab;
    private GameObject borderPrefab1;
    private GameObject borderPrefab2;
    private GameObject borderPrefab3;
    private GameObject borderPrefab4;
    private GameObject sandPrefab;
    private GameObject decorationPrefab;
    private GameObject playerShipPrefab;

    private List<Island> islandArray;
    private PlayerDataController playerDataController;
    private SaveLoadService saveLoadService;
    private UIController uiController;

    // Start is called before the first frame update
    void Awake()
    {
        saveLoadService = new SaveLoadService();
        playerDataController = FindObjectOfType<PlayerDataController>().GetComponent<PlayerDataController>();
        uiController = FindObjectOfType<UIController>();
        if (!PlayerPrefs.HasKey(Constants.NEWGAME) || PlayerPrefs.GetInt(Constants.NEWGAME) == 1)
        {
            seaWrapper = GameObject.FindGameObjectWithTag("SeaWrapper");
            seaPrefab = Resources.Load("Prefabs/Procedural/OceanTile") as GameObject;
            borderPrefab1 = Resources.Load("Prefabs/Procedural/Border1") as GameObject;
            borderPrefab2 = Resources.Load("Prefabs/Procedural/Border2") as GameObject;
            borderPrefab3 = Resources.Load("Prefabs/Procedural/Border3") as GameObject;
            borderPrefab4 = Resources.Load("Prefabs/Procedural/Border4") as GameObject;
            loadWater();

            bottomWrapper = GameObject.FindGameObjectWithTag("BottomWrapper");
            sandPrefab = Resources.Load("Prefabs/Procedural/SandTile") as GameObject;
            decorationPrefab = Resources.Load("Prefabs/Procedural/BottomDecoration1") as GameObject;
            loadBottom();

            mapWrapper = GameObject.FindGameObjectWithTag("MapWrapper");
            islandArray = new List<Island>();
            generateIslands(9, 1);
            generateIslands(6, 2);
            generateIslands(3, 3);
            loadIslands();

            playerShipPrefab = Resources.Load("Prefabs/Procedural/PlayerShip") as GameObject;

            eventsWrapper = GameObject.FindGameObjectWithTag("EventsWrapper");
            //Default resources when creating a new game
            SetInitialPlayerData();
            loadPlayerShipPrefab();
            playerDataController.Save();
            uiController.ResourcesChanged(playerDataController.PlayerData);
        }
        else
        {
            playerDataController.Load();
        }
    }

    private void SetInitialPlayerData()
    {
        playerDataController.PlayerData.TotalFood = 100;
        playerDataController.PlayerData.TotalPirates = 50;
        playerDataController.PlayerData.TotalGold = 1000;
        playerDataController.PlayerData.TotalMedicine = 20;
        playerDataController.PlayerData.TotalWater = 100;
        playerDataController.PlayerData.CurrentFood = 25;
        playerDataController.PlayerData.CurrentPirates = 5;
        playerDataController.PlayerData.CurrentWater = 50;
        playerDataController.PlayerData.CurrentGold = 100;
        playerDataController.PlayerData.CurrentMedicine = 5;
    }

    private void Start()
    {
        PlayerPrefs.SetInt(Constants.NEWGAME, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //addEvent();
    }

    // LoadSceneMode a 100X100 water prefabs
    void loadWater()
    {
        int fixedOffset = 30;
        int waterWidth = 30;
        GameObject borderPrefab;
        for (int x = 0; x < 50; x++)
        {
            switch (UnityEngine.Random.Range(1, 10))
            {
                case 1:
                    borderPrefab = borderPrefab1;
                    break;
                case 2:
                    borderPrefab = borderPrefab2;
                    break;
                case 3:
                    borderPrefab = borderPrefab3;
                    break;
                case 4:
                    borderPrefab = borderPrefab4;
                    break;
                default:
                    borderPrefab = borderPrefab3;
                    break;
            }
            GameObject a = GameObject.Instantiate(borderPrefab, new Vector3((x * waterWidth), SURFACE_Y, 0), Quaternion.identity);
            a.transform.parent = seaWrapper.transform;
            GameObject b = GameObject.Instantiate(borderPrefab, new Vector3((x * waterWidth), SURFACE_Y, 50 * waterWidth), Quaternion.identity);
            b.transform.parent = seaWrapper.transform;
            GameObject c = GameObject.Instantiate(borderPrefab, new Vector3((0 * waterWidth), SURFACE_Y, x * waterWidth), Quaternion.identity);
            c.transform.parent = seaWrapper.transform;
            GameObject d = GameObject.Instantiate(borderPrefab, new Vector3((50 * waterWidth), SURFACE_Y, x * waterWidth), Quaternion.identity);
            d.transform.parent = seaWrapper.transform;
            playerDataController.PlayerData.MapData.Add(new MapData()
            {
                PrefabName = borderPrefab.name,
                Position = a.transform.position,
                Rotation = a.transform.rotation
            });
            playerDataController.PlayerData.MapData.Add(new MapData()
            {
                PrefabName = borderPrefab.name,
                Position = b.transform.position,
                Rotation = b.transform.rotation
            });
            playerDataController.PlayerData.MapData.Add(new MapData()
            {
                PrefabName = borderPrefab.name,
                Position = c.transform.position,
                Rotation = c.transform.rotation
            });
            playerDataController.PlayerData.MapData.Add(new MapData()
            {
                PrefabName = borderPrefab.name,
                Position = d.transform.position,
                Rotation = d.transform.rotation
            });

        }

        for (int x = 0; x < 50; x++)
        {
            for (int z = 0; z < 50; z++)
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

    void loadBottom()
    {
        float sandWidth = 4.45f;
        for (int x = 0; x < 340; x++)
        {
            for (int z = 0; z < 340; z++)
            {
                GameObject tile = GameObject.Instantiate(sandPrefab, new Vector3(x * sandWidth, BOTTOM_Y, z * sandWidth), Quaternion.identity);
                tile.transform.parent = bottomWrapper.transform;
                playerDataController.PlayerData.MapData.Add(new MapData
                {
                    PrefabName = sandPrefab.name,
                    Position = tile.transform.position,
                    Rotation = tile.transform.rotation
                });
                if (UnityEngine.Random.Range(1, 30) == 1)
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
            GameObject island = GameObject.Instantiate(((Island)islandArray[i]).getPrefab(), new Vector3(((Island)islandArray[i]).getX(), ((Island)islandArray[i]).getY(), ((Island)islandArray[i]).getZ()), Quaternion.identity);
            island.transform.parent = mapWrapper.transform;
            playerDataController.PlayerData.MapData.Add(new MapData
            {
                PrefabName = ((Island)islandArray[i]).getPrefab().name,
                Position = island.transform.position,
                Rotation = island.transform.rotation
            });
        }
    }

    void generateIslands(int numberOfIslands, int lvl)
    {
        for (int i = 1; i <= numberOfIslands; i++)
        {
            Island island = new Island(UnityEngine.Random.Range(GRID_BOTTOM_LIMIT, GRID_UPPER_LIMIT), UnityEngine.Random.Range(GRID_BOTTOM_LIMIT, GRID_UPPER_LIMIT), lvl);
            if (!isAnotherIslandClose(island.getX(), island.getZ()))
            {
                islandArray.Add(island);
            }
            else
            {
                i--;
            }
        }
    }

    // Loads the player boat
    void loadPlayerShipPrefab()
    {
        GameObject playerShip = GameObject.Instantiate(playerShipPrefab, new Vector3(150, SURFACE_Y, 150), Quaternion.identity);
        playerShip.transform.parent = seaWrapper.transform;
        playerDataController.PlayerData.MapData.Add(new MapData
        {
            PrefabName = playerShipPrefab.name,
            Position = playerShip.transform.position,
            Rotation = playerShip.transform.rotation
        });
        ShipController controller = playerShip.GetComponentInChildren<ShipController>();
        controller.SetInitialCrew();
    }

    bool isAnotherIslandClose(float x, float z)
    {
        bool result = false;
        double pot = 2;
        for (int i = 0; i < islandArray.Count; i++)
        {
            double dx = ((Island)islandArray[i]).getX() - x;
            double dz = ((Island)islandArray[i]).getZ() - z;
            if (Math.Sqrt(Math.Pow(dx, pot) + Math.Pow(dz, pot)) < 80)
            {
                result = true;
                break;
            }
        }
        return result;
    }
}
