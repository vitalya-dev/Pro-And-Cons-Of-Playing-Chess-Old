using UnityEngine;
using System.Collections;

public class ClearTriggers : StateMachineBehaviour {
    public string[] triggers;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        foreach (var trigger in triggers)
            animator.ResetTrigger(trigger);
        animator.SetTrigger("finished");
    }
}
