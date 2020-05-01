using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour {
	public float speed;

	[HideInInspector]
	public Vector3 axis = Vector3.zero;

	public GameObject[] particles;

	Vector3 backup_position;
	Quaternion backup_rotation;

	PhysicBody pb;
	Animation am;

	void Awake() {
		pb = GetComponent<PhysicBody>();
		am = GetComponent<Animation>();
		/* ================================================== */

		LevelManager.restart_event.AddListener(restart);
		LevelManager.control_point_event.AddListener(control_point);
		/* ================================================== */
		backup_position = transform.position;
		backup_rotation = transform.rotation;
	}

	void Update() {
		axis.x = Input.GetAxisRaw("Horizontal");
		axis.z = Input.GetAxisRaw("Vertical");
		GetComponent<PlayMakerFSM>().FsmVariables.GetFsmFloat("axis").RawValue = axis.magnitude;
		/* ===================================================== */
		if (Input.GetButtonDown("Fire1"))
			GetComponent<PlayMakerFSM>().SendEvent("FIRE1");
		if (Input.GetButtonDown("Fire2"))
			GetComponent<PlayMakerFSM>().SendEvent("FIRE2");
		if (Input.GetButtonDown("Fire3"))
			GetComponent<PlayMakerFSM>().SendEvent("FIRE3");
	}

	public void restart() {
		transform.position = backup_position;
		transform.rotation = backup_rotation;
		gameObject.SetActive(true);
	}

	public void control_point() {
		backup_position = transform.position;
		backup_rotation = transform.rotation;
	}

	public IEnumerator idle_state() {
		/* ===================================================== */
		am.Play("Player Idle");
		/* ===================================================== */
		while (true) {
			Crosshair crosshair = GameObject.FindObjectOfType<Crosshair>();
			transform.rotation = Quaternion.LookRotation(
				Vector3.Scale(crosshair.transform.position - transform.position, new Vector3(1, 0, 1))
			);
			yield return null;
		}
	}

	public IEnumerator walk_state() {
		/* ===================================================== */
		am.Play("Player Walk");
		/* ===================================================== */
		while (true) {
			Vector3 offset_x = Vector3.Scale(axis, new Vector3(1, 0, 0)) * speed * Time.deltaTime;
			Vector3 offset_z = Vector3.Scale(axis, new Vector3(0, 0, 1)) * speed * Time.deltaTime;
			pb.move_position(transform.position + offset_x, GetComponent<BoxCollider>());
			pb.move_position(transform.position + offset_z, GetComponent<BoxCollider>());
			/* ===================================================== */
			Crosshair crosshair = GameObject.FindObjectOfType<Crosshair>();
			transform.rotation = Quaternion.LookRotation(
				Vector3.Scale(crosshair.transform.position - transform.position, new Vector3(1, 0, 1))
			);
			yield return null;
		}
	}

	public IEnumerator kick_state() {
		/* ===================================================== */
		am.Play("Player Kick");
		/* ===================================================== */
		GetComponent<Collider>().enabled = false;
		while (true) {
			yield return null;
		}
	}

	public void kick_state_cleanup() {
		GetComponent<Collider>().enabled = true;
	}

	public IEnumerator dead_state() {
		/* ===================================================== */
		am.Play("Hit State");
		/* ===================================================== */
		foreach (var particle in particles) {
			GameObject.Instantiate(particle, transform.position + Vector3.up, Quaternion.identity);
		}
		gameObject.SetActive(false);
		/* ===================================================== */
		return null;
	}

}