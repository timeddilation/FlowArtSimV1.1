using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRegionsXY : MonoBehaviour
{
    public static PlaneRegionsXY instance;
    public float propReach = 55f;

    public Vector2 localRightStart;
    public Vector2 localRightEnd;

    public Vector2 localLeftStart;
    public Vector2 localLeftEnd;

    public Vector2 localUpStart;
    public Vector2 localUpEnd;

    public Vector2 localDownStart;
    public Vector2 localDownEnd;

    private EnvironmentVariables envVariables;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one PlaneRegionsXY instance!");
        }
        instance = this;
    }

    private void Start()
    {
        envVariables = EnvironmentVariables.instance;

        localRightStart = new Vector2(-propReach, -envVariables.regionDetectionThreshold);
        localRightEnd = new Vector2(-propReach, envVariables.regionDetectionThreshold);

        localLeftStart = new Vector2(propReach, -envVariables.regionDetectionThreshold);
        localLeftEnd = new Vector2(propReach, envVariables.regionDetectionThreshold);

        localUpStart = new Vector2(-envVariables.regionDetectionThreshold, propReach);
        localUpEnd = new Vector2(envVariables.regionDetectionThreshold, propReach);

        localDownStart = new Vector2(-envVariables.regionDetectionThreshold, -propReach);
        localDownEnd = new Vector2(envVariables.regionDetectionThreshold, -propReach);
    }
}
