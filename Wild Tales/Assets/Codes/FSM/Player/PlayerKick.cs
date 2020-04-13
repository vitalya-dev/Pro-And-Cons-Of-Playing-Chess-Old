using System.Collections;
using UnityEngine;

public class PlayerKick : BasicFSM<Player> {
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator, stateInfo, layerIndex);
		/* ============================================== */
		ob.GetComponent<Collider>().enabled = false;
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateExit(animator, stateInfo, layerIndex);
		/* ============================================== */
		if (ob.transform.Find("Leg Left").GetComponent<Area>().overlap<Projectile>()) {
			ob.transform.Find("Leg Left").GetComponent<Area>().overlap<Projectile>().hit(ob.transform.forward, 10);
		}
		/* ============================================== */
		ob.GetComponent<Collider>().enabled = true;
	}
}