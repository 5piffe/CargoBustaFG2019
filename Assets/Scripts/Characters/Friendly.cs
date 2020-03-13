using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Friendly : MonoBehaviour
{
    

    private void Awake()
    {
        TargetData tData = Resources.Load<TargetData>("ScriptableObjects/TargetData");
        Assert.IsNotNull(tData);
        tData.friendlies.Add(gameObject);
    }
}
