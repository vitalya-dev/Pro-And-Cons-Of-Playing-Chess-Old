using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {
	public float length;
	[Range(1, 360)]
	public float angle = 1;

	public T see<T>(int mask = ~0) where T : class {
		for (float i = -angle / 2; i <= angle / 2; i += angle / 48) {
			Collider2D coll = Physics2D.Raycast(
				transform.position,
				Quaternion.Euler(0, 0, i) * transform.up,
				length,
				mask
			).collider;
			if (coll && coll.GetComponent<T>() != null)
				return coll.GetComponent<T>();
		}
		return null;
	}

	public T[] see_all<T>(int mask = ~0) {
		List<T> ts = new List<T>();
		for (float i = -angle / 2; i <= angle / 2; i += angle / 48) {
			Collider2D coll = Physics2D.Raycast(
				transform.position,
				Quaternion.Euler(0, 0, i) * transform.up,
				length,
				mask
			).collider;
			if (coll && coll.GetComponent<T>() != null)
				ts.Add(coll.GetComponent<T>());
		}
		return ts.ToArray();
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		for (float i = -angle / 2; i <= angle / 2; i += angle / 48)
			Gizmos.DrawRay(transform.position, length * (Quaternion.Euler(0, 0, i) * transform.up));
	}
}