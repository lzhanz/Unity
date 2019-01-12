using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNormalizedTime : StateMachineBehaviour
{
    private string targetParameter = "Normalized Time";

    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(targetParameter, 0);
    }

  
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(targetParameter, stateInfo.normalizedTime);
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(targetParameter, 0);
    }
}