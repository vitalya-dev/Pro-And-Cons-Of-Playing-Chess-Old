using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerWalk : BasicFSM<Player> {
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		/* ============================================== */
		Vector3 offset_x = Vector3.Scale(ob.movement, new Vector3(1, 0, 0)) * ob.speed * Time.deltaTime;
		Vector3 offset_z = Vector3.Scale(ob.movement, new Vector3(0, 0, 1)) * ob.speed * Time.deltaTime;
		NavMeshHit hit;
		/* ============================================== */
		if (NavMesh.FindClosestEdge(ob.transform.position + offset_x, out hit, NavMesh.AllAreas) && hit.distance > 0.2) {
			ob.transform.position += offset_x;
		} else {
			ob.transform.position += hit.normal * ob.speed * Time.deltaTime * Vector3.Dot(hit.normal, Vector3.forward * Mathf.Sign(hit.normal.z));
		}
		if (NavMesh.FindClosestEdge(ob.transform.position + offset_z, out hit, NavMesh.AllAreas) && hit.distance > 0.2) {
			ob.transform.position += offset_z;
		} else {
			ob.transform.position += hit.normal * ob.speed * Time.deltaTime * Vector3.Dot(hit.normal, Vector3.right * Mathf.Sign(hit.normal.x));
		}
		/* ============================================== */
		Crosshair crosshair = GameObject.FindObjectOfType<Crosshair>();
		ob.transform.rotation = Quaternion.LookRotation(
			Vector3.Scale(crosshair.transform.position - ob.transform.position, new Vector3(1, 0, 1))
		);
		/* ============================================== */
	}
}