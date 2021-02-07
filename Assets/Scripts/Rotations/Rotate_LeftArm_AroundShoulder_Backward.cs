using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_LeftArm_AroundShoulder_Backward : StateMachineBehaviour
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
           bodyParts.leftArm,
           bodyParts.leftShoulder,
           SpinDirections.Backward,
           animator.GetInteger("Left_ArmSpinModifier")
       );
    }
}
