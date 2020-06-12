using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

	void Update() {
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.down;
	}
}