using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAnimationState : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetInteger("EighthStep", EnvironmentVariables.instance.eigthSteps);
    }
}
