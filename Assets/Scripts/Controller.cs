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
    public Dropdown propDropDropdown;
    public GameObject spinnerWallPlane;
    public GameObject spinnerWheelPlane;
    [Header("Viewing Camera")]
    public Camera cam;

    private GameObject thisSpinner;

    private EnvironmentVariables envVariables;
    private BodyParts bodyParts;
    //private CardinalPoints cardinalPoints;

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

        propDropDropdown.onValueChanged.AddListener(delegate
        {
            PropDropdownValueChanged(propDropDropdown);
        });

        envVariables = EnvironmentVariables.instance;
        bodyParts = BodyParts.instance;
        //cardinalPoints = CardinalPoints.instance;
    }

    private void Update()
    {
        //Antispin wall Plane flower
        if (Input.GetKeyDown("e"))
        {
            AntiSpinWallPlaneFlower();
        }
        //Inspin vs antispin wheel Plane flower Split/Ops
        if (Input.GetKeyDown("r"))
        {
            InspinAntispinWheelPlaneFlowerSplitOps();
        }
        //Inspin vs antispin wheel Plane flower Same/Ops
        if (Input.GetKeyDown("t"))
        {
            InspinAntispinWheelPlaneFlowerSameOps();
        }
        //Inspin vs antispin wheel Plane flower Same/Same
        if (Input.GetKeyDown("u"))
        {
            InspinAntispinWheelPlaneFlowerSameSame();
        }
        //Inspin vs antispin wheel Plane flower Split/Same
        if (Input.GetKeyDown("y"))
        {
            InspinAntispinWheelPlaneFlowerSplitSame();
        }

        if (setupAntiSpinWallPlaneFlower || doingAntiSpinWallPlaneFlower)
        {
            AntiSpinWallPlaneFlower();
        }
        else if (setupInspinAntispinWheelPlaneFlowerSplitOps || doingInspinAntispinWheelPlaneFlowerSplitOps)
        {
            InspinAntispinWheelPlaneFlowerSplitOps();
        }
        else if (setupInspinAntispinWheelPlaneFlowerSameOps || doingInspinAntispinWheelPlaneFlowerSameOps)
        {
            InspinAntispinWheelPlaneFlowerSameOps();
        }
        else if (setupInspinAntispinWheelPlaneFlowerSameSame || doingInspinAntispinWheelPlaneFlowerSameSame)
        {
            InspinAntispinWheelPlaneFlowerSameSame();
        }
        else if (setupInspinAntispinWheelPlaneFlowerSplitSame || doingInspinAntispinWheelPlaneFlowerSplitSame)
        {
            InspinAntispinWheelPlaneFlowerSplitSame();
        }
        else
        {
            return;
        }
    }

    public void AntiSpinWallPlaneFlower()
    {
        if (!setupAntiSpinWallPlaneFlower && !doingAntiSpinWallPlaneFlower)
        {
            ClearSpinner();
            SetSpinner(spinnerWallPlane);
            setupAntiSpinWallPlaneFlower = true;
        }       

        if (doingAntiSpinWallPlaneFlower)
        {
            //faster global speeds means less sensitivity to phase points. Otherwise, it may skip stage increments
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
        if (setupAntiSpinWallPlaneFlower)
        {
            //need to give it a cycle for the new spinner to be instantiated
            doingAntiSpinWallPlaneFlower = true;
            setupAntiSpinWallPlaneFlower = false;

            thisSimulationName.text = "Wall Plane Antispin Flower";
        }

        
        
    }

    public void InspinAntispinWheelPlaneFlowerSplitOps()
    {
        if (!setupInspinAntispinWheelPlaneFlowerSplitOps && !doingInspinAntispinWheelPlaneFlowerSplitOps)
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
            //put left hand (inSpin hand) in Inspin starting location
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, new Vector3(0, 0, 180f), 180f);
            //need to give it a cycle for the new spinner to be instantiated
            setupInspinAntispinWheelPlaneFlowerSplitOps = false;
            doingInspinAntispinWheelPlaneFlowerSplitOps = true;

            thisSimulationName.text = "Wheel Plane Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Split Time / Opposite Direction";
        }
    }

    public void InspinAntispinWheelPlaneFlowerSameOps()
    {
        if (!setupInspinAntispinWheelPlaneFlowerSameOps && !doingInspinAntispinWheelPlaneFlowerSameOps)
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
            //put left hand (inSpin hand) in Inspin starting location
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, new Vector3(0, 0, 180f), 180f);
            //rotate shoulders to have right hand down and left hand up
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            //need to give it a cycle for the new spinner to be instantiated
            setupInspinAntispinWheelPlaneFlowerSameOps = false;
            doingInspinAntispinWheelPlaneFlowerSameOps = true;

            thisSimulationName.text = "Wheel Plane Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Same Time / Opposite Direction";
        }
    }

    public void InspinAntispinWheelPlaneFlowerSameSame()
    {
        if (!setupInspinAntispinWheelPlaneFlowerSameSame && !doingInspinAntispinWheelPlaneFlowerSameSame)
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
            //rotate shoulders to lower position
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), -90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            //need to give it a cycle for the new spinner to be instantiated
            setupInspinAntispinWheelPlaneFlowerSameSame = false;
            doingInspinAntispinWheelPlaneFlowerSameSame = true;

            thisSimulationName.text = "Wheel Plane Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Same Time / Same Direction";
        }
    }

    public void InspinAntispinWheelPlaneFlowerSplitSame()
    {
        if (!setupInspinAntispinWheelPlaneFlowerSplitSame && !doingInspinAntispinWheelPlaneFlowerSplitSame)
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
            //put left hand (inSpin hand) in Inspin starting location
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, new Vector3(0, 0, 180f), 180f);
            //rotate shoulders to lower position
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), -90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            //need to give it a cycle for the new spinner to be instantiated
            setupInspinAntispinWheelPlaneFlowerSplitSame = false;
            doingInspinAntispinWheelPlaneFlowerSplitSame = true;

            thisSimulationName.text = "Wheel Plane Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Split Time / Same Direction";
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

    private void PropDropdownValueChanged(Dropdown change)
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
            envVariables.bodyParts = null;
            Destroy(thisSpinner);
            thisSpinner = null;

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
        //cardinalPoints = CardinalPoints.instance;
        envVariables.bodyParts = BodyParts.instance;

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
