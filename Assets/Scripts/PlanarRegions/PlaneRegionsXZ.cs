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
        localRightStart = new Vector2(-55, -1);
        localRightEnd = new Vector2(-55, 1);

        localLeftStart = new Vector2(55, -1);
        localLeftEnd = new Vector2(55, -1);

        localForwardStart = new Vector2(-1, -55);
        localForwardEnd = new Vector2(1, -55);

        localBackStart = new Vector2(-1, 55);
        localBackEnd = new Vector2(1, 55);
    }
}
