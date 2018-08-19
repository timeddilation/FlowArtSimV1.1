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
    }

    private void GetVector2PoiPosition()
    {
        leftPropPos.x = leftProp.transform.position.x;
        leftPropPos.y = leftProp.transform.position.y;

        rightPropPos.x = rightProp.transform.position.x;
        rightPropPos.y = rightProp.transform.position.y;
    }
}
