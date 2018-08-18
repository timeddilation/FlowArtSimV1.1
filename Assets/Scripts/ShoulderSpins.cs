using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderSpins : MonoBehaviour
{
    private EnvironmentVariables environmentVariables;

    public float rotationSpeed = 100f;
    public float rotationSpeedModifier = 1f;

    private void Start()
    {
        environmentVariables = EnvironmentVariables.instance;
    }

    public void SpinShoulderAroundTorso(GameObject torso, string dir)
    {
        if (dir == "forward")
        {
            gameObject.transform.RotateAround(
                torso.transform.position,
                Vector3.forward,
                (rotationSpeed * rotationSpeedModifier) * Time.deltaTime * environmentVariables.globalSpeed);
        }
        else if (dir == "back")
        {
            gameObject.transform.RotateAround(
                torso.transform.position,
                Vector3.back,
                (rotationSpeed * rotationSpeedModifier) * Time.deltaTime * environmentVariables.globalSpeed);
        }
        else if (dir == "left")
        {
            gameObject.transform.RotateAround(
                torso.transform.position,
                Vector3.left,
                (rotationSpeed * rotationSpeedModifier) * Time.deltaTime * environmentVariables.globalSpeed);
        }
        else if (dir == "right")
        {
            gameObject.transform.RotateAround(
                torso.transform.position,
                Vector3.right,
                (rotationSpeed * rotationSpeedModifier) * Time.deltaTime * environmentVariables.globalSpeed);
        }
        else if (dir == "up")
        {
            gameObject.transform.RotateAround(
                torso.transform.position,
                Vector3.up,
                (rotationSpeed * rotationSpeedModifier) * Time.deltaTime * environmentVariables.globalSpeed);
        }
        else if (dir == "down")
        {
            gameObject.transform.RotateAround(
                torso.transform.position,
                Vector3.down,
                (rotationSpeed * rotationSpeedModifier) * Time.deltaTime * environmentVariables.globalSpeed);
        }
    }
}
