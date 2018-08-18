using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTrailActivity : MonoBehaviour
{
    public string toggleKey = "m";
    private TrailRenderer trailRenderer;

    private void Start()
    {
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            trailRenderer.enabled = !trailRenderer.enabled;
        }
    }

}
