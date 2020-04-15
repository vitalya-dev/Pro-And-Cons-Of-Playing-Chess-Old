using System.Collections;
using UnityEngine;

public class PlayerAttack : BasicFSM<Player> {
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator, stateInfo, layerIndex);
		/* ============================================== */
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		/* ============================================== */
		if (ob.transform.Find("Jab").GetComponent<Area>().overlap<Envi>()) {
			ob.transform.Find("Jab").GetComponent<Area>().overlap<Envi>().hit();
		}

	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateExit(animator, stateInfo, layerIndex);
		/* ============================================== */
		animator.SetFloat("attack speed", 1.0f);
	}
}