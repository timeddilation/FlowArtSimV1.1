using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_RightShoulder_Back : StateMachineBehaviour
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
           bodyParts.rightShoulder,
           bodyParts.rightShoulder,
           SpinDirections.Up,
           2
       );
    }
}
