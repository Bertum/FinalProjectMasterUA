using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string PlayerName;
    public Vector3 Position;
    public Quaternion Rotation;
    public string SceneName;
    public int CurrentFood;
    public int CurrentPirates;
    public int CurrentGold;
    public int TotalFood;
    public int TotalPirates;
    public int TotalGold;
    public List<MapData> MapData = new List<MapData>();
    public List<NpcBase> CurrentCrew = new List<NpcBase>();
}
