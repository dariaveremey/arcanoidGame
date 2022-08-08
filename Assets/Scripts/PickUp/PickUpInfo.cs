using System;
using UnityEngine;

[Serializable]
public class PickUpInfo
{
    #region Variables

    [HideInInspector]
    public string name;
    public PickUpBase PickUpPrefab;
    public int SpawnChance;

    #endregion


    #region Private methods

    public void OnVolidate()
    {
        name = PickUpPrefab == null ? String.Empty : PickUpPrefab.name;
    }

    #endregion
}