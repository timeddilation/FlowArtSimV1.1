using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentVariables : MonoBehaviour
{
    [Header("Simulation Tweaks")]
    public float globalSpeed = 1f;
    public float poiTrailSpeed = 3f;
    public string spinnerProps = "Hoops";

    [Header("Spinner Prefabs")]
    public GameObject hooperWallPlane;
    public GameObject hooperWheelPlane;
    public GameObject poiWallPlane;
    public GameObject poiWheelPlane;

    public static EnvironmentVariables instance;

    private void Awake()
    {
        instance = this;
    }
}
