using System.Collections;
using UnityEngine;

public class PhysicBody : MonoBehaviour {
    public Vector3 margin = Vector3.one;

    public Collider move_position(Vector3 position) {
        BoxCollider body_collider = GetComponent<BoxCollider>();
        Collider c = collider_there(position + body_collider.center,
                                    Vector3.Scale(body_collider.size / 2, margin));
        if (!c) transform.position = position;
        return c;
    }

    Collider collider_there(Vector3 center, Vector3 half_extents) {
        Collider[] colliders = Physics.OverlapBox(center, half_extents);
        foreach (var collider in colliders)
            if (collider.gameObject != gameObject) {
                return collider;
            }
        return null;
    }

    void Update() {
        BoxCollider body_collider = GetComponent<BoxCollider>();
        Collider c = collider_there(transform.position + body_collider.center,
                                    Vector3.Scale(body_collider.size / 2, margin));
        if (c) {
            Vector3[] directions = new [] {
                transform.forward, transform.right, transform.forward * -1, transform.right * -1,
            };
            /* ==================================================== */
            float s = 0.1f;
            bool done = false;
            while (!done) {
                foreach (var d in directions) {
                    if (!move_position(transform.position + d * s)) { // NO COLLIDERS
                        done = true;
                        break;
                    }
                }
                s *= 1.1f;
            }
            /* ==================================================== */
        }
    }
}
