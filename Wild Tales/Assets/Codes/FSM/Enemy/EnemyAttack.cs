using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : BasicFSM<Enemy> {
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		/* ============================================== */
		if (stateInfo.normalizedTime < 0.2f && ob.transform.Find("Jab") && ob.transform.Find("Jab").GetComponent<Area>().overlap<Player>()) {
			ob.transform.Find("Jab").GetComponent<Area>().overlap<Player>().hit();
		}

	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}
}

// public class EnemyAttack : BasicFSM<Enemy> {
//     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//         base.OnStateEnter(animator, stateInfo, layerIndex);
//         /* ============================================== */
//         if (ob.attack_area.overlap<Player>(LayerMask.GetMask("Top Layer"))) {
//             ob.attack_area.overlap<Player>(LayerMask.GetMask("Top Layer")).hit(-1 * ob.transform.up);
//         }
//         else if (ob.attack_area.overlap<Envi>(LayerMask.GetMask("Middle Layer"))) {
//             animator.SetFloat("attack speed", 5.0f);
//             ob.attack_area.overlap<Envi>(LayerMask.GetMask("Middle Layer")).hit(-1 * ob.transform.up);
//         }
//         rb.MovePosition(rb.position + (Vector2)rb.transform.up * -1);
//     }

//     public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//         base.OnStateExit(animator, stateInfo, layerIndex);
//         /* ============================================== */
//         animator.SetFloat("attack speed", 1.0f);
//     }
// }