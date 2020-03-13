using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(LineRenderer))]
public class MagnetSparks : MonoBehaviour
{
    Magnet magnet;
    LineRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        magnet = GetComponent<Magnet>();
        renderer = GetComponent<LineRenderer>();

        Assert.IsNotNull(magnet);
    }

}
