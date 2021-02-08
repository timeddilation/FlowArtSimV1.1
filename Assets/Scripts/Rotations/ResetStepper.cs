using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStepper : StateMachineBehaviour
{
    private EnvironmentVariables environmentVariables;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        environmentVariables = EnvironmentVariables.instance;
        if (!animator.GetBool("Reverse"))
        {
            environmentVariables.trickStepper = 0f;
            environmentVariables.eigthSteps = 0;
            animator.SetInteger("EighthStep", 0);
        }
        //On initial enter state, the animation lags a frame behind the step counter. Corrext the counter.
        if (!animator.GetBool("Looped"))
        {
            environmentVariables.trickStepper -= EnvironmentVariables.instance.globalSpeed;
        }
        animator.SetBool("PositionReady", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("Reverse"))
        {
            environmentVariables.trickStepper = animator.GetInteger("EighthStepsInTrick") * 45f;
            environmentVariables.eigthSteps = animator.GetInteger("EighthStepsInTrick");
        }
    }
}
