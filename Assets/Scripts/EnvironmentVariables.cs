using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpinDirections
{
    None = 0,
    Forward = 1,
    Backward = 2,
    Left = 3,
    Right = 4,
    Up = 5,
    Down = 6
}

public enum ZeroPointRegion
{
    None = 0,
    LocalForward = 1,
    LocalBackward = 2,
    LocalLeft = 3,
    LocalRight = 4,
    LocalUp = 5,
    LocalDown = 6
}

public enum PropSide
{
    None = 0,
    Left = 1,
    Right = 2
}

public enum SpinnerProps
{
    None = 0,
    Hoops = 1,
    Poi = 2
}

public class EnvironmentVariables : MonoBehaviour
{
    [Header("Simulation Tweaks")]
    public int targetFPS = 60;
    public float globalSpeed = 3f;
    public float propTrailSpeed = 6f;
    public float regionDetectionThreshold = 1f;
    public string spinnerProps = "Hoops";
    public Slider globalSpeedSlider;
    public Slider trailSpeedSlider;
    public Toggle reverseDirectionToggle;

    public GameObject planeMarkers;
    public Toggle showPlaneMarkersToggle;

    [Header("Spinner Prefabs")]
    public GameObject hooperWallPlane;
    public GameObject hooperWheelPlane;
    public GameObject poiWallPlane;
    public GameObject poiWheelPlane;

    [Header("Unity Gernerated Things")]
    public float trickStepper = 0f;
    public int eigthSteps = 0;
    public int stepsInTrick = 8; //tricks update this value with whatever their steps in the trick are to reset the steps counter
    public BodyParts bodyParts;
    public bool halfTrailSpeed = false;
    public bool halfTrailSpeedUsed = false;
    public bool reverseDirection = false;

    public static EnvironmentVariables instance;

    //tracker for when to change global speed
    private bool speedSliderChanged = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one EnvironmentVariables instance!");
        }
        instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = targetFPS;
        bodyParts = BodyParts.instance;

        //globalSpeedSlider.value = globalSpeed;
        GlobalSpeedSliderChanged(globalSpeedSlider);
        trailSpeedSlider.value = propTrailSpeed;

        planeMarkers.gameObject.SetActive(false);

        globalSpeedSlider.onValueChanged.AddListener(delegate
        {
            GlobalSpeedSliderChanged(globalSpeedSlider);
        });
        
        trailSpeedSlider.onValueChanged.AddListener(delegate
        {
            TrailSpeedSliderChanged(trailSpeedSlider);
        });

        reverseDirectionToggle.onValueChanged.AddListener(delegate
        {
            UpdateTrickDirection(reverseDirectionToggle);
        });

        showPlaneMarkersToggle.onValueChanged.AddListener(delegate
        {
            ShowPlaneMarkersChanged(showPlaneMarkersToggle);
        });
    }

    private void Update()
    {
        UpdatePoiTrailSpeed();

        //trick stepper tracker
        if (trickStepper >= (stepsInTrick * 45)) //trick steps are broken up into bits of 45 degrees
        {
            trickStepper = 0f;
        }
        if (eigthSteps == stepsInTrick)
        {
            eigthSteps = 0;
        }
       
        //update global speed if needing to
        if (speedSliderChanged)
        {
            GlobalSpeedSliderChanged(globalSpeedSlider);
        }
    }

    private void LateUpdate()
    {
        trickStepper += globalSpeed;
        eigthSteps = Convert.ToInt32(Math.Floor(trickStepper / 45));
    }

    private void GlobalSpeedSliderChanged(Slider slider)
    {
        speedSliderChanged = true;
        //only update global speed slider if trickStepper is divisble by 3 with a remainder of 0
        //this ensures we can always compare the trickStepper up-to 1/8 parts of the circle
        if (trickStepper % 3 == 0)
        {
            speedSliderChanged = false;
            switch (Convert.ToInt32(Math.Floor(slider.value)))
            {
                case 0:
                    globalSpeed = 0f;
                    break;
                case 1:
                    globalSpeed = 0.125f;
                    break;
                case 2:
                    globalSpeed = 0.25f;
                    break;
                case 3:
                    globalSpeed = 0.5f;
                    break;
                case 4:
                    globalSpeed = 1f;
                    break;
                case 5:
                    globalSpeed = 1.5f;
                    break;
                case 6:
                    globalSpeed = 3f;
                    break;
                default:
                    globalSpeed = 1.5f;
                    break;
            }
        }     
    }

    private void TrailSpeedSliderChanged(Slider slider)
    {
        propTrailSpeed = slider.value;
        halfTrailSpeedUsed = false;
    }

    private void UpdatePoiTrailSpeed()
    {
        if (halfTrailSpeed && !halfTrailSpeedUsed)
        {
            propTrailSpeed = propTrailSpeed / 2;
            halfTrailSpeedUsed = true;
        }

        bodyParts.leftPropTrail.time = propTrailSpeed;
        bodyParts.rightPropTrail.time = propTrailSpeed;
    }

    private void UpdateTrickDirection(Toggle toggle)
    {
        if (toggle.isOn && !reverseDirection)
        {
            reverseDirection = true;
            trickStepper = Math.Abs(360 - trickStepper);
        }
        else if (!toggle.isOn && reverseDirection)
        {
            reverseDirection = false;
            trickStepper = Math.Abs(360 - trickStepper);
        }
    }

    private void ShowPlaneMarkersChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            planeMarkers.gameObject.SetActive(true);
        }
        else
        {
            planeMarkers.gameObject.SetActive(false);
        }
    }

    public void QuitSimulation()
    {
        Application.Quit();
    }
}
