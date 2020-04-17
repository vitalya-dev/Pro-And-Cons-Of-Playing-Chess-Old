using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BasicFSM<Enemy> {
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		/* ============================================== */
		if (ob.transform.Find("Sight") && ob.transform.Find("Sight").GetComponent<Sight>().see<Player>()) {
			ob.player = ob.transform.Find("Sight").GetComponent<Sight>().see<Player>();
		}
		if (ob.player)
			ob.GetComponent<Animator>().SetTrigger("chase");
	}
}