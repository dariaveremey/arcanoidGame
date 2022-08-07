using System;
using UnityEngine;

[Serializable]
public class PickUpInfo
{
    [HideInInspector]
    public string name;
    public PickUpBase PickUpPrefab;
    public int SpawnChance;

    public void OnVolidate()
    {
        name = PickUpPrefab == null ? String.Empty : PickUpPrefab.name;
    }
}