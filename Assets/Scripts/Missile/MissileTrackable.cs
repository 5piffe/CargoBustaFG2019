using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class MissileTrackable : MonoBehaviour
{
    private MissileData data = null;
    private int defaultLayer, enemyLayer, trackableLayer, layers;
    //[System.NonSerialized]
    public List<MissileGuidance> trackedList = new List<MissileGuidance>();

    

    private void Awake()
    {
        defaultLayer = LayerMask.NameToLayer("Default");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        trackableLayer = LayerMask.NameToLayer("Trackable");

        if(gameObject.layer != trackableLayer && gameObject.layer != enemyLayer)
            throw new System.Exception("LayerMask Busy");

        data = Resources.Load<MissileData>("ScriptableObjects/MissileData");
        Assert.IsNotNull(data, "Could not find Missile Data");
        data.trackableObjects.Add(gameObject);
    }


}
