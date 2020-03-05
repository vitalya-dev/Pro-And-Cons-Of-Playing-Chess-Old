﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFSM<T> : StateMachineBehaviour {
    [HideInInspector]
    public T ob;
    [HideInInspector]
    public Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ob = animator.GetComponent<T>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

}
