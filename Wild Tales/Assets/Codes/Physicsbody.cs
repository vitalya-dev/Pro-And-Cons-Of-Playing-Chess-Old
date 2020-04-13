using System.Collections;
using UnityEngine;

public class Physicsbody : MonoBehaviour {

	void Update() {
	
	}

	Collider collider_there() {
		Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, transform.rotation);
		foreach (var collider in colliders)
			if (collider.gameObject != gameObject) {
				return collider;
			}
		return null;
	}
}