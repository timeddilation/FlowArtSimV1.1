using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Debugger : MonoBehaviour {

    private Stopwatch stopwatch = new Stopwatch();
    public long timeSpan;

	// Use this for initialization
	void Start () {
        stopwatch.Start();
	}
	
	// Update is called once per frame
	void Update () {
        timeSpan = stopwatch.Elapsed.Milliseconds;
	}
}
