using UnityEngine;
using System.Collections;

public class OutfighterChase : BasicFSM<Outfighter> {
    Pathfinding.Path path;
    int waypoint;

    // Use this for initialization
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ======================================================= */
        if (ob.player)
            ob.GetComponent<Pathfinding.Seeker>().StartPath(rb.position, ob.player.GetComponent<Rigidbody2D>().position, on_path_complete);
        else
            animator.SetTrigger("finished");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!ob.player) {
            animator.SetTrigger("finished");
        }
        else if (ob.eye.see<Player>(LayerMask.GetMask("Top Layer")) && Random.Range(0, 100) > 95) {
            ob.look_at((Vector2)ob.player.transform.position - rb.position);
            animator.SetTrigger("attack");
        }
        else if (ob.attack_area.overlap<Envi>(LayerMask.GetMask("Middle Layer")) && Random.Range(0, 100) > 95) {
            ob.look_at((Vector2)ob.attack_area.overlap<Envi>(LayerMask.GetMask("Middle Layer")).transform.position - rb.position);
            animator.SetFloat("attack speed", 5.0f);
            animator.SetTrigger("attack");
        }
        else if (path != null && waypoint < path.vectorPath.Count) {
            Vector2 dir = (Vector2)path.vectorPath[waypoint] - rb.position;
            dir.Normalize();
            /* ============================================ */
            rb.MovePosition(rb.position + dir * ob.speed * Time.fixedDeltaTime);
            /* ============================================ */
            ob.look_at(dir);
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
