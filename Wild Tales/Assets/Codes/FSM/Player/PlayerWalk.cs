using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerWalk : BasicFSM<Player> {
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		/* ============================================== */
		ob.GetComponent<NavMeshAgent>().Move(ob.movement * Time.deltaTime * ob.speed);
		/* ============================================== */
		// pb.move_position(ob.transform.position + (Vector3)(ob.movement * ob.speed * Time.fixedDeltaTime), ob.walkable_box);

		Crosshair crosshair = GameObject.FindObjectOfType<Crosshair>();
		ob.transform.rotation = Quaternion.LookRotation(
			Vector3.Scale(crosshair.transform.position - ob.transform.position, new Vector3(1, 0, 1))
		);
		/* ============================================== */
	}
}