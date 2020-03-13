using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "MissileData", menuName = "ScriptableObjects/MissileData", order = 2)]
public class MissileData : ScriptableObject
{
    [System.NonSerialized]
    public List<GameObject> trackableObjects = new List<GameObject>();

    public GameObject missile;

    private void Awake()
    {
        Assert.IsNotNull(missile, "Missile was not found");
    }
}

