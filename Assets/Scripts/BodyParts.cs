using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyParts : MonoBehaviour
{    
    [Header("Left Side")]
    public GameObject leftShoulder;
    public GameObject leftArm;
    public GameObject leftElbow;
    public GameObject leftForeArm;
    public GameObject leftWrist;
    public GameObject leftHand;
    public GameObject leftProp;
    [Header("Right Side")]
    public GameObject rightShoulder;
    public GameObject rightArm;
    public GameObject rightElbow;
    public GameObject rightForeArm;
    public GameObject rightWrist;
    public GameObject rightHand;
    public GameObject rightProp;
    [Header("Self (Unity Sets)")]
    public GameObject torso;
    public static BodyParts instance;
    [Header("Left Arm Spins (Unity Sets)")]
    public PropSpins leftPropSpin;
    public TrailRenderer leftPropTrail;
    public Vector2 leftPropPos;
    public ShoulderSpins leftShoulderSpin;
    public ForearmSpins leftForearmSpin;
    public ArmSpins leftArmSpin;
    public HandSpins leftHandSpin;
    [Header("Right Arm Spins (Unity Sets)")]
    public PropSpins rightPropSpin;
    public TrailRenderer rightPropTrail;
    public Vector2 rightPropPos;
    public ShoulderSpins rightShoulderSpin;
    public ForearmSpins rightForearmSpin;
    public ArmSpins rightArmSpin;
    public HandSpins rightHandSpin;
    [Header("other")]
    public string leftPropZeroPointRegionDebugText = "";
    public string rightPropZeroPointRegionDebugText = "";
    private Vector2 localRightStart = new Vector2(-55, -1);
    private Vector2 localRightEnd = new Vector2(-55, 1);
    private Vector2 localLeftStart = new Vector2(55, -1);
    private Vector2 localLeftEnd = new Vector2(55, 1);
    private Vector2 localDownStart = new Vector2(-1, -55);
    private Vector2 localDownEnd = new Vector2(1, -55);
    private Vector2 localUpStart = new Vector2(-1, 55);
    private Vector2 localUpEnd = new Vector2(1, 55);

    public ZeroPointRegion leftPropRegion;
    public ZeroPointRegion rightPropRegion;
    public bool zeroPointStageUpdate = false;

    private float posLeftX = 0f;
    private float posLeftY = 0f;
    private float posRightX = 0f;
    private float posRightY = 0f;

    private float previousLeftX = 0f;
    private float previousLeftY = 0f;
    private float previousRightX = 0f;
    private float previousRightY = 0f;

    private void Awake()
    {
        torso = gameObject;
        if (instance != null)
        {
            Debug.LogWarning("More than one BodyParts instance!");
        }
        instance = this;

        //left side spins
        leftPropSpin = leftProp.GetComponent<PropSpins>();
        leftPropTrail = leftProp.GetComponent<TrailRenderer>();
        leftShoulderSpin = leftShoulder.GetComponent<ShoulderSpins>();
        leftForearmSpin = leftForeArm.GetComponent<ForearmSpins>();
        leftArmSpin = leftArm.GetComponent<ArmSpins>();
        leftHandSpin = leftHand.GetComponent<HandSpins>();
        //right side spins
        rightPropSpin = rightProp.GetComponent<PropSpins>();
        rightPropTrail = rightProp.GetComponent<TrailRenderer>();
        rightShoulderSpin = rightShoulder.GetComponent<ShoulderSpins>();
        rightForearmSpin = rightForeArm.GetComponent<ForearmSpins>();
        rightArmSpin = rightArm.GetComponent<ArmSpins>();
        rightHandSpin = rightHand.GetComponent<HandSpins>();
    }

    private void Start()
    {
        GetVector2PoiPosition();
    }

    private void Update()
    {
        GetVector2PoiPosition();

        leftPropRegion = TrackZeroPointRegion(leftPropPos, PropSide.Left);
        rightPropRegion = TrackZeroPointRegion(rightPropPos, PropSide.Right);

        leftPropZeroPointRegionDebugText = "";
        rightPropZeroPointRegionDebugText = "";
    }

    private void GetVector2PoiPosition()
    {
        leftPropPos = new Vector2(leftProp.transform.position.x, leftProp.transform.position.y);
        rightPropPos = new Vector2(rightProp.transform.position.x, rightProp.transform.position.y);
    }

    private ZeroPointRegion TrackZeroPointRegion(Vector2 propPosition, PropSide side)
    {
        ZeroPointRegion region = ZeroPointRegion.None;
        zeroPointStageUpdate = false;

        //check for local right
        if ((propPosition.x < localRightStart.x && propPosition.y > localRightStart.y)
            || (propPosition.x < localRightEnd.x && propPosition.y < localRightEnd.y))
        {
            region = ZeroPointRegion.LocalRight;
        }
        //check for local left
        else if ((propPosition.x > localLeftStart.x && propPosition.y > localLeftStart.y)
            || (propPosition.x > localLeftEnd.x && propPosition.y < localLeftEnd.y))
        {
            region = ZeroPointRegion.LocalLeft;
        }
        //check for local down
        else if ((propPosition.x > localDownStart.x && propPosition.y < localDownStart.y)
            || (propPosition.x < localDownEnd.x && propPosition.y < localDownEnd.y))
        {
            region = ZeroPointRegion.LocalDown;
        }
        //check for local up
        else if ((propPosition.x > localUpStart.x && propPosition.y > localUpStart.y)
            || (propPosition.x < localUpEnd.x && propPosition.y > localUpEnd.y))
        {
            region = ZeroPointRegion.LocalUp;
        }

        //track positions of both props at all times
        if (side == PropSide.Left)
        {
            //temporarily store previous XY values last frame
            previousLeftX = posLeftX;
            previousLeftY = posLeftY;
            //update positions to current values this frame
            posLeftX = propPosition.x;
            posLeftY = propPosition.y;

            if ((leftPropRegion == ZeroPointRegion.LocalRight || leftPropRegion == ZeroPointRegion.LocalLeft)
                && ((posRightY > 0 && previousRightY < 0) || (posRightY < 0 && previousRightY > 0)))
            {
                zeroPointStageUpdate = true;
            }
            else if ((leftPropRegion == ZeroPointRegion.LocalUp || leftPropRegion == ZeroPointRegion.LocalDown)
                && ((posRightX > 0 && previousRightX < 0) || (posRightX < 0 && previousRightX > 0)))
            {
                zeroPointStageUpdate = true;
            }
        }
        else if (side == PropSide.Right)
        {
            //temporarily store previous XY values last frame
            previousRightX = posRightX;
            previousRightY = posRightY;
            //update positions to current values this frame
            posRightX = propPosition.x;
            posRightY = propPosition.y;

            if ((rightPropRegion == ZeroPointRegion.LocalRight || rightPropRegion == ZeroPointRegion.LocalLeft)
                && ((posRightY > 0 && previousRightY < 0) || (posRightY < 0 && previousRightY > 0)))
            {
                zeroPointStageUpdate = true;
            }
            else if ((rightPropRegion == ZeroPointRegion.LocalUp || rightPropRegion == ZeroPointRegion.LocalDown)
               && ((posRightX > 0 && previousRightX < 0) || (posRightX < 0 && previousRightX > 0)))
            {
                zeroPointStageUpdate = true;
            }
        }

            return region;
    }
}
