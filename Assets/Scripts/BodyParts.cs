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
    public Vector2 leftPropPosXY;
    public Vector2 leftPropPosXZ;
    public ShoulderSpins leftShoulderSpin;
    public ForearmSpins leftForearmSpin;
    public ArmSpins leftArmSpin;
    public HandSpins leftHandSpin;
    [Header("Right Arm Spins (Unity Sets)")]
    public PropSpins rightPropSpin;
    public TrailRenderer rightPropTrail;
    public Vector2 rightPropPosXY;
    public Vector2 rightPropPosXZ;
    public ShoulderSpins rightShoulderSpin;
    public ForearmSpins rightForearmSpin;
    public ArmSpins rightArmSpin;
    public HandSpins rightHandSpin;
    [Header("other")]
    public string leftPropZeroPointRegionDebugText = "";
    public string rightPropZeroPointRegionDebugText = "";
    //XY coords
    private Vector2 localRightStartXY = new Vector2(-55, -1);
    private Vector2 localRightEndXY = new Vector2(-55, 1);
    private Vector2 localLeftStartXY = new Vector2(55, -1);
    private Vector2 localLeftEndXY = new Vector2(55, 1);
    private Vector2 localDownStart = new Vector2(-1, -55);
    private Vector2 localDownEnd = new Vector2(1, -55);
    private Vector2 localUpStart = new Vector2(-1, 55);
    private Vector2 localUpEnd = new Vector2(1, 55);
    //XZ coords
    private Vector2 localRightStartXZ = new Vector2(-55, -1);
    private Vector2 localRightEndXZ = new Vector2(-55, 1);
    private Vector2 localLeftStartXZ = new Vector2(55, -1);
    private Vector2 localLeftEndXZ = new Vector2(55, -1);
    private Vector2 localForwardStart = new Vector2(-1, -55);
    private Vector2 localForwardEnd = new Vector2(1, -55);
    private Vector2 localBackStart = new Vector2(-1, 55);
    private Vector2 localBackEnd = new Vector2(1, 55);

    public ZeroPointRegion leftPropRegionXY;
    public ZeroPointRegion leftPropRegionXZ;
    public ZeroPointRegion rightPropRegionXY;
    public ZeroPointRegion rightPropRegionXZ;
    public bool zeroPointStageUpdate = false;

    private float posLeftX = 0f;
    private float posLeftY = 0f;
    private float posLeftZ = 0f;
    private float posRightX_XY = 0f;
    private float posRightX_XZ = 0f;
    private float posRightY = 0f;
    private float posRightZ = 0f;

    private float previousLeftX = 0f;
    private float previousLeftY = 0f;
    private float previosLeftZ = 0f;
    private float previousRightX_XY = 0f;
    private float previousRightX_XZ = 0f;
    private float previousRightY = 0f;
    private float previousRightZ = 0f;

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

        leftPropRegionXY = TrackZeroPointRegionXY(leftPropPosXY, PropSide.Left);
        rightPropRegionXY = TrackZeroPointRegionXY(rightPropPosXY, PropSide.Right);

        rightPropRegionXZ = TrackZeroPointRegionXZ(rightPropPosXZ, PropSide.Right);

        leftPropZeroPointRegionDebugText = leftPropPosXY.x.ToString() + " , " + leftPropPosXY.y.ToString();
        rightPropZeroPointRegionDebugText = rightPropPosXY.x.ToString() + " , " + rightPropPosXZ.y.ToString();
        //rightPropZeroPointRegionDebugText = rightProp.transform.position.x.ToString() + " , " + rightProp.transform.position.y.ToString() + " , " + rightProp.transform.position.z.ToString();
    }

    private void GetVector2PoiPosition()
    {
        leftPropPosXY = new Vector2(leftProp.transform.position.x, leftProp.transform.position.y);
        rightPropPosXY = new Vector2(rightProp.transform.position.x, rightProp.transform.position.y);

        leftPropPosXZ = new Vector2(leftProp.transform.position.x, leftProp.transform.position.z);
        rightPropPosXZ = new Vector2(rightProp.transform.position.x, rightProp.transform.position.z);
    }

    private ZeroPointRegion TrackZeroPointRegionXY(Vector2 propPosition, PropSide side)
    {
        ZeroPointRegion region = ZeroPointRegion.None;
        zeroPointStageUpdate = false;

        //check for local right
        if ((propPosition.x < localRightStartXY.x && propPosition.y > localRightStartXY.y)
            || (propPosition.x < localRightEndXY.x && propPosition.y < localRightEndXY.y))
        {
            region = ZeroPointRegion.LocalRight;
        }
        //check for local left
        else if ((propPosition.x > localLeftStartXY.x && propPosition.y > localLeftStartXY.y)
            || (propPosition.x > localLeftEndXY.x && propPosition.y < localLeftEndXY.y))
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
            //if prop not in a region, no need to check for passing zero points
            if (region == ZeroPointRegion.None)
            {
                return region;
            }
            //check for passing zero points
            bool inLeftOrRightRegion = (leftPropRegionXY == ZeroPointRegion.LocalRight || leftPropRegionXY == ZeroPointRegion.LocalLeft);
            bool inUpOrDownRegion = (leftPropRegionXY == ZeroPointRegion.LocalUp || leftPropRegionXY == ZeroPointRegion.LocalDown);

            bool passedZeroPointX = ((posLeftX > 0 && previousLeftX < 0) || (posLeftX < 0 && previousLeftX > 0));
            bool passedZeroPointY = ((posLeftY > 0 && previousLeftY < 0) || (posLeftY < 0 && previousLeftY > 0));

            if (inLeftOrRightRegion && passedZeroPointY)
            {
                zeroPointStageUpdate = true;
            }
            else if (inUpOrDownRegion && passedZeroPointX)
            {
                zeroPointStageUpdate = true;
            }
        }
        else if (side == PropSide.Right)
        {
            //temporarily store previous XY values last frame
            previousRightX_XY = posRightX_XY;
            previousRightY = posRightY;
            //update positions to current values this frame
            posRightX_XY = propPosition.x;
            posRightY = propPosition.y;
            //if prop not in a region, no need to check for passing zero points
            if (region == ZeroPointRegion.None)
            {
                return region;
            }
            //check for passing zero points
            bool inLeftOrRightRegion = (rightPropRegionXY == ZeroPointRegion.LocalRight || rightPropRegionXY == ZeroPointRegion.LocalLeft);
            bool inUpOrDownRegion = (rightPropRegionXY == ZeroPointRegion.LocalUp || rightPropRegionXY == ZeroPointRegion.LocalDown);

            bool passedZeroPointX = ((posRightX_XY > 0 && previousRightX_XY < 0) || (posRightX_XY < 0 && previousRightX_XY > 0));
            bool passedZeroPointY = ((posRightY > 0 && previousRightY < 0) || (posRightY < 0 && previousRightY > 0));

            if (inLeftOrRightRegion && passedZeroPointY)
            {
                zeroPointStageUpdate = true;
            }
            else if (inUpOrDownRegion && passedZeroPointX)
            {
                zeroPointStageUpdate = true;
            }
        }

        return region;
    }

    private ZeroPointRegion TrackZeroPointRegionXZ(Vector2 propPosition, PropSide side)
    {
        ZeroPointRegion region = ZeroPointRegion.None;
        
        //check for local forward
        if ((propPosition.x > localForwardStart.x && propPosition.y < localForwardStart.y)
            || (propPosition.x < localForwardEnd.x && propPosition.y < localForwardEnd.y))
        {
            region = ZeroPointRegion.LocalForward;
        }
        //check for local back
        else if ((propPosition.x > localBackStart.x && propPosition.y > localBackStart.y)
            || (propPosition.x < localBackEnd.x && propPosition.y > localBackEnd.y))
        {
            region = ZeroPointRegion.LocalBackward;
        }
        //check for local left
        else if ((propPosition.x > localLeftStartXZ.x && propPosition.y > localLeftStartXZ.y)
            || (propPosition.x > localLeftEndXZ.x && propPosition.y < localLeftEndXZ.y))
        {
            region = ZeroPointRegion.LocalLeft;
        }
        //check for local right
        else if ((propPosition.x < localRightStartXZ.x && propPosition.y > localRightStartXZ.y)
            || (propPosition.x < localRightEndXZ.x && propPosition.y < localRightEndXZ.y))
        {
            region = ZeroPointRegion.LocalRight;
        }

        if (side == PropSide.Right)
        {
            //temporarily store previous XY values last frame
            previousRightX_XZ = posRightX_XZ;
            previousRightZ = posRightZ;
            //update positions to current values this frame
            posRightX_XZ = propPosition.x;
            posRightZ = propPosition.y;

            if (region == ZeroPointRegion.None)
            {
                return region;
            }
            //check for passing zero points
            bool inLeftOrRightRegion = (rightPropRegionXY == ZeroPointRegion.LocalRight || rightPropRegionXY == ZeroPointRegion.LocalLeft);
            bool inForwardOrBackRegion = (rightPropRegionXZ == ZeroPointRegion.LocalForward || rightPropRegionXZ == ZeroPointRegion.LocalBackward);

            bool passedZeroPointX = ((posRightX_XZ > 0 && previousRightX_XZ < 0) || (posRightX_XZ < 0 && previousRightX_XZ > 0));
            bool passedZeroPointZ = ((posRightZ > 0 && previousRightZ < 0) || (posRightZ < 0 && previousRightZ > 0));

            if (inForwardOrBackRegion && passedZeroPointX)
            {
                zeroPointStageUpdate = true;
            }
            else if (inLeftOrRightRegion && passedZeroPointZ)
            {
                zeroPointStageUpdate = true;
            }
        }
        return region;
    }
}
