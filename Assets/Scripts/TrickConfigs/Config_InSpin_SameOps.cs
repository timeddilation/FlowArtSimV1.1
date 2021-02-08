using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config_InSpin_SameOps : StateMachineBehaviour
{
    private int eighthStepsInTrick = 8;
    private int leftArmSpinMod = 1;
    private int leftPropSpinMod = 2;
    private int rightArmSpinMod = 1;
    private int rightPropSpinMod = 2;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("EighthStepsInTrick", eighthStepsInTrick);
        animator.SetInteger("Left_ArmSpinModifier", leftArmSpinMod);
        animator.SetInteger("Left_PropSpinModifier", leftPropSpinMod);
        animator.SetInteger("Right_ArmSpinModifier", rightArmSpinMod);
        animator.SetInteger("Right_PropSpinModifier", rightPropSpinMod);
    }
}
