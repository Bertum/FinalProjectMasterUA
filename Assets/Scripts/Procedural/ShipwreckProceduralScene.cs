using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckProceduralScene : BaseProceduralScene
{
    private GameObject seaWrapper;
    private GameObject boatPrefab;
    private List<GameObject> debrisPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        base.start();
        seaWrapper = GameObject.FindGameObjectWithTag("SeaWrapper");
        boatPrefab = Resources.Load("Prefabs/Procedural/Boat") as GameObject;
        loadBoat();

        debrisPrefabs = new List<GameObject>();
        debrisPrefabs.Add(Resources.Load("Prefabs/Procedural/Debris1") as GameObject);
        debrisPrefabs.Add(Resources.Load("Prefabs/Procedural/Debris2") as GameObject);
        debrisPrefabs.Add(Resources.Load("Prefabs/Procedural/Debris3") as GameObject);
        loadDebris();
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Loads the player boat
    void loadBoat() {
        GameObject boat = GameObject.Instantiate(boatPrefab, new Vector3(100, SURFACE_Y, 100), Quaternion.identity);
        boat.transform.parent = seaWrapper.transform;
    }

    // Loads ramdom positioned wreck
    void loadDebris() {
        int numberOfDebris = Random.Range(6, 10);

        for (int x = 1; x <= numberOfDebris; x++)
        {
            GameObject debris = GameObject.Instantiate((GameObject)debrisPrefabs[Random.Range(0,3)], new Vector3(Random.Range(50, 250), SURFACE_Y, Random.Range(50, 250)), Quaternion.identity);
            debris.transform.parent = seaWrapper.transform;
        }
    }
}
