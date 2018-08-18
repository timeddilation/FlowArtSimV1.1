using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentVariables : MonoBehaviour
{
    public float globalSpeed = 1f;
    public float poiTrailSpeed = 3f;

    public static EnvironmentVariables instance;

    private void Awake()
    {
        instance = this;
    }
}
