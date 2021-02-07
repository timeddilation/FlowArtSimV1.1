using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetbackStepper : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (!animator.GetBool("Reverse")) {
           EnvironmentVariables.instance.trickStepper -= EnvironmentVariables.instance.globalSpeed;
       } else {
           EnvironmentVariables.instance.trickStepper += EnvironmentVariables.instance.globalSpeed;
       }
       
    }
}
