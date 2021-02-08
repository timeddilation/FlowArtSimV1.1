using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_LeftHand_Back : StateMachineBehaviour
{
    private BodyParts bodyParts;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bodyParts = animator.gameObject.GetComponent<BodyParts>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bodyParts.leftHand.transform.localPosition = Vector3.Lerp(
            bodyParts.leftHand.transform.localPosition,
            new Vector3(-3, 0, 0),
            EnvironmentVariables.instance.globalSpeed
        );
    }
}
