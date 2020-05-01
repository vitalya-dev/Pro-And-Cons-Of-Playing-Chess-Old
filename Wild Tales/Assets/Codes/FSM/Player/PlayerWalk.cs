using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerWalk : BasicFSM<Player> {
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		// base.OnStateUpdate(animator, stateInfo, layerIndex);
		// /* ============================================== */
		// // Vector3 offset_x = Vector3.Scale(ob.movement, new Vector3(1, 0, 0)) * ob.speed * Time.deltaTime;
		// // Vector3 offset_z = Vector3.Scale(ob.movement, new Vector3(0, 0, 1)) * ob.speed * Time.deltaTime;
		// pb.move_position(ob.transform.position + offset_x, ob.GetComponent<BoxCollider>());
		// pb.move_position(ob.transform.position + offset_z, ob.GetComponent<BoxCollider>());
		// /* ============================================== */
		// Crosshair crosshair = GameObject.FindObjectOfType<Crosshair>();
		// ob.transform.rotation = Quaternion.LookRotation(
		// 	Vector3.Scale(crosshair.transform.position - ob.transform.position, new Vector3(1, 0, 1))
		// );
		// /* ============================================== */
	}
}