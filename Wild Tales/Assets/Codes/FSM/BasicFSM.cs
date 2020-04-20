﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFSM<T> : StateMachineBehaviour {
	[HideInInspector]
	public T ob;
	[HideInInspector]
	public AI ai;
	[HideInInspector]
	public PhysicBody pb;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		ob = animator.GetComponent<T>();
		ai = animator.GetComponent<AI>();
		pb = animator.GetComponent<PhysicBody>();
	}
}