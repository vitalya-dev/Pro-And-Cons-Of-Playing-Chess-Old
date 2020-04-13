using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Area : MonoBehaviour {
	public T overlap<T>() {
		Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, transform.rotation);
		foreach (var collider in colliders) {
			if (collider.gameObject != gameObject && collider.GetComponent<T>() != null) {
				return collider.GetComponent<T>();
			}
		}
		return default;
	}
}