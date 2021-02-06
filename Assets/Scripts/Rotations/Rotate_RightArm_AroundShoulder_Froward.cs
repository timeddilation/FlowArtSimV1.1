using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_RightArm_AroundShoulder_Froward : StateMachineBehaviour
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
           bodyParts.rightArm,
           bodyParts.rightShoulder,
           SpinDirections.Forward,
           animator.GetInteger("Right_ArmSpinModifier")
       );
    }
}
