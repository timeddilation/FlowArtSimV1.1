using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLoop : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //must offset trick stepper when looping backwards to the end of a trick sequence
        if (animator.GetBool("Reverse"))
        {
            if(!animator.GetBool("Looped"))
            {
                EnvironmentVariables.instance.trickStepper -= (EnvironmentVariables.instance.globalSpeed * 1);
            }
            else
            {
                EnvironmentVariables.instance.trickStepper -= (EnvironmentVariables.instance.globalSpeed * 2);
            }
            
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetBool("Looped", true);
    }
}
