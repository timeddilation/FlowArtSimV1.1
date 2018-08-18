using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    #region Variable Setups
    [Header("Display Texts")]
    public Text thisSimulationName;
    public Text thisSimulationDescription;
    [Header("Spinner Prefabs")]
    public GameObject spinnerWallPlain;
    public GameObject spinnerWheelPlain;
    [Header("Viewing Camera")]
    public Camera cam;

    private GameObject thisSpinner;

    private EnvironmentVariables envVariables;
    private BodyParts bodyParts;
    private CardinalPoints cardinalPoints;

    private float trailSpeedModifier = 1f;

    private bool setupAntiSpinWallPlainFlower = false;
    private bool doingAntiSpinWallPlainFlower = false;

    private bool setupInspinAntispinWheelPlainFlowerSplitOps = false;
    private bool doingInspinAntispinWheelPlainFlowerSplitOps = false;

    private bool setupInspinAntispinWheelPlainFlowerSameOps = false;
    private bool doingInspinAntispinWheelPlainFlowerSameOps = false;

    private bool setupInspinAntispinWheelPlainFlowerSameSame = false;
    private bool doingInspinAntispinWheelPlainFlowerSameSame = false;

    private bool setupInspinAntispinWheelPlainFlowerSplitSame = false;
    private bool doingInspinAntispinWheelPlainFlowerSplitSame = false;

    private int trickStage = 0;
    private float zeroPointProximitySensitivity = 0.5f;
    #endregion

    private void Start()
    {
        thisSpinner = GameObject.FindGameObjectWithTag("Player");       

        envVariables = EnvironmentVariables.instance;
        bodyParts = BodyParts.instance;
        cardinalPoints = CardinalPoints.instance;

        InvokeRepeating("UpdatePoiTrailSpeed", 0f, 1f);
    }

    private void Update()
    {
        #region Antispin Wall Plain Flower 
        //Antispin wall plain flower
        if (Input.GetKeyDown("e"))
        {
            ClearSpinner();
            SetSpinner(spinnerWallPlain);
            setupAntiSpinWallPlainFlower = true;
        }
        if (doingAntiSpinWallPlainFlower)
        {
            AntiSpinWallPlainFlower();
        }
        if (setupAntiSpinWallPlainFlower)
        {
            //set poi trail speed
            UpdatePoiTrailSpeed();
            //need to give it a cycle for the new spinner to be instantiated
            doingAntiSpinWallPlainFlower = true;
            setupAntiSpinWallPlainFlower = false;

            thisSimulationName.text = "Wall Plain Antispin Flower";
        }
        #endregion

        #region Inspin vs antispin wheel plain flower Split/Ops
        //Inspin vs antispin wheel plain flower Split/Ops
        if (Input.GetKeyDown("r"))
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlain);
            setupInspinAntispinWheelPlainFlowerSplitOps = true;
        }
        if (doingInspinAntispinWheelPlainFlowerSplitOps)
        {
            InspinAntispinWheelPlainFlowerOpsDirection();
        }
        if (setupInspinAntispinWheelPlainFlowerSplitOps)
        {
            //double length of trails
            trailSpeedModifier = 2f;
            //put left hand (inSpin hand) in Inspin starting location
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, new Vector3(0, 0, 180f), 180f);
            //set poi trail speed
            UpdatePoiTrailSpeed();
            //need to give it a cycle for the new spinner to be instantiated
            setupInspinAntispinWheelPlainFlowerSplitOps = false;
            doingInspinAntispinWheelPlainFlowerSplitOps = true;

            thisSimulationName.text = "Wheel Plain Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Split Time / Opposite Direction";
        }
        #endregion

        #region Inspin vs antispin wheel plain flower Same/Ops
        if (Input.GetKeyDown("t"))
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlain);
            setupInspinAntispinWheelPlainFlowerSameOps = true;
        }
        if (doingInspinAntispinWheelPlainFlowerSameOps)
        {
            InspinAntispinWheelPlainFlowerOpsDirection();
        }
        if (setupInspinAntispinWheelPlainFlowerSameOps)
        {
            //double length of trails
            trailSpeedModifier = 2f;
            //put left hand (inSpin hand) in Inspin starting location
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, new Vector3(0, 0, 180f), 180f);
            //rotate shoulders to have right hand down and left hand up
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            //set poi trail speed
            UpdatePoiTrailSpeed();
            //need to give it a cycle for the new spinner to be instantiated
            setupInspinAntispinWheelPlainFlowerSameOps = false;
            doingInspinAntispinWheelPlainFlowerSameOps = true;

            thisSimulationName.text = "Wheel Plain Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Same Time / Opposite Direction";
        }
        #endregion

        #region Inspin vs antispin wheel plain flower Same/Same
        //Inspin vs antispin wheel plain flower Same/Same
        if (Input.GetKeyDown("u"))
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlain);
            setupInspinAntispinWheelPlainFlowerSameSame = true;
        }
        if (doingInspinAntispinWheelPlainFlowerSameSame)
        {
            InspinAntispinWheelPlainFlowerSameDirection();
        }
        if (setupInspinAntispinWheelPlainFlowerSameSame)
        {
            //double length of trails
            trailSpeedModifier = 2f;
            //rotate shoulders to lower position
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), -90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            //set poi trail speed
            UpdatePoiTrailSpeed();
            //need to give it a cycle for the new spinner to be instantiated
            setupInspinAntispinWheelPlainFlowerSameSame = false;
            doingInspinAntispinWheelPlainFlowerSameSame = true;

            thisSimulationName.text = "Wheel Plain Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Same Time / Same Direction";
        }
        #endregion

        #region Inspin vs antispin wheel plain flower Split/Same
        //Inspin vs antispin wheel plain flower Split/Same
        if (Input.GetKeyDown("y"))
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlain);
            setupInspinAntispinWheelPlainFlowerSplitSame = true;
        }
        if (doingInspinAntispinWheelPlainFlowerSplitSame)
        {
            InspinAntispinWheelPlainFlowerSameDirection();
        }
        if (setupInspinAntispinWheelPlainFlowerSplitSame)
        {
            //double length of trails
            trailSpeedModifier = 2f;
            //put left hand (inSpin hand) in Inspin starting location
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, new Vector3(0, 0, 180f), 180f);
            //rotate shoulders to lower position
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), -90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            //set poi trail speed
            UpdatePoiTrailSpeed();
            //need to give it a cycle for the new spinner to be instantiated
            setupInspinAntispinWheelPlainFlowerSplitSame = false;
            doingInspinAntispinWheelPlainFlowerSplitSame = true;

            thisSimulationName.text = "Wheel Plain Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Split Time / Same Direction";
        }
        #endregion
    }

    private void AntiSpinWallPlainFlower()
    {
        SetZeroPointProximitySensitivity();

        if (trickStage == 0)
        {
            //set poi speed modifier to 4 to get 4 zero points on each arm rotation
            bodyParts.leftPropSpin.rotationSpeedModifier = 4f;
            bodyParts.rightPropSpin.rotationSpeedModifier = 4f;
            //spin poi
            bodyParts.leftPropSpin.SpinPoi(bodyParts.leftHand, "forward");
            bodyParts.rightPropSpin.SpinPoi(bodyParts.rightHand, "forward");
            //rotate shoulders
            bodyParts.leftShoulderSpin.SpinShoulderAroundTorso(bodyParts.torso, "back");
            bodyParts.rightShoulderSpin.SpinShoulderAroundTorso(bodyParts.torso, "back");

            //right hand is at upper position           
            if ((270 - bodyParts.rightShoulder.gameObject.transform.rotation.eulerAngles.z) < zeroPointProximitySensitivity
                && (270 - bodyParts.rightShoulder.gameObject.transform.rotation.eulerAngles.z) > -zeroPointProximitySensitivity)
            {
                ++trickStage;
            }
        }
        else if (trickStage == 1)
        {
            //no need to apply rotation force to poi, we want them to follow the hands, relative velocity is zero
            //rotate shoulders
            bodyParts.leftShoulderSpin.SpinShoulderAroundTorso(bodyParts.torso, "forward");
            bodyParts.rightShoulderSpin.SpinShoulderAroundTorso(bodyParts.torso, "forward");

            //right hand is at lower position
            if ((90 - bodyParts.rightShoulder.gameObject.transform.rotation.eulerAngles.z) > -zeroPointProximitySensitivity
                && (90 - bodyParts.rightShoulder.gameObject.transform.rotation.eulerAngles.z) < zeroPointProximitySensitivity)
            {
                trickStage = 0;
            }
        }
        
    }

    private void InspinAntispinWheelPlainFlowerSameDirection()
    {
        //set poi speed modifier to 4 to get 4 zero points on each arm rotation
        bodyParts.leftPropSpin.rotationSpeedModifier = 2f;
        bodyParts.rightPropSpin.rotationSpeedModifier = 4f;
        //spin poi
        bodyParts.leftPropSpin.SpinPoi(bodyParts.leftHand, "back");
        bodyParts.rightPropSpin.SpinPoi(bodyParts.rightHand, "back");
        //rotate shoulders
        bodyParts.leftShoulderSpin.SpinShoulderAroundTorso(bodyParts.torso, "back");
        bodyParts.rightShoulderSpin.SpinShoulderAroundTorso(bodyParts.torso, "forward");
    }

    private void InspinAntispinWheelPlainFlowerOpsDirection()
    {
        //set poi speed modifier to 4 to get 4 zero points on each arm rotation
        bodyParts.leftPropSpin.rotationSpeedModifier = 2f;
        bodyParts.rightPropSpin.rotationSpeedModifier = 4f;
        //spin poi
        bodyParts.leftPropSpin.SpinPoi(bodyParts.leftHand, "back");
        bodyParts.rightPropSpin.SpinPoi(bodyParts.rightHand, "forward");
        //rotate shoulders
        bodyParts.leftShoulderSpin.SpinShoulderAroundTorso(bodyParts.torso, "back");
        bodyParts.rightShoulderSpin.SpinShoulderAroundTorso(bodyParts.torso, "back");
    }

    private void ClearSpinner()
    {
        if (thisSpinner != null)
        {
            BodyParts.instance = null;
            CardinalPoints.instance = null;
            Destroy(thisSpinner);
            thisSpinner = null;

            trailSpeedModifier = 1f;
            trickStage = 0;
            doingAntiSpinWallPlainFlower = false;
            doingInspinAntispinWheelPlainFlowerSplitOps = false;
            doingInspinAntispinWheelPlainFlowerSameOps = false;
            doingInspinAntispinWheelPlainFlowerSameSame = false;
            doingInspinAntispinWheelPlainFlowerSplitSame = false;

            thisSimulationName.text = "";
            thisSimulationDescription.text = "";
        }
    }

    private void SetSpinner(GameObject spinnerPrefab)
    {
        thisSpinner = Instantiate(spinnerPrefab);
        thisSpinner.SetActive(true);
        bodyParts = BodyParts.instance;
        cardinalPoints = CardinalPoints.instance;

        if (spinnerPrefab == spinnerWheelPlain)
        {
            cam.gameObject.transform.position = new Vector3(0f, 20f, -120f);
            cam.orthographicSize = 70f;
        }
        else if (spinnerPrefab == spinnerWallPlain)
        {
            cam.gameObject.transform.position = new Vector3(0f, 20f, -160f);
            cam.orthographicSize = 75;
        }
    }

    private void UpdatePoiTrailSpeed()
    {
        bodyParts.leftPropTrail.time = envVariables.poiTrailSpeed * trailSpeedModifier;
        bodyParts.rightPropTrail.time = envVariables.poiTrailSpeed * trailSpeedModifier;
    }

    private void SetZeroPointProximitySensitivity()
    {
        zeroPointProximitySensitivity = envVariables.globalSpeed;
        zeroPointProximitySensitivity = zeroPointProximitySensitivity * 0.8f;
        if (zeroPointProximitySensitivity < 0.5f)
        {
            zeroPointProximitySensitivity = 0.5f;
        }
    }
}
