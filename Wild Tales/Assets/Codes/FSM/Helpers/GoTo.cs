using UnityEngine;
using System.Collections;

public class GoTo : StateMachineBehaviour {
    public string go_to;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.Play(go_to);
    }
}
