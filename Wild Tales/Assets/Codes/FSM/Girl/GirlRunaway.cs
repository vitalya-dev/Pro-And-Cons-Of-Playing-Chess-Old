using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlRunaway : BasicFSM<Girl> {
	public Transform[] waypoints;

	Vector3[] corners;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator, stateInfo, layerIndex);
		/* ============================================== */
		corners = ai.calculate_path(ob.transform.position, waypoints[0].transform.position).corners.Clone() as Vector3[];
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		/* ============================================== */
		if (corners.Length > 1) {
			var d = Vector3.Scale(corners[1] - ob.transform.position, new Vector3(1, 0, 1));
			if (d.magnitude > 0.1) {
				pb.move_position(ob.transform.position + Vector3.ClampMagnitude(d.normalized * ob.speed * Time.deltaTime, d.magnitude), ob.GetComponent<BoxCollider>());
				ob.transform.rotation = Quaternion.LookRotation(d.normalized);
			} else {
				/********************* shifting corners *********************/
				Array.Copy(corners, 1, corners, 0, corners.Length - 1);
				Array.Resize(ref corners, corners.Length - 1);
				/********************* shifting corners *********************/
			}
		} else if (waypoints.Length > 1) {
			Array.Copy(waypoints, 1, waypoints, 0, waypoints.Length - 1);
			Array.Resize(ref waypoints, waypoints.Length - 1);
			corners = ai.calculate_path(ob.transform.position, waypoints[0].transform.position).corners.Clone() as Vector3[];
		}
	}
}