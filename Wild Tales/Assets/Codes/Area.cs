using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Area : MonoBehaviour {
	public T overlap<T>() where T : class {
		Collider[] colliders = new Collider[0];
		if (GetComponent<BoxCollider>())
			colliders = Physics.OverlapBox(
				transform.position,
				Vector3.Scale(transform.localScale, GetComponent<BoxCollider>().size) / 2,
				transform.rotation
			);
		else if (GetComponent<SphereCollider>())
			colliders = Physics.OverlapSphere(
				transform.position,
				Mathf.Max(new float[3]{transform.localScale.x, transform.localScale.y, transform.localScale.z}) * GetComponent<SphereCollider>().radius
			);

		foreach (var collider in colliders) {
			Debug.Log(collider.name);
			if (collider.gameObject != gameObject && collider.GetComponent<T>() != null) {
				return collider.GetComponent<T>();
			}
		}
		return null;
	}
}