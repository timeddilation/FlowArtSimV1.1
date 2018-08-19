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

    //public Vector2 localLeftXY;
    //public Vector2 localRightXY;
    //public Vector2 localUpXY;
    //public Vector2 localDownXY;
    //public Vector2 localForwardXY;
    //public Vector2 localBackXY;

    private void Awake()
    {
        instance = this;


        //this may all become dead code, and removed later.
        //Oringally I was checking the magnitude difference between the cardinal point XY and the prop XY to determine proximity.
        //However, this was proving to be less reliable than checking the rotation of the prop, as it would eventually de-sync.
        //Instead, I opted for public bools to check how close an object is to the deired rotation of the cardinal points.

        ////setup all the Vector2's of cardinal points
        //localLeftXY.x = localLeft.transform.position.x;
        //localRightXY.x = localRight.transform.position.x;
        //localUpXY.x = localUp.transform.position.x;
        //localDownXY.x = localDown.transform.position.x;
        //localForwardXY.x = localForward.transform.position.x;
        //localBackXY.x = localBack.transform.position.x;

        //localLeftXY.y = localLeft.transform.position.y;
        //localRightXY.y = localRight.transform.position.y;
        //localUpXY.y = localUp.transform.position.y;
        //localDownXY.y = localDown.transform.position.y;
        //localForwardXY.y = localForward.transform.position.y;
        //localBackXY.y = localBack.transform.position.y;
    }

    public bool CheckLocalUpProximity(GameObject bodyPart, float sensitivity)
    {
        if ((270 - bodyPart.transform.rotation.eulerAngles.z) < sensitivity
            && (270 - bodyPart.transform.rotation.eulerAngles.z) > -sensitivity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckLocalDownProximity(GameObject bodyPart, float sensitivity)
    {
        if ((90 - bodyPart.transform.rotation.eulerAngles.z) > -sensitivity
            && (90 - bodyPart.transform.rotation.eulerAngles.z) < sensitivity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
