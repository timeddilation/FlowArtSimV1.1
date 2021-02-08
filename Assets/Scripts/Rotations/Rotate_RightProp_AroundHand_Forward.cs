using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_RightProp_AroundHand_Forward : StateMachineBehaviour
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
           bodyParts.rightProp,
           bodyParts.rightHand,
           SpinDirections.Forward,
           animator.GetInteger("Right_PropSpinModifier")
       );
    }
}
