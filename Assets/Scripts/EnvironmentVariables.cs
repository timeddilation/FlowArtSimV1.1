using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentVariables : MonoBehaviour
{
    [Header("Simulation Tweaks")]
    public float globalSpeed = 1f;
    public float propTrailSpeed = 6f;
    public string spinnerProps = "Hoops";
    public Slider globalSpeedSlider;
    public Slider trailSpeedSlider;

    [Header("Spinner Prefabs")]
    public GameObject hooperWallPlane;
    public GameObject hooperWheelPlane;
    public GameObject poiWallPlane;
    public GameObject poiWheelPlane;

    [Header("Unity Gernerated Things")]
    public BodyParts bodyParts;

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
    }

    private void GlobalSpeedSliderChanged(Slider slider)
    {
        globalSpeed = slider.value;
    }

    private void TrailSpeedSliderChanged(Slider slider)
    {
        propTrailSpeed = slider.value;
    }

    private void UpdatePoiTrailSpeed()
    {
        bodyParts.leftPropTrail.time = propTrailSpeed;
        bodyParts.rightPropTrail.time = propTrailSpeed;
    }
}
