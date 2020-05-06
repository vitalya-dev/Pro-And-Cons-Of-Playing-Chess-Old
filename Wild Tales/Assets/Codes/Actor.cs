using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
	public float speed;
	
	[HideInInspector]
	public bool moving;

	public void move(Vector3 position) {
		StartCoroutine(__move(position));
	}

	IEnumerator __move(Vector3 position) {
		moving = true;
		GetComponent<AI>().calculate_path(position);
		while (GetComponent<AI>().move_by_path(speed)) {
			yield return null;
		}
		moving = false;
	}
}