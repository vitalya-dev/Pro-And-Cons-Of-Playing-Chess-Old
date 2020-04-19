using System.Collections;
using UnityEngine;

public class Physicsbody : MonoBehaviour {

	public bool move_position(Vector3 position, BoxCollider collider) {
		if (!collider_there(position + Vector3.Scale(transform.lossyScale, collider.center), Vector3.Scale(transform.lossyScale, collider.size) / 2)) {
			transform.position = position;
			return true;
		} else
			return false;
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