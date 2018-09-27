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
        localRightStart = new Vector2(-propReach, -1);
        localRightEnd = new Vector2(-propReach, 1);

        localLeftStart = new Vector2(propReach, -1);
        localLeftEnd = new Vector2(propReach, 1);

        localUpStart = new Vector2(-1, propReach);
        localUpEnd = new Vector2(1, propReach);

        localDownStart = new Vector2(-1, -propReach);
        localDownEnd = new Vector2(1, -propReach);
    }
}
