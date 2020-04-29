using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
	NavMeshPath path;

	void Awake() {
		path = new NavMeshPath();
	}

	public NavMeshPath calculate_path(Vector3 source, Vector3 target) {
		NavMesh.CalculatePath(source, target, NavMesh.AllAreas, path);
		return path;
	}

}