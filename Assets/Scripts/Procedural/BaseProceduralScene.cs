using System.Collections.Generic;
using UnityEngine;

public class BaseProceduralScene : MonoBehaviour
{
    public static int BOTTOM_Y = 0;
    public static int SURFACE_Y = 3;

    protected GameObject seaWrapper;
    protected GameObject bottomWrapper;
    private GameObject seaPrefab;
    private GameObject sandPrefab;
    private GameObject decorationPrefab;
    private GameObject borderPrefab;

    // Start is called before the first frame update
    void Start() {
        start();
    }

    public void start() {
        borderPrefab = Resources.Load("Prefabs/Procedural/BorderInv") as GameObject;
        seaWrapper = GameObject.FindGameObjectWithTag("SeaWrapper");
        seaPrefab = Resources.Load("Prefabs/Procedural/OceanTile") as GameObject;
        loadWater();

        bottomWrapper = GameObject.FindGameObjectWithTag("BottomWrapper");
        sandPrefab = Resources.Load("Prefabs/Procedural/SandTile") as GameObject;
        decorationPrefab = Resources.Load("Prefabs/Procedural/BottomDecoration1") as GameObject;
        loadBottom();
    }


    // LoadSceneMode a 10X10 water prefabs
    void loadWater() {
        int fixedOffset = 30;
        int waterWidth = 30;
        for(int x = -3; x < 13; x++) {
            for (int z = -3; z < 13; z++) {
                GameObject tile = GameObject.Instantiate(seaPrefab, new Vector3((x * waterWidth) + fixedOffset, SURFACE_Y, z * waterWidth), Quaternion.identity);
                tile.transform.parent = seaWrapper.transform;
            }
        }
        for (int x = 0; x < 10; x++) {
            GameObject a = GameObject.Instantiate(borderPrefab, new Vector3((x * waterWidth), SURFACE_Y, 0), Quaternion.identity);
            a.transform.parent = seaWrapper.transform;
            GameObject b = GameObject.Instantiate(borderPrefab, new Vector3((x * waterWidth), SURFACE_Y, 10 * waterWidth), Quaternion.identity);
            b.transform.parent = seaWrapper.transform;
            GameObject c = GameObject.Instantiate(borderPrefab, new Vector3((0 * waterWidth), SURFACE_Y, x * waterWidth), Quaternion.identity);
            c.transform.parent = seaWrapper.transform;
            GameObject d = GameObject.Instantiate(borderPrefab, new Vector3((10 * waterWidth), SURFACE_Y, x * waterWidth), Quaternion.identity);
            d.transform.parent = seaWrapper.transform;
        }
    }

    // LoadSceneMode a 10X10 water prefabs
    void loadBottom() {
        float sandWidth = 4.45f;
        for (int x = 0; x < 68; x++) {
            for (int z = 0; z < 68; z++) {
                GameObject tile = GameObject.Instantiate(sandPrefab, new Vector3(x * sandWidth, BOTTOM_Y, z * sandWidth), Quaternion.identity);
                tile.transform.parent = bottomWrapper.transform;
                if (Random.Range(1,25) == 1) {
                    GameObject decoration = GameObject.Instantiate(decorationPrefab, new Vector3(x * sandWidth, BOTTOM_Y, z * sandWidth), Quaternion.identity);
                    decoration.transform.parent = bottomWrapper.transform;
                }
            }
        }
    }
}
