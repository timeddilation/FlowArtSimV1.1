using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRegionsXZ : MonoBehaviour
{
    public static PlaneRegionsXZ instance;
    public float propReach = 55f;

    public Vector2 localRightStart;
    public Vector2 localRightEnd;

    public Vector2 localLeftStart;
    public Vector2 localLeftEnd;

    public Vector2 localForwardStart;
    public Vector2 localForwardEnd;

    public Vector2 localBackStart;
    public Vector2 localBackEnd;

    private EnvironmentVariables envVariables;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one PlaneRegionsXZ instance!");
        }
        instance = this;
    }

    private void Start()
    {
        envVariables = EnvironmentVariables.instance;

        localRightStart = new Vector2(-55, -envVariables.regionDetectionThreshold);
        localRightEnd = new Vector2(-55, envVariables.regionDetectionThreshold);

        localLeftStart = new Vector2(55, -envVariables.regionDetectionThreshold);
        localLeftEnd = new Vector2(55, -envVariables.regionDetectionThreshold);

        localForwardStart = new Vector2(-envVariables.regionDetectionThreshold, -55);
        localForwardEnd = new Vector2(envVariables.regionDetectionThreshold, -55);

        localBackStart = new Vector2(-envVariables.regionDetectionThreshold, 55);
        localBackEnd = new Vector2(envVariables.regionDetectionThreshold, 55);
    }
}
