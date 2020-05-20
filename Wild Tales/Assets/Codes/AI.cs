using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
	NavMeshPath path;
	Vector3[] corners;

	void Awake() {
		path = new NavMeshPath();
	}

	public NavMeshPath calculate_path(Vector3 target) {
		NavMesh.CalculatePath(Vector3.Scale(transform.position, new Vector3(1, 0, 1)), Vector3.Scale(target, new Vector3(1, 0, 1)), NavMesh.AllAreas, path);
		corners = path.corners.Clone() as Vector3[];
		return path;
	}

	public bool move_by_path(float speed) {
		PhysicBody pb = GetComponent<PhysicBody>();
		if (corners.Length > 1) {
			var d = Vector3.Scale(corners[1] - transform.position, new Vector3(1, 0, 1));
			if (d.magnitude > 0.1) {
				pb.move_position(transform.position + Vector3.ClampMagnitude(d.normalized * speed * Time.deltaTime, d.magnitude), GetComponent<BoxCollider>());
				transform.rotation = Quaternion.LookRotation(d.normalized);
			} else {
				/********************* shifting corners *********************/
				Array.Copy(corners, 1, corners, 0, corners.Length - 1);
				Array.Resize(ref corners, corners.Length - 1);
				/********************* shifting corners *********************/
			}
			return true;
		} else {
			return false;
		}
	}

}