using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamControls : MonoBehaviour
{
    public Camera mainCamera;
    public Camera angledCamera;

    public RenderTexture secondCameraView;

    private bool mainCameraViewing = true;

    private void Start()
    {
        angledCamera.targetTexture = secondCameraView;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(3))
        {
            SwapCameras();
        }
    }

    public void SwapCameras()
    {
        if (mainCameraViewing)
        {
            angledCamera.targetTexture = null;
            mainCamera.targetTexture = secondCameraView;

            mainCameraViewing = false;
        }
        else
        {
            angledCamera.targetTexture = secondCameraView;
            mainCamera.targetTexture = null;

            mainCameraViewing = true;
        }
    }
}
