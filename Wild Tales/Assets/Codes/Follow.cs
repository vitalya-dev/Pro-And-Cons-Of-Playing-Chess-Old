using System.Collections;
using UnityEngine;

public class Follow : MonoBehaviour {
	public Transform target;
	public float smooth = 10f;

	void LateUpdate () {
		if (target != null && Vector2.Distance (target.position, transform.position) > 0.5) {
			Vector3 new_position = Vector2.Lerp (transform.position, target.position, smooth * Time.fixedDeltaTime);
			transform.position = new Vector3 (new_position.x, new_position.y, transform.position.z);
		}
	}
}