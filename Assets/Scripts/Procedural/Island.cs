using UnityEngine;

public class Island
{
    float x;
    float y;
    float z;
    string prefabStr;
    int level;
    //List<Buildings> buildings
    public Island(float x, float z, int level)
    {
        this.x = x;
        this.z = z;
        this.y = 0.8f;
        this.level = level;
        switch (this.level)
        {
            case 1:
                prefabStr = "Prefabs/Procedural/Island1";
                break;
            case 2:
                prefabStr = "Prefabs/Procedural/Island2";
                break;
            case 3:
                prefabStr = "Prefabs/Procedural/Island1";
                break;
            default:
                prefabStr = "Prefabs/Procedural/Island1";
                break;
        }
    }

    public float getX()
    {
        return x;
    }

    public float getY()
    {
        return y;
    }

    public float getZ()
    {
        return z;
    }

    public int getLevel()
    {
        return level;
    }

    public GameObject getPrefab() {
        return Resources.Load(prefabStr) as GameObject;
    }
}