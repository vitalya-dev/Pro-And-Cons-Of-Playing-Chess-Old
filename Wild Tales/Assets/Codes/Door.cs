using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public GameObject particle;

	bool is_active;

	void Awake() {
		is_active = gameObject.activeSelf;
	}

	public void knock() {
		FindObjectOfType<TriggerSystem>().door_knocked_stack.Push(this);
	}

	public void hit() {
		GameObject.Instantiate(particle, transform.position, Quaternion.identity);
		gameObject.SetActive(false);
	}

	public void restart() {
		gameObject.SetActive(is_active);
	}

	public void control_point() {
		is_active = gameObject.activeSelf;
	}
}