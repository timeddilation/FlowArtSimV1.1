using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStepper : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (!animator.GetBool("Reverse"))
       {
           EnvironmentVariables.instance.trickStepper = 0f;
       }
       //On initial enter state, the animation lags a frame behind the step counter. Corrext the counter.
       if (!animator.GetBool("Looped"))
       {
           EnvironmentVariables.instance.trickStepper -= EnvironmentVariables.instance.globalSpeed;
       }
       EnvironmentVariables.instance.eigthSteps = 0;
    }
}
