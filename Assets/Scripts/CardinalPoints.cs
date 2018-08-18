using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardinalPoints : MonoBehaviour
{
    public static CardinalPoints instance;

    [Header("Cardinal Points Around Spinner")]
    public GameObject localLeft;
    public GameObject localRight;
    public GameObject localUp;
    public GameObject localDown;
    public GameObject localForward;
    public GameObject localBack;

    public Vector2 localLeftXY;
    public Vector2 localRightXY;
    public Vector2 localUpXY;
    public Vector2 localDownXY;
    public Vector2 localForwardXY;
    public Vector2 localBackXY;

    private void Awake()
    {
        instance = this;
        
        //setup all the Vector2's of cardinal points
        localLeftXY.x = localLeft.transform.position.x;
        localRightXY.x = localRight.transform.position.x;
        localUpXY.x = localUp.transform.position.x;
        localDownXY.x = localDown.transform.position.x;
        localForwardXY.x = localForward.transform.position.x;
        localBackXY.x = localBack.transform.position.x;

        localLeftXY.y = localLeft.transform.position.y;
        localRightXY.y = localRight.transform.position.y;
        localUpXY.y = localUp.transform.position.y;
        localDownXY.y = localDown.transform.position.y;
        localForwardXY.y = localForward.transform.position.y;
        localBackXY.y = localBack.transform.position.y;
    }
}
