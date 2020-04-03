using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFSM<T> : StateMachineBehaviour {
    [HideInInspector]
    public T ob;
    public Physicsbody pb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ob = animator.GetComponent<T>();
        pb = animator.GetComponent<Physicsbody>();
    }
}
