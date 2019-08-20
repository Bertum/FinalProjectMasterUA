using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    private float offSetX = 25f;
    private float offSetZ = 25f;

    // Start is called before the first frame update
    void Start() {

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        offSetX = UnityEngine.Random.Range(0, 100f);
        offSetZ = UnityEngine.Random.Range(0, 100f);
        createShape();
        updateMesh();
    }

    private void updateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void createShape() {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        //Create Vertices
        for (int i = 0, z = 0; z <= zSize; z++) {
            for(int x = 0; x<= xSize; x++) {
                float y = Mathf.PerlinNoise(x * .2f + offSetX, z * .2f + offSetZ) * 1.2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        //vertices of a square
        int vert = 0;
        //triangles in a square
        int tris = 0;
        //Build a square
        for (int z = 0; z < zSize; z++) {
            for (int x = 0; x < xSize; x++) {
                //low triangle |_
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                //top triangle -|
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                //6 because 3vertices+3vertices
                tris += 6;
            }
            vert++;
        }

    }

    // Update is called once per frame
    void Update() {
        offSetX += Time.deltaTime;
        createShape();
        updateMesh();
    }
}
