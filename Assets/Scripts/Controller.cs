using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Tricks
{
    None,
    TestTrick,
    //stall moves
    ButterflyTraceVertical,
    //antispin variations
    AntispinPointsSplitOps,
    AntispinPointsSameOps,
    AntispinWallPlaneFlower,
    AntispinWallPlaneFlowerWithInSpin,
    Triquetra,
    //wheel plane flowers
    AntispinWheelPlaneFlowerSameOps,
    AntispinWheelPlaneFlowerSplitSame,
    InspinWheelPlaneFlowerSameSame,
    InspinVsAntispinFlowerSameOps,
    InspinVsAntispinFlowerSameSame,
    InspinVsAntispinFlowerSplitOps,
    InspinVsAntispinFlowerSplitSame,
    //3D tricks
    ThreedFlowerXZ,
    ThreedDrills
}

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
    private Tricks trick = Tricks.None;
    private bool setupTrick = false;
    private bool doingTrick = false;

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

        //default value in this function is currentl set to hoops if none is provided.
        SetSpinnerProps(SpinnerProps.None);
    }

    private void Update()
    {
        UpdateDebugMenu();
        RunTrick();

        if (Input.GetKeyDown("f1"))
        {
            canvas.enabled = !canvas.enabled;
        }        
    }

    private void RunTrick()
    {
        switch (trick)
        {
            case Tricks.TestTrick:
                TestTrick();
                break;
            //stall moves
            case Tricks.ButterflyTraceVertical:
                ButterflyTaceVertical();
                break;
            //antispin variations
            case Tricks.AntispinPointsSplitOps:
                AntiSpinPointsSplitOps();
                break;
            case Tricks.AntispinPointsSameOps:
                AntiSpinPointsSameOps();
                break;
            case Tricks.AntispinWallPlaneFlower:
                AntiSpinWallPlaneFlower();
                break;
            case Tricks.AntispinWallPlaneFlowerWithInSpin:
                AntiSpinWallPlaneFlowerWithInSpin();
                break;
            case Tricks.Triquetra:
                StandardTriquetra();
                break;
            //wheel plane flowers
            case Tricks.AntispinWheelPlaneFlowerSameOps:
                AntispinWheelPlaneFlowerSameOps();
                break;
            case Tricks.AntispinWheelPlaneFlowerSplitSame:
                AntispinWheelPlaneFlowerSplitSame();
                break;
            case Tricks.InspinWheelPlaneFlowerSameSame:
                InspinWheelPlaneFlowerSameSame();
                break;
            case Tricks.InspinVsAntispinFlowerSameOps:
                InspinAntispinWheelPlaneFlowerSameOps();
                break;
            case Tricks.InspinVsAntispinFlowerSameSame:
                InspinAntispinWheelPlaneFlowerSameSame();
                break;
            case Tricks.InspinVsAntispinFlowerSplitOps:
                InspinAntispinWheelPlaneFlowerSplitOps();
                break;
            case Tricks.InspinVsAntispinFlowerSplitSame:
                InspinAntispinWheelPlaneFlowerSplitSame();
                break;
            //3D tricks
            case Tricks.ThreedFlowerXZ:
                ThreeDimFlowerXZ();
                break;
            case Tricks.ThreedDrills:
                ThreeDimDrills();
                break;
            default:
                break;
        }
    }

    public void TestTrick()
    {
        if (trick != Tricks.TestTrick)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.TestTrick;
        }
        if (doingTrick)
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
        if (setupTrick)
        {
            //rotate both arms to have prop on left side of body
            bodyParts.rightArm.transform.RotateAround(bodyParts.rightShoulder.transform.position, Vector3.back, 180f);
            //rotate both props to inspin starting position
            //bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, Vector3.forward, 90f);
            //bodyParts.rightProp.transform.RotateAround(bodyParts.rightHand.transform.position, Vector3.forward, 90f);
            //rotate both hand to be on inside of body
            bodyParts.leftHand.transform.RotateAround(bodyParts.leftWrist.transform.position, Vector3.up, 180f);
            bodyParts.rightHand.transform.RotateAround(bodyParts.rightWrist.transform.position, Vector3.up, 180f);

            setupTrick = false;
            doingTrick = true;

            thisSimulationName.text = "Test Trick";
            thisSimulationDescription.text = "";
        }
    }

    //stall moves
    public void ButterflyTaceVertical()
    {
        if (trick != Tricks.ButterflyTraceVertical)
        {
            ClearSpinner();
            SetSpinner(spinnerWallPlane);
            setupTrick = true;
            trick = Tricks.ButterflyTraceVertical;
            envVariables.halfTrailSpeed = true;
        }
        if (doingTrick)
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
        if (setupTrick)
        {
            //rotate shoulders to lower position
            bodyParts.leftArm.transform.RotateAround(bodyParts.leftShoulder.transform.position, new Vector3(0, 0, 1f), -105);
            bodyParts.rightArm.transform.RotateAround(bodyParts.rightShoulder.transform.position, new Vector3(0, 0, 1f), 105f);
            //rotate props to match additional rotation of arms
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, new Vector3(0, 0, 1f), 15f);
            bodyParts.rightProp.transform.RotateAround(bodyParts.rightHand.transform.position, new Vector3(0, 0, 1f), -15f);

            setupTrick = false;
            doingTrick = true;
            thisSimulationName.text = "Butterfly Tracing Vertically";
        }
    }

    //antispin variations
    public void AntiSpinPointsSplitOps()
    {
        if (trick != Tricks.AntispinPointsSplitOps)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.AntispinPointsSplitOps;
            envVariables.halfTrailSpeed = true;
        }
        if (doingTrick)
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
        if (setupTrick)
        {
            //need to give it a cycle for the new spinner to be instantiated
            doingTrick = true;
            setupTrick = false;
            //rotate shoulders to have right hand down and left hand up
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);

            thisSimulationName.text = "Antispin Points";
            thisSimulationDescription.text = "Split Time / Opposite Direction";
        }
    }

    public void AntiSpinPointsSameOps()
    {
        if (trick != Tricks.AntispinPointsSameOps)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.AntispinPointsSameOps;
            envVariables.halfTrailSpeed = true;
        }

        if (doingTrick)
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
        if (setupTrick)
        {
            //need to give it a cycle for the new spinner to be instantiated
            doingTrick = true;
            setupTrick = false;
            //rotate shoulders to have right hand down and left hand up
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);

            thisSimulationName.text = "Antispin Points";
            thisSimulationDescription.text = "Same Time / Opposite Direction";
        }
    }

    public void AntiSpinWallPlaneFlower()
    {
        if (trick != Tricks.AntispinWallPlaneFlower)
        {
            ClearSpinner();
            SetSpinner(spinnerWallPlane);
            setupTrick = true;
            trick = Tricks.AntispinWallPlaneFlower;
        }       

        if (doingTrick)
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
        if (setupTrick)
        {
            //need to give it a cycle for the new spinner to be instantiated
            doingTrick = true;
            setupTrick = false;

            thisSimulationName.text = "Wall Plane Antispin Flower";
        }       
    }

    public void AntiSpinWallPlaneFlowerWithInSpin()
    {
        if (trick != Tricks.AntispinWallPlaneFlowerWithInSpin)
        {
            ClearSpinner();
            SetSpinner(spinnerWallPlane);
            setupTrick = true;
            trick = Tricks.AntispinWallPlaneFlowerWithInSpin;
        }

        if (doingTrick)
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
        if (setupTrick)
        {
            //need to give it a cycle for the new spinner to be instantiated
            doingTrick = true;
            setupTrick = false;

            thisSimulationName.text = "Wall Plane Antispin Flower with In Spin";
        }
    }

    public void StandardTriquetra()
    {
        if (trick != Tricks.Triquetra)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.Triquetra;
        }
        if (doingTrick)
        {
            //rotate arms back
            bodyParts.rightArmSpin.RotateBodyPartAround(bodyParts.rightShoulder, SpinDirections.Backward);
            bodyParts.leftArmSpin.RotateBodyPartAround(bodyParts.leftShoulder, SpinDirections.Backward);
            //set speed modifier to 3 for 3 points in triquetra
            bodyParts.rightPropSpin.rotationSpeedModifier = 3f;
            bodyParts.rightPropSpin.RotateBodyPartAround(bodyParts.rightHand, SpinDirections.Forward);
        }
        if (setupTrick)
        {
            //rotate both arms to have prop above head
            bodyParts.rightArm.transform.RotateAround(bodyParts.rightShoulder.transform.position, Vector3.back, 90f);
            bodyParts.leftArm.transform.RotateAround(bodyParts.leftShoulder.transform.position, Vector3.forward, 90f);

            setupTrick = false;
            doingTrick = true;

            thisSimulationName.text = "Triquetra";
            thisSimulationDescription.text = "";
        }
    }

    //public void PendulumStall()
    //{

    //}

    //wheel plane flowers
    public void AntispinWheelPlaneFlowerSameOps()
    {
        if (trick != Tricks.AntispinWheelPlaneFlowerSameOps)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.AntispinWheelPlaneFlowerSameOps;
            envVariables.halfTrailSpeed = true;
        }
        if (doingTrick)
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
        if (setupTrick)
        {
            setupTrick = false;
            doingTrick = true;

            thisSimulationName.text = "Wheel Plaine Antispine Flower";
            thisSimulationDescription.text = "Same Time / Opposite Direction";
        }
    }

    public void AntispinWheelPlaneFlowerSplitSame()
    {
        if (trick != Tricks.AntispinWheelPlaneFlowerSplitSame)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.AntispinWheelPlaneFlowerSplitSame;
            envVariables.halfTrailSpeed = true;
        }
        if (doingTrick)
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
        if (setupTrick)
        {
            setupTrick = false;
            doingTrick = true;

            thisSimulationName.text = "Wheel Plaine Antispine Flower";
            thisSimulationDescription.text = "Split Time / Same Direction";
        }
    }

    public void InspinWheelPlaneFlowerSameSame()
    {
        if (trick != Tricks.InspinWheelPlaneFlowerSameSame)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.InspinWheelPlaneFlowerSameSame;
            envVariables.halfTrailSpeed = true;
        }
        if (doingTrick)
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
        if (setupTrick)
        {
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, Vector3.forward, 180f);
            bodyParts.rightProp.transform.RotateAround(bodyParts.rightHand.transform.position, Vector3.forward, 180f);

            setupTrick = false;
            doingTrick = true;

            thisSimulationName.text = "Inspin Wheel Plane Flower";
            thisSimulationDescription.text = "Same Time / Same Direction";
        }
    }

    public void InspinAntispinWheelPlaneFlowerSameOps()
    {
        if (trick != Tricks.InspinVsAntispinFlowerSameOps)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.InspinVsAntispinFlowerSameOps;
        }
        if (doingTrick)
        {
            InspinAntispinWheelPlaneFlowerOpsDirection();
        }
        if (setupTrick)
        {
            //put left hand (inSpin hand) in Inspin starting location
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, new Vector3(0, 0, 180f), 180f);
            //rotate shoulders to have right hand down and left hand up
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            //need to give it a cycle for the new spinner to be instantiated
            setupTrick = false;
            doingTrick = true;

            thisSimulationName.text = "Wheel Plane Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Same Time / Opposite Direction";
        }
    }

    public void InspinAntispinWheelPlaneFlowerSameSame()
    {
        if (trick != Tricks.InspinVsAntispinFlowerSameSame)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.InspinVsAntispinFlowerSameSame;
        }
        if (doingTrick)
        {
            InspinAntispinWheelPlaneFlowerSameDirection();
        }
        if (setupTrick)
        {
            //rotate shoulders to lower position
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), -90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            //need to give it a cycle for the new spinner to be instantiated
            setupTrick = false;
            doingTrick = true;

            thisSimulationName.text = "Wheel Plane Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Same Time / Same Direction";
        }
    }

    public void InspinAntispinWheelPlaneFlowerSplitOps()
    {
        if (trick != Tricks.InspinVsAntispinFlowerSplitOps)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.InspinVsAntispinFlowerSplitOps;
        }
        if (doingTrick)
        {
            InspinAntispinWheelPlaneFlowerOpsDirection();
        }
        if (setupTrick)
        {
            //put left hand (inSpin hand) in Inspin starting location
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, new Vector3(0, 0, 180f), 180f);
            //need to give it a cycle for the new spinner to be instantiated
            setupTrick = false;
            doingTrick = true;

            thisSimulationName.text = "Wheel Plane Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Split Time / Opposite Direction";
        }
    }

    public void InspinAntispinWheelPlaneFlowerSplitSame()
    {
        if (trick != Tricks.InspinVsAntispinFlowerSplitSame)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.InspinVsAntispinFlowerSplitSame;
        }
        if (doingTrick)
        {
            InspinAntispinWheelPlaneFlowerSameDirection();
        }
        if (setupTrick)
        {
            //put left hand (inSpin hand) in Inspin starting location
            bodyParts.leftProp.transform.RotateAround(bodyParts.leftHand.transform.position, new Vector3(0, 0, 180f), 180f);
            //rotate shoulders to lower position
            bodyParts.leftShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), -90f);
            bodyParts.rightShoulder.transform.RotateAround(bodyParts.torso.transform.position, new Vector3(0, 0, 90f), 90f);
            //need to give it a cycle for the new spinner to be instantiated
            setupTrick = false;
            doingTrick = true;

            thisSimulationName.text = "Wheel Plane Inspin vs. Antispin Flower";
            thisSimulationDescription.text = "Split Time / Same Direction";
        }
    }

    //3D tricks
    public void ThreeDimFlowerXZ()
    {
        if (trick != Tricks.ThreedFlowerXZ)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.ThreedFlowerXZ;
        }
        if (doingTrick)
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
        if (setupTrick)
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

            setupTrick = false;
            doingTrick = true;

            thisSimulationName.text = "3D Flower XZ-Planes";
            thisSimulationDescription.text = "";
        }
    }

    public void ThreeDimDrills()
    {
        if (trick != Tricks.ThreedDrills)
        {
            ClearSpinner();
            SetSpinner(spinnerWheelPlane);
            setupTrick = true;
            trick = Tricks.ThreedDrills;
        }
        if (doingTrick)
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
                        doingTrick = false;
                        ThreeDimDrills();
                        return;
                    }
                    //TODO: Smooth rotation into position
                    bodyParts.rightHand.transform.RotateAround(bodyParts.rightWrist.transform.position, Vector3.right, 90f);
                    bodyParts.leftHand.transform.RotateAround(bodyParts.leftWrist.transform.position, Vector3.right, 90f);
                }
            }
        }
        if (setupTrick)
        {
            setupTrick = false;
            doingTrick = true;

            thisSimulationName.text = "3D Drills";
            thisSimulationDescription.text = "";
        }
    }

    //re-usables

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
        SpinnerProps selectedProp = SpinnerProps.None;
        switch (change.captionText.text)
        {
            case "Hoops":
                selectedProp = SpinnerProps.Hoops;
                break;
            case "Poi":
                selectedProp = SpinnerProps.Poi;
                break;
            default:
                selectedProp = SpinnerProps.Hoops;
                break;
        }
        SetSpinnerProps(selectedProp);
        ClearSpinner();
        SetSpinner(spinnerWallPlane);
    }

    private void SetSpinnerProps(SpinnerProps propName)
    {
        switch (propName)
        {
            case SpinnerProps.Hoops:
                spinnerWallPlane = envVariables.hooperWallPlane;
                spinnerWheelPlane = envVariables.hooperWheelPlane;
            break;
            case SpinnerProps.Poi:
                spinnerWallPlane = envVariables.poiWallPlane;
            spinnerWheelPlane = envVariables.poiWheelPlane;
            break;
            default:
                spinnerWallPlane = envVariables.hooperWallPlane;
            spinnerWheelPlane = envVariables.hooperWheelPlane;
            break;
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
            envVariables.trickStepper = 0f;
            envVariables.eigthSteps = 0;

            trickStage = 0;
            trickCycles = 0;
            doingTrick = false;
            trick = Tricks.None;

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
                + "\r\nRight Prop Point: " + bodyParts.rightPropZeroPointRegionDebugText
                + "\r\nTrick Stepper: " + envVariables.trickStepper.ToString()
                + "\r\nEigth Steps: " + envVariables.eigthSteps.ToString();
        }

        debugMenu.text = debugText;
    }
}
