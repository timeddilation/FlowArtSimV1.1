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
    public Dropdown propDropdown;
    public GameObject spinnerWallPlane;
    public GameObject spinnerWheelPlane;
    [Header("Viewing Camera")]
    public Camera cam;

    private GameObject thisSpinner;

    private EnvironmentVariables envVariables;
    private BodyParts bodyParts;
    private CardinalPoints cardinalPoints;

    private float trailSpeedModifier = 1f;

    private bool setupAntiSpinWallPlaneFlower = false;
    private bool doingAntiSpinWallPlaneFlower = false;

    private bool setupInspinAntispinWheelPlaneFlowerSplitOps = false;
    private bool doingInspinAntispinWheelPlaneFlowerSplitOps = false;

    private bool setupInspinAntispinWheelPlaneFlowerSameOps = false;
    private bool doingInspinAntispinWheelPlaneFlowerSameOps = false;

    private bool setupInspinAntispinWheelPlaneFlowerSameSame = false;
    private bool doingInspinAntispinWheelPlaneFlowerSameSame = false;

    private bool setupInspinAntispinWheelPlaneFlowerSplitSame = false;
    private bool doingInspinAntispinWheelPlaneFlowerSplitSame = false;

    private int trickStage = 0;
    private float zeroPointProximitySensitivity = 0.5f;
    #endregion

    private void Start()
    {
        thisSpinner = GameObject.FindGameObjectWithTag("Player");

        propDropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(propDropdown);
        });

        envVariables = EnvironmentVariables.instance;
        bodyParts = BodyParts.instance;
        cardinalPoints = CardinalPoints.instance;

        InvokeRepeating("UpdatePoiTrailSpeed", 0f, 1f);
    }

    private void Update()
    {
        #region Antispin Wall Plane Flower 
        //Antispin wall Plane flower
        if (Input.GetKeyDown("e"))
        {
            ClearSpinner();
            SetSpinner(spinnerWallPlane);
            setupAntiSpinWallPlaneFlower = true;
        }
        if (doingAntiSpinWallPlaneFlower)
        {
            AntiSpinWallPlaneFlower();
        }
        if (setupAntiSpinWallPlaneFlower)
        {
            //set poi trail speed
            UpdatePoiTrailSpeed();
            //need to give it a cycle for the new spinner to be instantiated
            doingAntiSpinWallPlaneFlower = true;
            setupAntiSpinWallPlaneFlower = false;

            thisSimulationName.text = "Wall Plane Antispin Flower";
        }
        #endregion

        #region Inspin vs antispin wheel Plane flower Split/Ops
        //Inspin vs antispin wheel Plane flower Split/Ops
        if (Input.GetKeyDown("r"))
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupInspinAntispinWheelPlaneFlowerSplitOps = true;
        }
        if (doingInspinAntispinWheelPlaneFlowerSplitOps)
        {
            InspinAntispinWheelPlaneFlowerOpsDirection();
        }
        if (setupInspinAntispinWheelPlaneFlowerSplitOps)
        {
            //double length of trails
            trailSpeedModifier = 2f;
            //put left hand (inSpin hand) in Inspin starting location
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, new Vector3(0, 0, 180f), 180f);
            //set poi trail speed
            UpdatePoiTrailSpeed();
            //need to give it a cycle for the new spinner to be instantiated
            setupInspinAntispinWheelPlaneFlowerSplitOps = false;
            doingInspinAntispinWheelPlaneFlowerSplitOps = true;

            thisSimulationName.text = "Wheel Plane Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Split Time / Opposite Direction";
        }
        #endregion

        #region Inspin vs antispin wheel Plane flower Same/Ops
        if (Input.GetKeyDown("t"))
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupInspinAntispinWheelPlaneFlowerSameOps = true;
        }
        if (doingInspinAntispinWheelPlaneFlowerSameOps)
        {
            InspinAntispinWheelPlaneFlowerOpsDirection();
        }
        if (setupInspinAntispinWheelPlaneFlowerSameOps)
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
            setupInspinAntispinWheelPlaneFlowerSameOps = false;
            doingInspinAntispinWheelPlaneFlowerSameOps = true;

            thisSimulationName.text = "Wheel Plane Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Same Time / Opposite Direction";
        }
        #endregion

        #region Inspin vs antispin wheel Plane flower Same/Same
        //Inspin vs antispin wheel Plane flower Same/Same
        if (Input.GetKeyDown("u"))
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupInspinAntispinWheelPlaneFlowerSameSame = true;
        }
        if (doingInspinAntispinWheelPlaneFlowerSameSame)
        {
            InspinAntispinWheelPlaneFlowerSameDirection();
        }
        if (setupInspinAntispinWheelPlaneFlowerSameSame)
        {
            //double length of trails
            trailSpeedModifier = 2f;
            //rotate shoulders to lower position
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), -90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            //set poi trail speed
            UpdatePoiTrailSpeed();
            //need to give it a cycle for the new spinner to be instantiated
            setupInspinAntispinWheelPlaneFlowerSameSame = false;
            doingInspinAntispinWheelPlaneFlowerSameSame = true;

            thisSimulationName.text = "Wheel Plane Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Same Time / Same Direction";
        }
        #endregion

        #region Inspin vs antispin wheel Plane flower Split/Same
        //Inspin vs antispin wheel Plane flower Split/Same
        if (Input.GetKeyDown("y"))
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupInspinAntispinWheelPlaneFlowerSplitSame = true;
        }
        if (doingInspinAntispinWheelPlaneFlowerSplitSame)
        {
            InspinAntispinWheelPlaneFlowerSameDirection();
        }
        if (setupInspinAntispinWheelPlaneFlowerSplitSame)
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
            setupInspinAntispinWheelPlaneFlowerSplitSame = false;
            doingInspinAntispinWheelPlaneFlowerSplitSame = true;

            thisSimulationName.text = "Wheel Plane Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Split Time / Same Direction";
        }
        #endregion
    }

    private void AntiSpinWallPlaneFlower()
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

    private void InspinAntispinWheelPlaneFlowerSameDirection()
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

    private void InspinAntispinWheelPlaneFlowerOpsDirection()
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

    private void DropdownValueChanged(Dropdown change)
    {
        SetSpinnerProps(change.captionText.text);
        ClearSpinner();
        SetSpinner(spinnerWallPlane);
    }

    private void SetSpinnerProps(string propName)
    {
        if (propName == "Hoops")
        {
            spinnerWallPlane = envVariables.hooperWallPlane;
            spinnerWheelPlane = envVariables.hooperWheelPlane;
        }
        else if (propName == "Poi")
        {
            spinnerWallPlane = envVariables.poiWallPlane;
            spinnerWheelPlane = envVariables.poiWheelPlane;
        }
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
            doingAntiSpinWallPlaneFlower = false;
            doingInspinAntispinWheelPlaneFlowerSplitOps = false;
            doingInspinAntispinWheelPlaneFlowerSameOps = false;
            doingInspinAntispinWheelPlaneFlowerSameSame = false;
            doingInspinAntispinWheelPlaneFlowerSplitSame = false;

            thisSimulationName.text = "Not Currently Simulating";
            thisSimulationDescription.text = "";
        }
    }

    private void SetSpinner(GameObject spinnerPrefab)
    {
        thisSpinner = Instantiate(spinnerPrefab);
        thisSpinner.SetActive(true);
        bodyParts = BodyParts.instance;
        cardinalPoints = CardinalPoints.instance;

        if (spinnerPrefab == spinnerWheelPlane)
        {
            cam.gameObject.transform.position = new Vector3(0f, 20f, -120f);
            cam.orthographicSize = 70f;
        }
        else if (spinnerPrefab == spinnerWallPlane)
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
