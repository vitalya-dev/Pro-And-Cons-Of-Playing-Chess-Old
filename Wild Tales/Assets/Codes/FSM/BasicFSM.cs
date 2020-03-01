﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBehaviour<T> : StateMachineBehaviour {
    public T ob;
    public Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ob = animator.GetComponent<T>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

}
