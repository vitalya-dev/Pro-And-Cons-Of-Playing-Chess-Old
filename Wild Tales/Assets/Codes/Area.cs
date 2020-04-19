using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Area : MonoBehaviour {
	public T overlap<T>()where T : class {
		Collider[] colliders = new Collider[0];
		if (GetComponent<BoxCollider>())
			colliders = Physics.OverlapBox(
				transform.position + Vector3.Scale(transform.lossyScale, GetComponent<BoxCollider>().center),
				Vector3.Scale(transform.lossyScale, GetComponent<BoxCollider>().size) / 2,
				transform.rotation
			);
		else if (GetComponent<SphereCollider>())
			colliders = Physics.OverlapSphere(
				transform.position,
				Mathf.Max(new float[3] { transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z }) * GetComponent<SphereCollider>().radius
			);

		foreach (var collider in colliders) {
			if (collider.gameObject != gameObject && collider.GetComponent<T>() != null) {
				return collider.GetComponent<T>();
			}
		}
		return null;
	}
}