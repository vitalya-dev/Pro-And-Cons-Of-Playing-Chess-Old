using System.Collections;
using UnityEngine;

public class PlayerKick : BasicFSM<Player> {
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator, stateInfo, layerIndex);
		/* ============================================== */
		ob.GetComponent<Collider>().enabled = false;
		/* ============================================== */
		if (ob.GetComponent<Area>().overlap<Projectile>()) {
			Debug.Log("Time to Kick It");
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateExit(animator, stateInfo, layerIndex);
		/* ============================================== */
		ob.GetComponent<Collider>().enabled = true;
	}
}