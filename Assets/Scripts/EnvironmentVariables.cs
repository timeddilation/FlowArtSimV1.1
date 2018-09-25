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

public class EnvironmentVariables : MonoBehaviour
{
    [Header("Simulation Tweaks")]
    public float globalSpeed = 1f;
    public float propTrailSpeed = 6f;
    public string spinnerProps = "Hoops";
    public Slider globalSpeedSlider;
    public Slider trailSpeedSlider;
    public Toggle reverseDirectionToggle;

    [Header("Spinner Prefabs")]
    public GameObject hooperWallPlane;
    public GameObject hooperWheelPlane;
    public GameObject poiWallPlane;
    public GameObject poiWheelPlane;

    [Header("Unity Gernerated Things")]
    public BodyParts bodyParts;
    public bool halfTrailSpeed = false;
    public bool halfTrailSpeedUsed = false;

    public static EnvironmentVariables instance;

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
        bodyParts = BodyParts.instance;

        globalSpeedSlider.value = globalSpeed;
        globalSpeedSlider.onValueChanged.AddListener(delegate
        {
            GlobalSpeedSliderChanged(globalSpeedSlider);
        });

        trailSpeedSlider.value = propTrailSpeed;
        trailSpeedSlider.onValueChanged.AddListener(delegate
        {
            TrailSpeedSliderChanged(trailSpeedSlider);
        });
    }

    private void Update()
    {
        UpdatePoiTrailSpeed();
        UpdateTrickDirection();
    }

    private void GlobalSpeedSliderChanged(Slider slider)
    {
        globalSpeed = slider.value;
    }

    private void TrailSpeedSliderChanged(Slider slider)
    {
        propTrailSpeed = slider.value;
        UpdateTrickDirection();
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

    private void UpdateTrickDirection()
    {
        if (reverseDirectionToggle.isOn && globalSpeed > 0)
        {
            globalSpeed = globalSpeed * -1;
        }
        else if (!reverseDirectionToggle.isOn && globalSpeed < 0)
        {
            globalSpeed = globalSpeed * -1;
        }
    }
}
