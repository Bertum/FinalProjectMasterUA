using System.Collections.Generic;
using UnityEngine;

public class BaseProceduralScene : MonoBehaviour
{
    public static int BOTTOM_Y = 0;
    public static int SURFACE_Y = 3;

    private GameObject seaWrapper;
    private GameObject bottomWrapper;
    private GameObject seaPrefab;
    private GameObject sandPrefab;

    private List<GameObject> bottomLayer;
    private List<GameObject> surfaceLayer;

    // Start is called before the first frame update
    void Start() {
        start();
    }

    public void start() {
        setupCamera();

        seaWrapper = GameObject.FindGameObjectWithTag("SeaWrapper");
        seaPrefab = Resources.Load("Prefabs/OceanTile") as GameObject;
        loadWater();

        bottomWrapper = GameObject.FindGameObjectWithTag("BottomWrapper");
        sandPrefab = Resources.Load("Prefabs/SandTile") as GameObject;
        loadBottom();
    }


    // LoadSceneMode a 10X10 water prefabs
    void loadWater() {
        int fixedOffset = 30;
        int waterWidth = 30;
        for (int x = 0; x < 10; x++) {
            for (int z = 0; z < 10; z++) {
                GameObject tile = GameObject.Instantiate(seaPrefab, new Vector3((x * waterWidth) + fixedOffset, SURFACE_Y, z * waterWidth), Quaternion.identity);
                tile.transform.parent = seaWrapper.transform;
            }
        }
    }

    // LoadSceneMode a 10X10 water prefabs
    void loadBottom() {
        float sandWidth = 4.45f;
        for (int x = 0; x < 68; x++) {
            for (int z = 0; z < 68; z++) {
                GameObject tile = GameObject.Instantiate(sandPrefab, new Vector3(x * sandWidth, BOTTOM_Y, z * sandWidth), Quaternion.identity);
                tile.transform.parent = bottomWrapper.transform;
            }
        }
    }

    // Setup the camera initial params
    void setupCamera() {


    }


}
