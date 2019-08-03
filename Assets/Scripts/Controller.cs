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
    public Canvas canvas;
    public Text debugMenu;
    [Header("Spinner Prefabs")]
    public Dropdown propDropDropdown;
    public GameObject spinnerWallPlane;
    public GameObject spinnerWheelPlane;
    [Header("Viewing Camera")]
    public Camera cam;
    private Transform camTransform;

    private GameObject thisSpinner;
    private EnvironmentVariables envVariables;
    private BodyParts bodyParts;
    #endregion
    #region State Engine
    //TODO: Build state engine

    private bool setupTestTrick = false;
    private bool doingTestTrick = false;

    private bool setupAntiSpinPointsSameOps = false;
    private bool doingAntiSpinPointsSameOps = false;

    private bool setupAntiSpinPointsSplitOps = false;
    private bool doingAntiSpinPointsSplitOps = false;

    private bool setupAntiSpinWallPlaneFlower = false;
    private bool doingAntiSpinWallPlaneFlower = false;

    private bool setupAntiSpinWallPlaneFlowerWithInSpin = false;
    private bool doingAntiSpinWallPlaneFlowerWithInSpin = false;

    private bool setupStandardTriquetra = false;
    private bool doingStandardTriquetra = false;

    private bool setupButterflyTaceVertical = false;
    private bool doingButterflyTaceVertical = false;

    private bool setupAntispinWheelPlaneFlowerSameOps = false;
    private bool doingAntispinWheelPlaneFlowerSameOps = false;

    private bool setupAntispinWheelPlaneFlowerSplitSame = false;
    private bool doingAntispinWheelPlaneFlowerSplitSame = false;

    private bool setupInspinWheelPlaneFlowerSameSame = false;
    private bool doingInspinWheelPlaneFlowerSameSame = false;

    private bool setupInspinAntispinWheelPlaneFlowerSplitOps = false;
    private bool doingInspinAntispinWheelPlaneFlowerSplitOps = false;

    private bool setupInspinAntispinWheelPlaneFlowerSameOps = false;
    private bool doingInspinAntispinWheelPlaneFlowerSameOps = false;

    private bool setupInspinAntispinWheelPlaneFlowerSameSame = false;
    private bool doingInspinAntispinWheelPlaneFlowerSameSame = false;

    private bool setupInspinAntispinWheelPlaneFlowerSplitSame = false;
    private bool doingInspinAntispinWheelPlaneFlowerSplitSame = false;

    private bool setupThreeDimFlowerXZ = false;
    private bool doingThreeDimFlowerXZ = false;

    private bool setupThreeDimDrills = false;
    private bool doingThreeDimDrills = false;

    private int trickStage = 0;
    private int trickCycles = 0;
    //private float intermidiateStageRotationCounter = 0f;
    #endregion

    private void Start()
    {
        thisSpinner = GameObject.FindGameObjectWithTag("Player");

        camTransform = cam.gameObject.transform;

        propDropDropdown.onValueChanged.AddListener(delegate
        {
            PropDropdownValueChanged(propDropDropdown);
        });

        envVariables = EnvironmentVariables.instance;
        bodyParts = BodyParts.instance;

        SetSpinnerProps("Hoops");
    }

    private void Update()
    {
        UpdateDebugMenu();

        if (Input.GetKeyDown("f1"))
        {
            canvas.enabled = !canvas.enabled;
        }
        //TODO: Switch case
        if (setupTestTrick || doingTestTrick)
        {
            TestTrick();
        }
        else if (setupAntiSpinPointsSameOps || doingAntiSpinPointsSameOps)
        {
            AntiSpinPointsSameOps();
        }
        else if (setupAntiSpinPointsSplitOps || doingAntiSpinPointsSplitOps)
        {
            AntiSpinPointsSplitOps();
        }
        else if (setupAntiSpinWallPlaneFlower || doingAntiSpinWallPlaneFlower)
        {
            AntiSpinWallPlaneFlower();
        }
        else if (setupAntiSpinWallPlaneFlowerWithInSpin || doingAntiSpinWallPlaneFlowerWithInSpin)
        {
            AntiSpinWallPlaneFlowerWithInSpin();
        }
        else if (setupStandardTriquetra || doingStandardTriquetra)
        {
            StandardTriquetra();
        }
        else if (setupAntispinWheelPlaneFlowerSameOps || doingAntispinWheelPlaneFlowerSameOps)
        {
            AntispinWheelPlaneFlowerSameOps();
        }
        else if (setupAntispinWheelPlaneFlowerSplitSame || doingAntispinWheelPlaneFlowerSplitSame)
        {
            AntispinWheelPlaneFlowerSplitSame();
        }
        else if (setupInspinWheelPlaneFlowerSameSame || doingInspinWheelPlaneFlowerSameSame)
        {
            InspinWheelPlaneFlowerSameSame();
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
        else if (setupButterflyTaceVertical || doingButterflyTaceVertical)
        {
            ButterflyTaceVertical();
        }
        else if(setupThreeDimFlowerXZ || doingThreeDimFlowerXZ)
        {
            ThreeDimFlowerXZ();
        }
        else if(setupThreeDimDrills || doingThreeDimDrills)
        {
            ThreeDimDrills();
        }
        else
        {
            return;
        }
    }

    public void TestTrick()
    {
        if (!setupTestTrick && !doingTestTrick)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTestTrick = true;
        }
        if (doingTestTrick)
        {
            //pibot body around in circle
            bodyParts.leftShoulderSpin.rotationSpeedModifier = 2f;
            bodyParts.rightShoulderSpin.rotationSpeedModifier = 2f;

            bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Up);
            bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Up);
            //set speed modifier on props to 2 to get 2 points on inspin
            //bodyParts.leftPropSpin.rotationSpeedModifier = 2f;
            //bodyParts.rightPropSpin.rotationSpeedModifier = 2f;

            //rotate props in opposite directions
            //bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Forward);
            //bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Backward);

            //rotate arms in opposite directions, same direction as holding prop
            //bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Forward);
            bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Backward);

            //counter rotations to keep props in plane
            bodyParts.rightHandSpin.rotationSpeedModifier = 2f;
            bodyParts.rightHandSpin.RotateBodyPartAround(bodyParts.rightWrist, SpinDirections.Down);
            //bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Right);
        }
        if (setupTestTrick)
        {
            //rotate both arms to have prop on left side of body
            bodyParts.rightArm.transform.RotateAround(bodyParts.rightShoulder.transform.position, Vector3.back, 180f);
            //rotate both props to inspin starting position
            //bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, Vector3.forward, 90f);
            //bodyParts.rightProp.transform.RotateAround(bodyParts.rightHand.transform.position, Vector3.forward, 90f);
            //rotate both hand to be on inside of body
            bodyParts.leftHand.transform.RotateAround(bodyParts.leftWrist.transform.position, Vector3.up, 180f);
            bodyParts.rightHand.transform.RotateAround(bodyParts.rightWrist.transform.position, Vector3.up, 180f);

            setupTestTrick = false;
            doingTestTrick = true;

            thisSimulationName.text = "Test Trick";
            thisSimulationDescription.text = "";
        }
    }

    public void AntiSpinPointsSameOps()
    {
        if (!setupAntiSpinPointsSameOps && !doingAntiSpinPointsSameOps)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupAntiSpinPointsSameOps = true;
            envVariables.halfTrailSpeed = true;
        }

        if (doingAntiSpinPointsSameOps)
        {
            if (trickStage == 0)
            {
                //set poi speed modifier to 4 to get 4 zero points on each arm rotation
                bodyParts.leftPropSpin.rotationSpeedModifier = 4f;
                bodyParts.rightPropSpin.rotationSpeedModifier = 4f;
                //spin poi
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Forward);
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Backward);
                //rotate shoulders
                bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Backward);
                bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Forward);

                //right hand is at upper position           
                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalUp && bodyParts.zeroPointStageUpdate)
                {
                    ++trickStage;
                }
            }
            else if (trickStage == 1)
            {
                //kill velocity on both props
                bodyParts.leftPropSpin.rotationSpeedModifier = 0f;
                bodyParts.rightPropSpin.rotationSpeedModifier = 0f;
                //rotate shoulders in opposite directions
                bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Forward);
                bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Backward);

                //right hand is at lower position
                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalDown && bodyParts.zeroPointStageUpdate)
                {
                    trickStage = 0;
                }
            }
        }
        if (setupAntiSpinPointsSameOps)
        {
            //need to give it a cycle for the new spinner to be instantiated
            doingAntiSpinPointsSameOps = true;
            setupAntiSpinPointsSameOps = false;
            //rotate shoulders to have right hand down and left hand up
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);

            thisSimulationName.text = "Antispin Points";
            thisSimulationDescription.text = "Same Time / Opposite Direction";
        }
    }

    public void AntiSpinPointsSplitOps()
    {
        if (!setupAntiSpinPointsSplitOps && !doingAntiSpinPointsSplitOps)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupAntiSpinPointsSplitOps = true;
            envVariables.halfTrailSpeed = true;
        }
        if (doingAntiSpinPointsSplitOps)
        {
            if (trickStage == 0)
            {
                //set poi speed modifier to 4 to get 4 zero points on left arm rotation
                bodyParts.leftPropSpin.rotationSpeedModifier = 4f;
                //kill velocity on right arm
                bodyParts.rightPropSpin.rotationSpeedModifier = 0f;
                //spin poi
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Forward);
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Backward);
                //rotate shoulders
                bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Backward);
                bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Forward);

                //right hand is at upper position           
                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalUp && bodyParts.zeroPointStageUpdate)
                {
                    ++trickStage;
                }
            }
            else if (trickStage == 1)
            {
                //kill velocity on left prop
                bodyParts.leftPropSpin.rotationSpeedModifier = 0f;
                //set poi speed modifier to 4 to get 4 zero points on right arm rotation
                bodyParts.rightPropSpin.rotationSpeedModifier = 4f;
                //spin poit
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Backward);
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Forward);
                //rotate shoulders in opposite directions
                bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Forward);
                bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Backward);

                //right hand is at lower position
                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalDown && bodyParts.zeroPointStageUpdate)
                {
                    trickStage = 0;
                }
            }
        }
        if (setupAntiSpinPointsSplitOps)
        {
            //need to give it a cycle for the new spinner to be instantiated
            doingAntiSpinPointsSplitOps = true;
            setupAntiSpinPointsSplitOps = false;
            //rotate shoulders to have right hand down and left hand up
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);

            thisSimulationName.text = "Antispin Points";
            thisSimulationDescription.text = "Split Time / Opposite Direction";
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

            if (trickStage == 0)
            {
                //set poi speed modifier to 4 to get 4 zero points on each arm rotation
                bodyParts.leftPropSpin.rotationSpeedModifier = 4f;
                bodyParts.rightPropSpin.rotationSpeedModifier = 4f;
                //spin poi
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Forward);
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Forward);
                //rotate shoulders
                bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Backward);
                bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Backward);

                //right hand is at upper position           
                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalUp && bodyParts.zeroPointStageUpdate)
                {
                    ++trickStage;
                }
            }
            else if (trickStage == 1)
            {
                //no need to apply rotation force to poi, we want them to follow the hands, relative velocity is zero
                //rotate shoulders
                bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Forward);
                bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Forward);

                //right hand is at lower position
                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalDown && bodyParts.zeroPointStageUpdate)
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

    public void AntiSpinWallPlaneFlowerWithInSpin()
    {
        if (!setupAntiSpinWallPlaneFlowerWithInSpin && !doingAntiSpinWallPlaneFlowerWithInSpin)
        {
            ClearSpinner();
            SetSpinner(spinnerWallPlane);
            setupAntiSpinWallPlaneFlowerWithInSpin = true;
        }

        if (doingAntiSpinWallPlaneFlowerWithInSpin)
        {

            if (trickStage == 0)
            {
                //set poi speed modifier to 4 to get 4 zero points on each arm rotation
                bodyParts.leftPropSpin.rotationSpeedModifier = 4f;
                bodyParts.rightPropSpin.rotationSpeedModifier = 4f;
                //spin poi
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Forward);
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Forward);
                //rotate shoulders
                bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Backward);
                bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Backward);

                //right hand is at upper position           
                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalUp && bodyParts.zeroPointStageUpdate)
                {
                    ++trickStage;
                }
            }
            else if (trickStage == 1)
            {
                ////no need to apply rotation force to poi, we want them to follow the hands, relative velocity is zero
                ////rotate shoulders
                //bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Forward);
                //bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Forward);

                //set rotation speed modifier on props to 2 for 2 points on the inspin
                bodyParts.leftPropSpin.rotationSpeedModifier = 2f;
                bodyParts.rightPropSpin.rotationSpeedModifier = 2f;
                //rotate props forward
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Forward);
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Forward);
                //rotate arms forward
                bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Forward);
                bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Forward);

                //right hand is at lower position
                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalDown && bodyParts.zeroPointStageUpdate)
                {
                    trickStage = 0;
                }
            }
        }
        if (setupAntiSpinWallPlaneFlowerWithInSpin)
        {
            //need to give it a cycle for the new spinner to be instantiated
            doingAntiSpinWallPlaneFlowerWithInSpin = true;
            setupAntiSpinWallPlaneFlowerWithInSpin = false;

            thisSimulationName.text = "Wall Plane Antispin Flower";
        }
    }

    public void StandardTriquetra()
    {
        if (!setupStandardTriquetra && !doingStandardTriquetra)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupStandardTriquetra = true;
        }
        if (doingStandardTriquetra)
        {
            //rotate arms back
            bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Backward);
            bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Backward);
            //set speed modifier to 3 for 3 points in triquetra
            bodyParts.rightPropSpin.rotationSpeedModifier = 3f;
            bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Forward);
        }
        if (setupStandardTriquetra)
        {
            //rotate both arms to have prop above head
            bodyParts.rightArm.transform.RotateAround(bodyParts.rightShoulder.transform.position, Vector3.back, 90f);
            bodyParts.leftArm.transform.RotateAround(bodyParts.leftShoulder.transform.position, Vector3.forward, 90f);

            setupStandardTriquetra = false;
            doingStandardTriquetra = true;

            thisSimulationName.text = "Triquetra";
            thisSimulationDescription.text = "";
        }
    }

    public void ButterflyTaceVertical()
    {
        if (!setupButterflyTaceVertical && !doingButterflyTaceVertical)
        {
            ClearSpinner();
            SetSpinner(spinnerWallPlane);
            setupButterflyTaceVertical = true;
            envVariables.halfTrailSpeed = true;
        }
        if (doingButterflyTaceVertical)
        {
            if (trickStage == 0)
            {
                //rotate arms up
                bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Right);
                bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Right);
                //rotate hands against arm rotation to keep props in plane
                bodyParts.leftHandSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Left);
                bodyParts.rightHandSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Left);
                //rotate props
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Forward);
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Backward);

                //right hand is at upper position           
                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalUp && bodyParts.zeroPointStageUpdate)
                {
                    ++trickStage;
                }
            }
            if (trickStage == 1)
            {
                //rotate arms down
                bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Left);
                bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Left);
                //rotate hands against arm rotation to keep props in plane
                bodyParts.leftHandSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Right);
                bodyParts.rightHandSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Right);
                //rotate props
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Forward);
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Backward);

                //right hand is at lower position
                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalDown && bodyParts.zeroPointStageUpdate)
                {
                    trickStage = 0;
                }
            }
        }
        if (setupButterflyTaceVertical)
        {
            //rotate shoulders to lower position
            bodyParts.leftArm.transform.RotateAround(bodyParts.leftShoulder.transform.position, new Vector3(0, 0, 1f), -105);
            bodyParts.rightArm.transform.RotateAround(bodyParts.rightShoulder.transform.position, new Vector3(0, 0, 1f), 105f);
            //rotate props to match additional rotation of arms
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, new Vector3(0, 0, 1f), 15f);
            bodyParts.rightProp.transform.RotateAround(bodyParts.rightHand.transform.position, new Vector3(0, 0, 1f), -15f);

            setupButterflyTaceVertical = false;
            doingButterflyTaceVertical = true;
            thisSimulationName.text = "Butterfly Tracing Vertically";
        }
    }

    //public void PendulumStall()
    //{

    //}

    public void AntispinWheelPlaneFlowerSameOps()
    {
        if (!setupAntispinWheelPlaneFlowerSameOps && !doingAntispinWheelPlaneFlowerSameOps)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupAntispinWheelPlaneFlowerSameOps = true;
            envVariables.halfTrailSpeed = true;
        }
        if (doingAntispinWheelPlaneFlowerSameOps)
        {
            //set prop speed modifier to 4 to get 4 zero points
            bodyParts.leftPropSpin.rotationSpeedModifier = 4f;
            bodyParts.rightPropSpin.rotationSpeedModifier = 4f;
            //rotate arms around shoulders in opposite directions
            bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Forward);
            bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Backward);
            //spin props in opposite directions to get antispin
            bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Backward);
            bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Forward);
        }
        if (setupAntispinWheelPlaneFlowerSameOps)
        {
            setupAntispinWheelPlaneFlowerSameOps = false;
            doingAntispinWheelPlaneFlowerSameOps = true;

            thisSimulationName.text = "Wheel Plaine Antispine Flower";
            thisSimulationDescription.text = "Same Time / Opposite Direction";
        }
    }

    public void AntispinWheelPlaneFlowerSplitSame()
    {
        if (!setupAntispinWheelPlaneFlowerSplitSame && !doingAntispinWheelPlaneFlowerSplitSame)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupAntispinWheelPlaneFlowerSplitSame = true;
            envVariables.halfTrailSpeed = true;
        }
        if (doingAntispinWheelPlaneFlowerSplitSame)
        {
            //set prop speed modifier to 4 to get 4 zero points
            bodyParts.leftPropSpin.rotationSpeedModifier = 4f;
            bodyParts.rightPropSpin.rotationSpeedModifier = 4f;
            //rotate arms around shoulders in same directions
            bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Forward);
            bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Forward);
            //spin props in opposite directions to get antispin
            bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Backward);
            bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Backward);
        }
        if (setupAntispinWheelPlaneFlowerSplitSame)
        {
            setupAntispinWheelPlaneFlowerSplitSame = false;
            doingAntispinWheelPlaneFlowerSplitSame = true;

            thisSimulationName.text = "Wheel Plaine Antispine Flower";
            thisSimulationDescription.text = "Split Time / Same Direction";
        }
    }

    public void InspinWheelPlaneFlowerSameSame()
    {
        if (!setupInspinWheelPlaneFlowerSameSame && !doingInspinWheelPlaneFlowerSameSame)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupInspinWheelPlaneFlowerSameSame = true;
            envVariables.halfTrailSpeed = true;
        }
        if (doingInspinWheelPlaneFlowerSameSame)
        {
            //set rotation speed modifier on props to 2 for 2 points on the inspin
            bodyParts.leftPropSpin.rotationSpeedModifier = 2f;
            bodyParts.rightPropSpin.rotationSpeedModifier = 2f;
            //rotate props forward
            bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Backward);
            bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Backward);
            //rotate arms forward
            bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Backward);
            bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Backward);
        }
        if (setupInspinWheelPlaneFlowerSameSame)
        {
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, Vector3.forward, 180f);
            bodyParts.rightProp.transform.RotateAround(bodyParts.rightHand.transform.position, Vector3.forward, 180f);

            setupInspinWheelPlaneFlowerSameSame = false;
            doingInspinWheelPlaneFlowerSameSame = true;

            thisSimulationName.text = "Inspin Wheel Plane Flower";
            thisSimulationDescription.text = "Same Time / Same Direction";
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

    public void ThreeDimFlowerXZ()
    {
        if (!setupThreeDimFlowerXZ && !doingThreeDimFlowerXZ)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupThreeDimFlowerXZ = true;
        }
        if (doingThreeDimFlowerXZ)
        {
            //set speed modifiers on both props to 4 to get our 4 stall/zero points
            bodyParts.leftPropSpin.rotationSpeedModifier = 4f;
            bodyParts.rightPropSpin.rotationSpeedModifier = 4f;
            //rotate shoulders around torse
            bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Down);
            bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Down);
            //rotate left arm around shoulder joint to counter rotation around torso
            bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Up);           
            //rotate left arm back
            bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Forward);
            //rotate left props to get antispin
            bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Backward);
            bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Up);
        }
        if (setupThreeDimFlowerXZ)
        {
            //rotate right hand 90degrees to hold prop on Z plane
            bodyParts.rightHand.transform.RotateAround(
                bodyParts.rightWrist.transform.position,
                Vector3.right,
                90f);
            //rotate right arm outward
            bodyParts.rightArm.transform.RotateAround(
                bodyParts.rightShoulder.transform.position,
                Vector3.down,
                90f);
            //rotate left arm to up position
            bodyParts.leftArm.transform.RotateAround(
                bodyParts.leftShoulder.transform.position,
                Vector3.forward,
                90f);

            setupThreeDimFlowerXZ = false;
            doingThreeDimFlowerXZ = true;

            thisSimulationName.text = "3D Flower XZ-Planes";
            thisSimulationDescription.text = "";
        }
    }

    public void ThreeDimDrills()
    {
        if (!setupThreeDimDrills && !doingThreeDimDrills)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupThreeDimDrills = true;
        }
        if (doingThreeDimDrills)
        {
            //set speed modifier on props to 4 to get 4 zeropoint
            bodyParts.leftPropSpin.rotationSpeedModifier = 4f;
            bodyParts.rightPropSpin.rotationSpeedModifier = 4f;

            if (trickStage == 0)
            {
                //right prop: from back to down
                bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Forward);
                bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Forward);

                //spin prop anti to arms
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Backward);
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Backward);

                ////smooth hand rotation into position
                //if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalDown && intermidiateStageRotationCounter < 90)
                //{
                //    bodyParts.rightHand.transform.RotateAround(bodyParts.rightWrist.transform.position, Vector3.down, 15f);
                //    bodyParts.leftHand.transform.RotateAround(bodyParts.leftWrist.transform.position, Vector3.down, 15f);

                //    intermidiateStageRotationCounter += 15;
                //}

                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalDown && bodyParts.zeroPointStageUpdate)
                {
                    ++trickStage;

                    //rotate hands
                    //TODO: Smooth rotation into position
                    bodyParts.rightHand.transform.RotateAround(bodyParts.rightWrist.transform.position, Vector3.down, 90f);
                    bodyParts.leftHand.transform.RotateAround(bodyParts.leftWrist.transform.position, Vector3.down, 90f);
                }
            }
            else if (trickStage == 1)
            {                
                //right prop: from down to out
                bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Right);
                bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Right);

                //spin prop anti to arms
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Left);
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Left);

                if (bodyParts.rightPropRegionYZ == ZeroPointRegion.LocalForward && bodyParts.zeroPointStageUpdate)
                {
                    ++trickStage;
                    bodyParts.zeroPointPosition = -8f;
                    //rotate hands
                    //TODO: Smooth rotation into position
                    bodyParts.rightHand.transform.RotateAround(bodyParts.rightWrist.transform.position, Vector3.forward, 90f);
                    bodyParts.leftHand.transform.RotateAround(bodyParts.leftWrist.transform.position, Vector3.forward, 90f);
                }
            }
            else if (trickStage == 2)
            {
                //right prop: from out to forward
                bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Down);
                bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Down);

                //spin prop anti to arms
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Up);
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Up);

                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalLeft && bodyParts.zeroPointStageUpdate)
                {
                    ++trickStage;
                    bodyParts.zeroPointPosition = 0f;
                    //TODO: Smooth rotation into position
                    bodyParts.rightHand.transform.RotateAround(bodyParts.rightWrist.transform.position, Vector3.left, 90f);
                    bodyParts.leftHand.transform.RotateAround(bodyParts.leftWrist.transform.position, Vector3.left, 90f);
                }
            }
            else if (trickStage == 3)
            {
                //right prop: forward to up
                bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Forward);
                bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Forward);

                //spin prop anti to arms
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Backward);
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Backward);

                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalUp && bodyParts.zeroPointStageUpdate)
                {
                    ++trickStage;
                    //rotate hands
                    //TODO: Smooth rotation into position
                    bodyParts.rightHand.transform.RotateAround(bodyParts.rightWrist.transform.position, Vector3.up, 90f);
                    bodyParts.leftHand.transform.RotateAround(bodyParts.leftWrist.transform.position, Vector3.up, 90f);
                }
            }
            else if (trickStage == 4)
            {
                //right prop: up to out
                bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Left);
                bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Left);

                //spin prop anti to arms
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Right);
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Right);

                if (bodyParts.rightPropRegionYZ == ZeroPointRegion.LocalForward && bodyParts.zeroPointStageUpdate)
                {
                    ++trickStage;
                    bodyParts.zeroPointPosition = -8f;
                    //rotate hands
                    //TODO: Smooth rotation into position
                    bodyParts.rightHand.transform.RotateAround(bodyParts.rightWrist.transform.position, Vector3.forward, 90f);
                    bodyParts.leftHand.transform.RotateAround(bodyParts.leftWrist.transform.position, Vector3.forward, 90f);
                }
            }
            else if (trickStage == 5)
            {
                //right prop: out to back
                bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Up);
                bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Up);

                //spin prop anti to arms
                bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Down);
                bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Down);

                if (bodyParts.rightPropRegionXY == ZeroPointRegion.LocalRight && bodyParts.zeroPointStageUpdate)
                {
                    trickStage = 0;
                    ++trickCycles;
                    bodyParts.zeroPointPosition = 0f;
                    if (trickCycles == 2)
                    {
                        doingThreeDimDrills = false;
                        ThreeDimDrills();
                        return;
                    }
                    //TODO: Smooth rotation into position
                    bodyParts.rightHand.transform.RotateAround(bodyParts.rightWrist.transform.position, Vector3.right, 90f);
                    bodyParts.leftHand.transform.RotateAround(bodyParts.leftWrist.transform.position, Vector3.right, 90f);
                }
            }
        }
        if (setupThreeDimDrills)
        {
            setupThreeDimDrills = false;
            doingThreeDimDrills = true;

            thisSimulationName.text = "3D Drills";
            thisSimulationDescription.text = "";
        }
    }


    private void InspinAntispinWheelPlaneFlowerSameDirection()
    {
        //set prop speed modifier to 4 on right hand to get 4 zero points on each arm rotation, 2 on left hand to get 2 for inspins
        bodyParts.leftPropSpin.rotationSpeedModifier = 2f;
        bodyParts.rightPropSpin.rotationSpeedModifier = 4f;
        //spin prop around hand
        bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Backward);
        bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Backward);
        //rotate shoulders
        bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Backward);
        bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Forward);
    }

    private void InspinAntispinWheelPlaneFlowerOpsDirection()
    {
        //set prop speed modifier to 4 on right hand to get 4 zero points on each arm rotation, 2 on left hand to get 2 for inspins
        bodyParts.leftPropSpin.rotationSpeedModifier = 2f;
        bodyParts.rightPropSpin.rotationSpeedModifier = 4f;
        //spin prop around hand
        bodyParts.leftPropSpin.RotateBodyPartAround(bodyParts.leftHand, SpinDirections.Backward);
        bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Forward);
        //rotate shoulders
        bodyParts.leftShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Backward);
        bodyParts.rightShoulderSpin.RotateBodyPartAround(bodyParts.torso, SpinDirections.Backward);
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
            bodyParts.zeroPointPosition = 0f;
            BodyParts.instance = null;
            envVariables.bodyParts = null;
            Destroy(thisSpinner);
            thisSpinner = null;
            envVariables.halfTrailSpeed = false;
            envVariables.halfTrailSpeedUsed = false;
            envVariables.propTrailSpeed = envVariables.trailSpeedSlider.value;

            trickStage = 0;
            trickCycles = 0;
            doingTestTrick = false;
            doingAntiSpinPointsSameOps = false;
            doingAntiSpinPointsSplitOps = false;
            doingAntiSpinWallPlaneFlower = false;
            doingStandardTriquetra = false;
            doingAntispinWheelPlaneFlowerSameOps = false;
            doingAntispinWheelPlaneFlowerSplitSame = false;
            doingInspinWheelPlaneFlowerSameSame = false;
            doingInspinAntispinWheelPlaneFlowerSplitOps = false;
            doingInspinAntispinWheelPlaneFlowerSameOps = false;
            doingInspinAntispinWheelPlaneFlowerSameSame = false;
            doingInspinAntispinWheelPlaneFlowerSplitSame = false;
            doingButterflyTaceVertical = false;
            doingThreeDimFlowerXZ = false;
            doingThreeDimDrills = false;

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
            cam.gameObject.transform.position = camTransform.position;
            cam.orthographicSize = 70f;
        }
        else if (spinnerPrefab == spinnerWallPlane)
        {
            cam.gameObject.transform.position = camTransform.position + new Vector3(0f, 0f, -20f);
            cam.orthographicSize = 75;
        }
    }

    private void UpdateDebugMenu()
    {
        string debugText = "";

        if (bodyParts != null)
        {
            //debugText = "Left Prop Rot: " + bodyParts.leftPropPos.ToString()
            //    + "\r\nRight Prop Rot: " + bodyParts.rightPropPos.ToString();
            //debugText = "Left Prop Rot: " + bodyParts.leftProp.transform.rotation.eulerAngles
            //    + "\r\nRight Prop Rot: " + bodyParts.rightProp.transform.rotation.eulerAngles;
            debugText = "Left Prop Point: " + bodyParts.leftPropZeroPointRegionDebugText
                + "\r\nRight Prop Point: " + bodyParts.rightPropZeroPointRegionDebugText;
        }

        debugMenu.text = debugText;
    }
}
