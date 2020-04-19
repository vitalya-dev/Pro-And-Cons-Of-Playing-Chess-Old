using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : BasicFSM<Enemy> {
	Vector3[] corners;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator, stateInfo, layerIndex);
		/* ============================================== */
		if (ob.player.isActiveAndEnabled)
			corners = ob.calculate_path(ob.transform.position, ob.player.transform.position).corners.Clone() as Vector3[];
		else
			ob.GetComponent<Animator>().SetTrigger("finished");
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		/* ============================================== */
		/********************* chase *********************/
		if (corners.Length > 1) {
			var d = Vector3.Scale(corners[1] - ob.transform.position, new Vector3(1, 0, 1));
			if (d.magnitude > 0.1) {
				ob.transform.position += Vector3.ClampMagnitude(d.normalized * ob.speed * Time.deltaTime, d.magnitude);
				ob.transform.rotation = Quaternion.LookRotation(d.normalized);
			} else {
				ob.transform.position = corners[1] + Vector3.Scale(ob.transform.position, new Vector3(0, 1, 0));
				/********************* shifting corners *********************/
				Array.Copy(corners, 1, corners, 0, corners.Length - 1);
				Array.Resize(ref corners, corners.Length - 1);
				/********************* shifting corners *********************/
			}
			/********************* debug *********************/
			for (int i = 0; i < corners.Length - 1; i++) {
				Debug.DrawLine(corners[i], corners[i + 1], Color.red);
			}
			/********************* debug *********************/
		} else {
			corners = ob.calculate_path(ob.transform.position, ob.player.transform.position).corners.Clone() as Vector3[];
		}
		/********************* chase *********************/

		/********************* attack *********************/
		if (ob.transform.Find("Attack Area") && ob.transform.Find("Attack Area").GetComponent<Area>().overlap<Player>()) {
			ob.GetComponent<Animator>().SetTrigger("attack");
		}
		/********************* attack *********************/
	}
}