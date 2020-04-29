using System.Collections;
using UnityEngine;

public class PhysicBody : MonoBehaviour {
	public Collider move_position(Vector3 position, BoxCollider walk_collider) {
		Collider c = collider_there(position + walk_collider.center, Vector3.Scale(walk_collider.size / 2, new Vector3(0.9f, 0.9f, 0.9f)));
		if (!c) transform.position = position;
		return c;
	}

	Collider collider_there(Vector3 center, Vector3 half_extents) {
		Collider[] colliders = Physics.OverlapBox(center, half_extents);
		foreach (var collider in colliders)
			if (collider.gameObject != gameObject) {
				return collider;
			}
		return null;
	}
}