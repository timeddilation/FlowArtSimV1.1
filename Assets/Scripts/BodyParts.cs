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
    public PoiSpins leftPropSpin;
    public TrailRenderer leftPropTrail;
    public Vector2 leftPropPos;
    public ShoulderSpins leftShoulderSpin;
    [Header("Right Arm Spins (Unity Sets)")]
    public PoiSpins rightPropSpin;
    public TrailRenderer rightPropTrail;
    public Vector2 rightPropPos;
    public ShoulderSpins rightShoulderSpin;

    private void Awake()
    {
        torso = gameObject;
        instance = this;

        //left side spins
        leftPropSpin = leftProp.GetComponent<PoiSpins>();
        leftPropTrail = leftProp.GetComponent<TrailRenderer>();
        leftShoulderSpin = leftShoulder.GetComponent<ShoulderSpins>();
        //right side spins
        rightPropSpin = rightProp.GetComponent<PoiSpins>();
        rightPropTrail = rightProp.GetComponent<TrailRenderer>();
        rightShoulderSpin = rightShoulder.GetComponent<ShoulderSpins>();

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
