using System.Collections;
using UnityEngine;

public class PlayerKick : BasicFSM<Player> {
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator, stateInfo, layerIndex);
		/* ============================================== */
		ob.GetComponent<Collider>().enabled = false;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		/* ============================================== */
		if (ob.transform.Find("Leg Left").GetComponent<Area>().overlap<Projectile>()) {
			ob.transform.Find("Leg Left").GetComponent<Area>().overlap<Projectile>().hit(ob.transform.forward, 20);
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateExit(animator, stateInfo, layerIndex);
		/* ============================================== */
		ob.GetComponent<Collider>().enabled = true;
	}
}