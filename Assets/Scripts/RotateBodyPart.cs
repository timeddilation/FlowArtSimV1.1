using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBodyPart : MonoBehaviour {

    private EnvironmentVariables environmentVariables;

    private readonly float baseRotationSpeed = 1f;
    public float rotationSpeedModifier = 1f;

    private void Awake()
    {
        environmentVariables = EnvironmentVariables.instance; //need to global speed scale value
    }

    public void RotateBodyPartAround(GameObject bodyPart, SpinDirections dir)
    {
        switch (dir)
        {
            case SpinDirections.Forward:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.forward,
                    baseRotationSpeed * rotationSpeedModifier * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Backward:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.back,
                    baseRotationSpeed * rotationSpeedModifier * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Left:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.left,
                    baseRotationSpeed * rotationSpeedModifier * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Right:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.right,
                    baseRotationSpeed * rotationSpeedModifier * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Up:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.up,
                    baseRotationSpeed * rotationSpeedModifier * environmentVariables.globalSpeed);
                break;
            case SpinDirections.Down:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.down,
                    baseRotationSpeed * rotationSpeedModifier * environmentVariables.globalSpeed);
                break;
            default:
                break;
        }
    }
}
