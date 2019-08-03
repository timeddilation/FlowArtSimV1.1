using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBodyPart : MonoBehaviour {

    private EnvironmentVariables environmentVariables;

    public float rotationSpeed = 100f;
    public float rotationSpeedModifier = 1f;

    private void Awake()
    {
        environmentVariables = EnvironmentVariables.instance;
    }

    public void RotateBodyPartAround(GameObject bodyPart, SpinDirections dir)
    {
        switch (dir)
        {
            case SpinDirections.Forward:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.forward,
                    (rotationSpeed * rotationSpeedModifier) * Time.deltaTime * environmentVariables.globalSpeed);
                    //(rotationSpeed * rotationSpeedModifier) * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Backward:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.back,
                    (rotationSpeed * rotationSpeedModifier) * Time.deltaTime * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Left:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.left,
                    (rotationSpeed * rotationSpeedModifier) * Time.deltaTime * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Right:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.right,
                    (rotationSpeed * rotationSpeedModifier) * Time.deltaTime * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Up:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.up,
                    (rotationSpeed * rotationSpeedModifier) * Time.deltaTime * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Down:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.down,
                    (rotationSpeed * rotationSpeedModifier) * Time.deltaTime * environmentVariables.globalSpeed);
                break;
            default:
                break;
        }
    }
}
