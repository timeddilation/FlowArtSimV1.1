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
        float speedMultiplier = environmentVariables.globalSpeed;
        if (environmentVariables.reverseDirection)
        {
            speedMultiplier = environmentVariables.globalSpeed * -1;
        }
        switch (dir)
        {
            case SpinDirections.Forward:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.forward,
                    baseRotationSpeed * rotationSpeedModifier * speedMultiplier);
                break;
            case SpinDirections.Backward:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.back,
                    baseRotationSpeed * rotationSpeedModifier * speedMultiplier);
                break;
            case SpinDirections.Left:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.left,
                    baseRotationSpeed * rotationSpeedModifier * speedMultiplier);
                break;
            case SpinDirections.Right:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.right,
                    baseRotationSpeed * rotationSpeedModifier * speedMultiplier);
                break;
            case SpinDirections.Up:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.up,
                    baseRotationSpeed * rotationSpeedModifier * speedMultiplier);
                break;
            case SpinDirections.Down:
                gameObject.transform.RotateAround(
                    bodyPart.transform.position,
                    Vector3.down,
                    baseRotationSpeed * rotationSpeedModifier * speedMultiplier);
                break;
            default:
                break;
        }
    }
}
