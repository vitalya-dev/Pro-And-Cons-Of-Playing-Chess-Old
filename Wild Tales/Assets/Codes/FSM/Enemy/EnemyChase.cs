using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : BasicFSM<Enemy> {

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		/* ============================================== */
		NavMeshPath path = ob.calculate_path(ob.transform.position, ob.player.transform.position);
		for (int i = 0; i < path.corners.Length - 1; i++)
			Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
	}
}

// using UnityEngine;
// using System.Collections;

// public class InfighterChase : BasicFSM<Infighter> {
//     // Use this for initialization
//     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//         base.OnStateEnter(animator, stateInfo, layerIndex);
//         /* ======================================================= */
//     }

//     // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
//     override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//         if (!ob.player) {
//             animator.SetTrigger("finished");
//         }
//         else if (ob.attack_area.overlap<Player>(LayerMask.GetMask("Top Layer"))) {
//             ob.look_at(ob.player.transform.position - ob.transform.position);
//             animator.SetTrigger("attack");
//         }
//         else if (ob.attack_area.overlap<Envi>(LayerMask.GetMask("Middle Layer")) && Random.Range(0, 100) > 95) {
//             ob.look_at(ob.attack_area.overlap<Envi>(LayerMask.GetMask("Middle Layer")).transform.position - ob.transform.position);
//             animator.SetFloat("attack speed", 5.0f);
//             animator.SetTrigger("attack");
//         }
//         else {
//             ob.move_to_player();
//         }
//     }
// }

// -    /* ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ */
// -    Pathfinding.Path path = null;
// -    int waypoint;
// -
// -    public void move_to_player(bool clean_previous_path = false) {
// -        if (!player) // nothing to do here
// -            return;
// -        /* ============================================ */
// -        if (clean_previous_path) {
// -            path = null;
// -        }
// -        /* ============================================ */
// -        if (path == null || waypoint >= path.vectorPath.Count) {
// -            GetComponent<Pathfinding.Seeker>().StartPath((Vector2)transform.position, (Vector2)player.transform.position, on_path_complete);     
// -        }
// -        else {
// -            if (path != null && waypoint < path.vectorPath.Count) {
// -                Vector2 dir = path.vectorPath[waypoint] - transform.position;
// -                dir.Normalize();
// -                /* ============================================ */
// -                GetComponent<Rigidbody2D>().MovePosition(transform.position + (Vector3)dir * speed * Time.fixedDeltaTime);
// -                /* ============================================ */
// -                look_at(dir);
// -                /* ============================================ */
// -                float distance = Vector2.Distance(transform.position, path.vectorPath[waypoint]);
// -                if (distance <= 1) {
// -                    waypoint += 1;
// -                    if (waypoint >= path.vectorPath.Count) {
// -                        path = null;
// -                    }
// -                }
// -            }
// -        }
//      }

// -    void on_path_complete(Pathfinding.Path p) {
// -        if (!p.error) {
// -            path = p;
// -            waypoint = 0;
// -        }
// -        else {
// -            path = null;
// -        }
// -    }
// -    /* ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ */