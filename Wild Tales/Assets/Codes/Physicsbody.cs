using UnityEngine;
using System.Collections;

public class Physicsbody : MonoBehaviour {
    public Vector3 velocity = Vector3.zero;
    public float friction;

    void Update() {
        if (velocity.sqrMagnitude > 0) {
            move_position(transform.position + velocity * Time.fixedDeltaTime);
            velocity -=  friction * velocity.magnitude * Time.deltaTime * velocity.normalized;
        } else {
            velocity = Vector3.zero;
        }
    }

    public void move_position(Vector3 position) {
        if (!collider_there(position)) {
            transform.position = position;
        } else {
            SendMessage("on_collision", collider_there(position), SendMessageOptions.DontRequireReceiver);
            // slide
            Vector3 a = position - transform.position;
            if (!collider_there(transform.position + new Vector3(a.x, 0, 0)))
                transform.position += new Vector3(a.x, 0, 0);
            else if (!collider_there(transform.position + new Vector3(0, a.y, 0)))
                transform.position += new Vector3(0, a.y, 0);
            else {
                SendMessage("on_collision_head", collider_there(position), SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    Collider collider_there(Vector3 position) {
        Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(GetComponent<BoxCollider>().center) + (position - transform.position), GetComponent<BoxCollider>().size / 2);
        foreach (var collider in colliders)
            if (collider.gameObject != gameObject) {
                BoxCollider other = collider.GetComponent<BoxCollider>();
                if ((transform.TransformPoint(GetComponent<BoxCollider>().center) - other.transform.TransformPoint(other.center)).z > -1) {
                    return other;
                }
            }
        return null;
    }



    void on_collision_head(Collider collider) {
        velocity = Vector3.zero;
    }


    [SerializeField]
    private Color gizmo_color;
    void OnDrawGizmos() {
        Gizmos.color = gizmo_color;
        Gizmos.DrawCube(transform.TransformPoint(GetComponent<BoxCollider>().center), GetComponent<BoxCollider>().size);
    }
}
