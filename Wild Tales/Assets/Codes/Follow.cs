using System.Collections;
using UnityEngine;

public class Follow : MonoBehaviour {
	public Transform target;
	public float smooth;

	void Update() {
		Vector3 new_position = Vector3.Lerp(transform.position, target.position, smooth * Time.deltaTime);
		transform.position += Vector3.Scale(new_position - transform.position, new Vector3(1, 0, 1));
	}
}