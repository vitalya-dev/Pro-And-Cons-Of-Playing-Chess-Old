using UnityEngine;
using System.Collections;


[SelectionBase]
abstract public class Enemy : MonoBehaviour {
    public float speed;
    [HideInInspector]
    public Player player;

    abstract public void hit(Vector2 direction);
    abstract public void stun();

    public void look_at(Vector2 direction) {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, -1 * direction.normalized);
    }

    void Update() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().inertia = 0f;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }


    /* ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ */
    Pathfinding.Path path = null;
    int waypoint;

    public void move_to_player(bool clean_previous_path = false) {
        if (!player) // nothing to do here
            return;
        /* ============================================ */
        if (clean_previous_path) {
            path = null;
        }
        /* ============================================ */
        if (path == null || waypoint >= path.vectorPath.Count) {
            GetComponent<Pathfinding.Seeker>().StartPath((Vector2)transform.position, (Vector2)player.transform.position, on_path_complete);
        }
        else {
            if (path != null && waypoint < path.vectorPath.Count) {
                Vector2 dir = path.vectorPath[waypoint] - transform.position;
                dir.Normalize();
                /* ============================================ */
                transform.position += (Vector3)dir * speed * Time.fixedDeltaTime;
                /* ============================================ */
                look_at(dir);
                /* ============================================ */
                float distance = Vector2.Distance(transform.position, path.vectorPath[waypoint]);
                if (distance <= 1) {
                    waypoint += 1;
                    if (waypoint >= path.vectorPath.Count) {
                        path = null;
                    }
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
    /* ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ */

}



