using UnityEngine;
using System.Collections;

public class EnemyChase : BasicFSM<Enemy> {
    Pathfinding.Path path;
    int waypoint;

    // Use this for initialization
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ======================================================= */
        if (ob.player)
            ob.GetComponent<Pathfinding.Seeker>().StartPath(rb.position, ob.player.GetComponent<Rigidbody2D>().position, on_path_complete);
        else
            ob.GetComponent<Animator>().SetTrigger("idle");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!ob.player) {
            ob.GetComponent<Animator>().SetTrigger("idle");
        }
        else if (ob.attack_area.overlap<Player>(LayerMask.GetMask("Top Layer")))
            ob.GetComponent<Animator>().SetTrigger("attack");
        else if (ob.attack_area.overlap<Envi>(LayerMask.GetMask("Middle Layer")))
            ob.GetComponent<Animator>().SetTrigger("attack");
        else if (path != null && waypoint < path.vectorPath.Count) {
            Vector2 dir = (Vector2)path.vectorPath[waypoint] - rb.position;
            dir.Normalize();
            /* ============================================ */
            rb.MovePosition(rb.position + dir * ob.speed * Time.fixedDeltaTime);
            /* ============================================ */
            ob.transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector2)(-1 * dir));
            /* ============================================ */
            float distance = Vector2.Distance(rb.position, path.vectorPath[waypoint]);
            if (distance <= 1) {
                waypoint += 1;
                if (waypoint >= path.vectorPath.Count) {
                    path = null;
                    waypoint = 0;
                    ob.GetComponent<Pathfinding.Seeker>().StartPath(rb.position, ob.player.GetComponent<Rigidbody2D>().position, on_path_complete);
                }
            }
        }
    }

    void on_path_complete(Pathfinding.Path p) {
        if (!p.error) {
            path = p;
            waypoint = 0;
        }
        else {
            path = null;
        }
    }
}
