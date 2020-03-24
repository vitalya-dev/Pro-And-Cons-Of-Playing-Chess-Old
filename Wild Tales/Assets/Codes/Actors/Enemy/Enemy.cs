using UnityEngine;
using System.Collections;
using UnityEngine.Events;


[SelectionBase]
abstract public class Enemy : MonoBehaviour {
    public float speed;
    [HideInInspector]
    public Eye eye;
    [HideInInspector]
    public Area body_area;
    [HideInInspector]
    public Player player;

    public int health;

    public GameObject[] particles;

    public static UnityEvent kill_event = new UnityEvent();

    virtual protected void Start() {
        eye = transform.Find("Eye").GetComponent<Eye>();
        body_area = GetComponent<Area>();
    }

    public void hit(Vector2 direction) {
        foreach (var particle in particles) {
            GameObject.Instantiate(particle, new Vector3(transform.position.x, transform.position.y, particle.transform.position.z), Quaternion.identity);
        }
        /* ================================ */
        health -= 1;
        if (health <= 0) {
            GameObject.Destroy(this.gameObject);
            /* ================================ */
            kill_event.Invoke();
        }
        else
            GetComponent<Animator>().SetTrigger("hurt");
    }

    public void stun() {
        GetComponent<Animator>().SetTrigger("stun");
    }

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
                GetComponent<Rigidbody2D>().MovePosition(transform.position + (Vector3)dir * speed * Time.fixedDeltaTime);
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



