using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRegionsYZ : MonoBehaviour
{
    public static PlaneRegionsYZ instance;
    public float propReach = 55f;

    public Vector2 localForwardStart;
    public Vector2 localForwardEnd;

    public Vector2 localBackStart;
    public Vector2 localBackEnd;

    public Vector2 localUpStart;
    public Vector2 localUpEnd;

    public Vector2 localDownStart;
    public Vector2 localDownEnd;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one PlaneRegionsYZ instance!");
        }
        instance = this;
    }

    private void Start()
    {
        localForwardStart = new Vector2(-1, -propReach);
        localForwardEnd = new Vector2(1, -propReach);

        localBackStart = new Vector2(-1, propReach);
        localBackEnd = new Vector2(1, propReach);

        localUpStart = new Vector2(propReach, -1);
        localUpEnd = new Vector2(propReach, 1);

        localDownStart = new Vector2(-propReach, -1);
        localDownEnd = new Vector2(-propReach, 1);
    }
}
