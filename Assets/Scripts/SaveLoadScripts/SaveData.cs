using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string playerName;
    public Vector3Data playerPosition;
    public List<string> unlockedAchievements;
}

[System.Serializable]
public class Vector3Data
{
    public float x, y, z;

    public Vector3Data(Vector3 v) { x = v.x; y = v.y; z = v.z; }
    public Vector3 ToVector3() => new Vector3(x, y, z);
}