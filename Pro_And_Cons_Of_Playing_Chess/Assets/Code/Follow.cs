using System.Collections;
using UnityEngine;

public class Follow : MonoBehaviour {
	public Transform target;
	public float smooth;

	private Vector3 velocity;
	void Update() {
		transform.position = Vector3.SmoothDamp(
			transform.position,
			Vector3.Scale(target.position, new Vector3(1, 0, 1)) + Vector3.Scale(transform.position, new Vector3(0, 1, 0)),
			ref velocity, 
			0.25f
		);
	}
}