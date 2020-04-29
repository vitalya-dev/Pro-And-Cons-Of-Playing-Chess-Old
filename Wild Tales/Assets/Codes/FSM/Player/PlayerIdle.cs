using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : BasicFSM<Player> {
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator, stateInfo, layerIndex);
		/* ============================================== */
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		/* ============================================== */
		Crosshair crosshair = GameObject.FindObjectOfType<Crosshair>();
		ob.transform.rotation = Quaternion.LookRotation(
			Vector3.Scale(crosshair.transform.position - ob.transform.position, new Vector3(1, 0, 1))
		);
		/* ============================================== */
	}
}