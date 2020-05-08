using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;

public class Level : MonoBehaviour {
	void Awake() {
		GameObject gui = new GameObject("GUI");
		/* ============================================== */
		GameObject crosshair = Instantiate(Resources.Load("Crosshair", typeof(GameObject))) as GameObject;
		crosshair.transform.position = Vector3.up * (Camera.main.transform.position.y - 1);
		crosshair.transform.parent = gui.transform;
	}

	void Update() {
		GameObject triggers = GameObject.Find("Triggers");
		for (int i = 0; i < triggers.transform.childCount; i++) {
			tick(triggers.transform.GetChild(i).gameObject);
		}
	}

	void tick(GameObject trigger_action) {
		if (trigger_action.GetComponent<PandaBehaviour>())
			trigger_action.GetComponent<PandaBehaviour>().Tick();
		for (int i = 0; i < trigger_action.transform.childCount; i++) {
			tick(trigger_action.transform.GetChild(i).gameObject);
		}
	}
}