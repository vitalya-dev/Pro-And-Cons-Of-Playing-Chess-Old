using UnityEngine;
using System.Collections;

public class Physicsbody : MonoBehaviour {
    public void move_position(Vector3 position) {
        if (can_move(position)) {
            transform.position = position;
        } else {
            // slide
            Vector3 a = position - transform.position;
            if (can_move(transform.position + new Vector3(a.x, 0, 0)))
                transform.position += new Vector3(a.x, 0, 0);
            else if (can_move(transform.position + new Vector3(0, a.y, 0)))
                transform.position += new Vector3(0, a.y, 0);
        }
    }

    bool can_move(Vector3 position) {
        Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(GetComponent<BoxCollider>().center) + (position - transform.position), GetComponent<BoxCollider>().size / 2);
        foreach (var collider in colliders)
            if (collider.gameObject != gameObject) {
                return false;
            }
        return true;
    }
}
