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
    public RotateBodyPart leftPropSpin;
    public TrailRenderer leftPropTrail;
    public Vector2 leftPropPosXY;
    public Vector2 leftPropPosXZ;
    public Vector2 leftPropPosYZ;
    public RotateBodyPart leftShoulderSpin;
    public RotateBodyPart leftForearmSpin;
    public RotateBodyPart leftArmSpin;
    public RotateBodyPart leftHandSpin;
    [Header("Right Arm Spins (Unity Sets)")]
    public RotateBodyPart rightPropSpin;
    public TrailRenderer rightPropTrail;
    public Vector2 rightPropPosXY;
    public Vector2 rightPropPosXZ;
    public Vector2 rightPropPosYZ;
    public RotateBodyPart rightShoulderSpin;
    public RotateBodyPart rightForearmSpin;
    public RotateBodyPart rightArmSpin;
    public RotateBodyPart rightHandSpin;
    [Header("other")]
    public string leftPropZeroPointRegionDebugText = "";
    public string rightPropZeroPointRegionDebugText = "";
    public float zeroPointPosition = 0f;
    //XY coords
    private Vector2 localRightStartXY = new Vector2(-55, -1);
    private Vector2 localRightEndXY = new Vector2(-55, 1);
    private Vector2 localLeftStartXY = new Vector2(55, -1);
    private Vector2 localLeftEndXY = new Vector2(55, 1);
    private Vector2 localDownStartXY = new Vector2(-1, -55);
    private Vector2 localDownEndXY = new Vector2(1, -55);
    private Vector2 localUpStartXY = new Vector2(-1, 55);
    private Vector2 localUpEndXY = new Vector2(1, 55);
    //XZ coords
    private Vector2 localRightStartXZ = new Vector2(-55, -1);
    private Vector2 localRightEndXZ = new Vector2(-55, 1);
    private Vector2 localLeftStartXZ = new Vector2(55, -1);
    private Vector2 localLeftEndXZ = new Vector2(55, -1);
    private Vector2 localForwardStartXZ = new Vector2(-1, -55);
    private Vector2 localForwardEndXZ = new Vector2(1, -55);
    private Vector2 localBackStartXZ = new Vector2(-1, 55);
    private Vector2 localBackEndXZ = new Vector2(1, 55);
    //YZ coords
    private Vector2 localForwardStartYZ = new Vector2(-1, -55);
    private Vector2 localForwardEndYZ = new Vector2(1, -55);
    private Vector2 localBackStartYZ = new Vector2(-1, 55);
    private Vector2 localBackEndYZ = new Vector2(1, 55);
    private Vector2 localUpStartYZ = new Vector2(55, -1);
    private Vector2 localUpEndYZ = new Vector2(55, 1);
    private Vector2 localDownStartYZ = new Vector2(-55, -1);
    private Vector2 localDownEndYZ = new Vector2(-55, 1);

    public ZeroPointRegion leftPropRegionXY;
    public ZeroPointRegion leftPropRegionXZ;
    public ZeroPointRegion leftPropRegionYZ;
    public ZeroPointRegion rightPropRegionXY;
    public ZeroPointRegion rightPropRegionXZ;
    public ZeroPointRegion rightPropRegionYZ;
    public bool zeroPointStageUpdate = false;

    //convert all of these into Vector2
    private float posLeftX = 0f;
    private float posLeftY = 0f;
    private float posLeftY_YZ;
    private float posLeftZ = 0f;
    private float posRightX_XY = 0f;
    private float posRightX_XZ = 0f;
    private float posRightX_YZ = 0f;
    private float posRightY = 0f;
    private float posRightY_XZ = 0f;
    private float posRightY_YZ = 0f;

    private float previousLeftX = 0f;
    private float previousLeftY = 0f;
    private float previosLeftZ = 0f;
    private float previousRightX_XY = 0f;
    private float previousRightX_XZ = 0f;
    private float previousRightX_YZ = 0f;
    private float previousRightY = 0f;
    private float previousRightZ_XZ = 0f;
    private float previousRightY_YZ = 0f;

    private void Awake()
    {
        torso = gameObject;
        if (instance != null)
        {
            Debug.LogWarning("More than one BodyParts instance!");
        }
        instance = this;

        //left side spins
        leftPropSpin = leftProp.GetComponent<RotateBodyPart>();
        leftPropTrail = leftProp.GetComponent<TrailRenderer>();
        leftShoulderSpin = leftShoulder.GetComponent<RotateBodyPart>();
        leftForearmSpin = leftForeArm.GetComponent<RotateBodyPart>();
        leftArmSpin = leftArm.GetComponent<RotateBodyPart>();
        leftHandSpin = leftHand.GetComponent<RotateBodyPart>();
        //right side spins
        rightPropSpin = rightProp.GetComponent<RotateBodyPart>();
        rightPropTrail = rightProp.GetComponent<TrailRenderer>();
        rightShoulderSpin = rightShoulder.GetComponent<RotateBodyPart>();
        rightForearmSpin = rightForeArm.GetComponent<RotateBodyPart>();
        rightArmSpin = rightArm.GetComponent<RotateBodyPart>();
        rightHandSpin = rightHand.GetComponent<RotateBodyPart>();
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

        rightPropRegionYZ = TrackZeroPointRegionYZ(rightPropPosYZ, PropSide.Right);

        leftPropZeroPointRegionDebugText = leftPropPosXY.x.ToString() + " , " + leftPropPosXY.y.ToString();
        rightPropZeroPointRegionDebugText = rightPropPosYZ.x.ToString() + " , " + rightPropPosYZ.y.ToString();
        //rightPropZeroPointRegionDebugText = rightProp.transform.position.x.ToString() + " , " + rightProp.transform.position.y.ToString() + " , " + rightProp.transform.position.z.ToString();
    }

    private void GetVector2PoiPosition()
    {
        leftPropPosXY = new Vector2(leftProp.transform.position.x, leftProp.transform.position.y);
        rightPropPosXY = new Vector2(rightProp.transform.position.x, rightProp.transform.position.y);

        leftPropPosXZ = new Vector2(leftProp.transform.position.x, leftProp.transform.position.z);
        rightPropPosXZ = new Vector2(rightProp.transform.position.x, rightProp.transform.position.z);

        leftPropPosYZ = new Vector2(leftProp.transform.position.y, leftProp.transform.position.z);
        rightPropPosYZ = new Vector2(rightProp.transform.position.y, rightProp.transform.position.z);
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
        else if ((propPosition.x > localDownStartXY.x && propPosition.y < localDownStartXY.y)
            || (propPosition.x < localDownEndXY.x && propPosition.y < localDownEndXY.y))
        {
            region = ZeroPointRegion.LocalDown;
        }
        //check for local up
        else if ((propPosition.x > localUpStartXY.x && propPosition.y > localUpStartXY.y)
            || (propPosition.x < localUpEndXY.x && propPosition.y > localUpEndXY.y))
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

            bool passedZeroPointX = ((posLeftX > zeroPointPosition && previousLeftX < zeroPointPosition) || (posLeftX < zeroPointPosition && previousLeftX > zeroPointPosition));
            bool passedZeroPointY = ((posLeftY > zeroPointPosition && previousLeftY < zeroPointPosition) || (posLeftY < zeroPointPosition && previousLeftY > zeroPointPosition));

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

            bool passedZeroPointX = ((posRightX_XY > zeroPointPosition && previousRightX_XY < zeroPointPosition) || (posRightX_XY < zeroPointPosition && previousRightX_XY > zeroPointPosition));
            bool passedZeroPointY = ((posRightY > zeroPointPosition && previousRightY < zeroPointPosition) || (posRightY < zeroPointPosition && previousRightY > zeroPointPosition));

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
        if ((propPosition.x > localForwardStartXZ.x && propPosition.y < localForwardStartXZ.y)
            || (propPosition.x < localForwardEndXZ.x && propPosition.y < localForwardEndXZ.y))
        {
            region = ZeroPointRegion.LocalForward;
        }
        //check for local back
        else if ((propPosition.x > localBackStartXZ.x && propPosition.y > localBackStartXZ.y)
            || (propPosition.x < localBackEndXZ.x && propPosition.y > localBackEndXZ.y))
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
            previousRightZ_XZ = posRightY_XZ;
            //update positions to current values this frame
            posRightX_XZ = propPosition.x;
            posRightY_XZ = propPosition.y;

            if (region == ZeroPointRegion.None)
            {
                return region;
            }
            //check for passing zero points
            bool inLeftOrRightRegion = (rightPropRegionXY == ZeroPointRegion.LocalRight || rightPropRegionXY == ZeroPointRegion.LocalLeft);
            bool inForwardOrBackRegion = (rightPropRegionXZ == ZeroPointRegion.LocalForward || rightPropRegionXZ == ZeroPointRegion.LocalBackward);

            bool passedZeroPointX = ((posRightX_XZ > zeroPointPosition && previousRightX_XZ < zeroPointPosition) || (posRightX_XZ < zeroPointPosition && previousRightX_XZ > zeroPointPosition));
            bool passedZeroPointZ = ((posRightY_XZ > zeroPointPosition && previousRightZ_XZ < zeroPointPosition) || (posRightY_XZ < zeroPointPosition && previousRightZ_XZ > zeroPointPosition));

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

    private ZeroPointRegion TrackZeroPointRegionYZ(Vector2 propPosition, PropSide side)
    {
        ZeroPointRegion region = ZeroPointRegion.None;

        //check for local down
        if ((propPosition.x < localDownStartYZ.x && propPosition.y > localDownStartYZ.y)
            || (propPosition.x < localDownEndYZ.x && propPosition.y < localDownEndYZ.y))
        {
            region = ZeroPointRegion.LocalDown;
        }
        //check for local up
        else if ((propPosition.x > localUpStartYZ.x && propPosition.y > localUpStartYZ.y)
            || (propPosition.x > localUpEndYZ.x && propPosition.y < localUpEndYZ.y))
        {
            region = ZeroPointRegion.LocalUp;
        }
        //check for local forward
        else if ((propPosition.x > localForwardStartYZ.x && propPosition.y < localForwardStartYZ.y)
            || (propPosition.x < localForwardEndYZ.x && propPosition.y < localForwardEndYZ.y))
        {
            region = ZeroPointRegion.LocalForward;
        }
        //check for local back
        else if ((propPosition.x > localBackStartYZ.x && propPosition.y > localBackStartYZ.y)
            || (propPosition.x < localBackEndYZ.x && propPosition.y > localBackEndYZ.y))
        {
            region = ZeroPointRegion.LocalBackward;
        }

        if (side == PropSide.Right)
        {
            //temporarily store previous XY values last frame
            previousRightX_YZ = posRightX_YZ;
            previousRightY_YZ = posRightY_YZ;
            //update positions to current values this frame
            posRightX_YZ = propPosition.x;
            posRightY_YZ = propPosition.y;
        }
        //check for passing zero points
        bool inUpOrDownRegion = (rightPropRegionYZ == ZeroPointRegion.LocalUp || rightPropRegionYZ == ZeroPointRegion.LocalDown);
        bool inForwardOrBackRegion = (rightPropRegionYZ == ZeroPointRegion.LocalForward || rightPropRegionYZ == ZeroPointRegion.LocalBackward);

        bool passedZeroPointX = ((posRightX_YZ > zeroPointPosition && previousRightX_YZ < zeroPointPosition) || (posRightX_YZ < zeroPointPosition && previousRightX_YZ > zeroPointPosition));
        bool passedZeroPointZ = ((posRightY_YZ > zeroPointPosition && previousRightY_YZ < zeroPointPosition) || (posRightY_YZ < zeroPointPosition && previousRightY_YZ > zeroPointPosition));

        if (inForwardOrBackRegion && passedZeroPointX)
        {
            zeroPointStageUpdate = true;
        }
        else if (inUpOrDownRegion && passedZeroPointZ)
        {
            zeroPointStageUpdate = true;
        }

        return region;
    }
}
