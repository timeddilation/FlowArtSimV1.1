using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentVariables : MonoBehaviour
{
    [Header("Simulation Tweaks")]
    public float globalSpeed = 1f;
    public float propTrailSpeed = 3f;
    public string spinnerProps = "Hoops";
    public Slider globalSpeedSlider;
    public Slider trailSpeedSlider;

    [Header("Spinner Prefabs")]
    public GameObject hooperWallPlane;
    public GameObject hooperWheelPlane;
    public GameObject poiWallPlane;
    public GameObject poiWheelPlane;

    public static EnvironmentVariables instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
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

    private void GlobalSpeedSliderChanged(Slider slider)
    {
        globalSpeed = slider.value;
    }

    private void TrailSpeedSliderChanged(Slider slider)
    {
        propTrailSpeed = slider.value;
    }
}
