using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FreeMovementCam : MonoBehaviour
{
    public float zoomSpeed = 7f;
    public float dragSpeed = 2f;
    public float rotateSpeed = 3f;
    public float rotationDampening = 15f;

    private float lookX = 0;
    private float lookY = 0;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;

    private void Start()
    {
        lookX = Vector3.Angle(Vector3.right, transform.right);
        lookY = Vector3.Angle(Vector3.up, transform.up);
    }

    private void LateUpdate()
    {
        //no mouse camera controls when using UI elements
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        //zoom in and out based on scroll wheel
        float cameraTransposeMagnitude = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        transform.Translate(0, 0, cameraTransposeMagnitude, Space.Self);

        //drag camera around 2D XY plane
        if (Input.GetMouseButton(0))
        {
            float dragX = Input.GetAxis("Mouse X") * dragSpeed;
            float dragY = Input.GetAxis("Mouse Y") * dragSpeed;
            transform.Translate(-dragX, -dragY, 0);
        }
        //rotate camera, but disallowed when holding left click too
        else if (Input.GetMouseButton(1))
        {
            //be sure to grab the current rotation as starting point.
            lookX += Input.GetAxis("Mouse X") * rotateSpeed;
            lookY -= Input.GetAxis("Mouse Y") * rotateSpeed;

            //set camera rotation 
            currentRotation = transform.rotation;
            desiredRotation = Quaternion.Euler(lookY, lookX, 0);
            rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * rotationDampening);
            transform.rotation = rotation;
        }
    }
}
