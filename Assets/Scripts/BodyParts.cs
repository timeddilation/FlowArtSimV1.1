using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BodyParts : MonoBehaviour
{    
    private EnvironmentVariables environmentVariables;
    private readonly float baseRotationSpeed = 1f;
    private Animator animator;
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
    public RotateBodyPart torsoSpin;
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
    [Header("Other")]
    public string leftPropZeroPointRegionDebugText = "";
    public string rightPropZeroPointRegionDebugText = "";
    public float zeroPointPosition = 0f;
    private float directionModifier = 1f;

    private void Awake()
    {
        torso = gameObject;
        if (instance != null)
        {
            Debug.LogWarning("More than one BodyParts instance!");
        }
        instance = this;

        animator = gameObject.GetComponent<Animator>();
        //torso spins
        torsoSpin = torso.GetComponent<RotateBodyPart>();
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
        environmentVariables = EnvironmentVariables.instance; //need for global speed scale value
        GetVector2PoiPosition();
    }

    private void Update()
    {
        GetVector2PoiPosition();

        leftPropZeroPointRegionDebugText = leftProp.transform.position.ToString();
        rightPropZeroPointRegionDebugText = rightProp.transform.position.ToString();
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
    public void InvertDirection()
    {
        directionModifier = directionModifier * -1;
        animator.SetBool("Reverse", !animator.GetBool("Reverse"));
    }
    public void RotateBodyPartRelative(GameObject rotatingPart, GameObject rotatePoint, SpinDirections dir, float rotationSpeedModifier = 1f)
    {
        switch (dir)
        {
            case SpinDirections.Forward:
                rotatingPart.transform.RotateAround(
                    rotatePoint.transform.position,
                    Vector3.forward,
                    baseRotationSpeed * rotationSpeedModifier * directionModifier * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Backward:
                rotatingPart.transform.RotateAround(
                    rotatePoint.transform.position,
                    Vector3.back,
                    baseRotationSpeed * rotationSpeedModifier * directionModifier * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Left:
                rotatingPart.transform.RotateAround(
                    rotatePoint.transform.position,
                    Vector3.left,
                    baseRotationSpeed * rotationSpeedModifier * directionModifier * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Right:
                rotatingPart.transform.RotateAround(
                    rotatePoint.transform.position,
                    Vector3.right,
                    baseRotationSpeed * rotationSpeedModifier * directionModifier * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Up:
                rotatingPart.transform.RotateAround(
                    rotatePoint.transform.position,
                    Vector3.up,
                    baseRotationSpeed * rotationSpeedModifier * directionModifier * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Down:
                rotatingPart.transform.RotateAround(
                    rotatePoint.transform.position,
                    Vector3.down,
                    baseRotationSpeed * rotationSpeedModifier * directionModifier * environmentVariables.globalSpeed);
                break;
            default:
                break;
        }
    }
    public void SetTrickById(int id)
    {
        animator.SetInteger("TrickId", id);
    }
}
