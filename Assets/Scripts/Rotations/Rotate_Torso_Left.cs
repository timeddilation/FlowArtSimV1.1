using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Torso_Left : StateMachineBehaviour
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
           bodyParts.torso,
           bodyParts.torso,
           SpinDirections.Down,
           2
       );
    }
}
