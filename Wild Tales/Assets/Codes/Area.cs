using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Area : MonoBehaviour {
	public T overlap<T>() where T : class {
		Collider[] colliders;
		/* ============================================ */
		if (GetComponent<BoxCollider>())
			colliders = Physics.OverlapBox(
				transform.position + GetComponent<BoxCollider>().center,
				GetComponent<BoxCollider>().size / 2,
				transform.rotation
			);
		else if (GetComponent<SphereCollider>())
			colliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);
		else
			colliders = new Collider[0];
		/* ============================================ */
		foreach (var collider in colliders) {
			if (collider.gameObject != gameObject && collider.GetComponent<T>() != null) {
				return collider.GetComponent<T>();
			}
		}
		return null;
	}
}