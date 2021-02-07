using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_LeftProp_AroundHand_Forward : StateMachineBehaviour
{
    private BodyParts bodyParts;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       bodyParts = animator.gameObject.GetComponent<BodyParts>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       bodyParts.RotateBodyPartRelative
       (
           bodyParts.leftProp,
           bodyParts.leftHand,
           SpinDirections.Forward,
           animator.GetInteger("Left_PropSpinModifier")
       );

       animator.SetInteger("EighthStep", EnvironmentVariables.instance.eigthSteps);
    }
}
