using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerWalk : BasicFSM<Player> {
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		/* ============================================== */
		ob.GetComponent<NavMeshAgent>().Move(ob.movement * Time.deltaTime * ob.speed);
		/* ============================================== */
		Crosshair crosshair = GameObject.FindObjectOfType<Crosshair>();
		ob.transform.rotation = Quaternion.LookRotation(
			Vector3.Scale(crosshair.transform.position - ob.transform.position, new Vector3(1, 0, 1))
		);
		/* ============================================== */
	}
}