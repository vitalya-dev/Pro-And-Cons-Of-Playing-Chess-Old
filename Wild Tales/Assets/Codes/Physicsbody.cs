using UnityEngine;
using System.Collections;

public class Physicsbody : MonoBehaviour {
    [SerializeField]
    private Color gizmo_color;

    public Vector3 velocity = Vector3.zero;

    void Update() {
        if (velocity.sqrMagnitude > 0)
            move_position(transform.position + velocity * Time.deltaTime);
    }

    public void move_position(Vector3 position) {
        if (can_move(position)) {
            transform.position = position;
        } else {
            // slide
            Vector3 a = position - transform.position;
            if (can_move(transform.position + new Vector3(a.x, 0, 0), false))
                transform.position += new Vector3(a.x, 0, 0);
            else if (can_move(transform.position + new Vector3(0, a.y, 0), false))
                transform.position += new Vector3(0, a.y, 0);
        }
    }

    bool can_move(Vector3 position, bool send_message = true) {
        Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(GetComponent<BoxCollider>().center) + (position - transform.position), GetComponent<BoxCollider>().size / 2);
        foreach (var collider in colliders)
            if (collider.gameObject != gameObject) {
                BoxCollider other = collider.GetComponent<BoxCollider>();
                if ((transform.TransformPoint(GetComponent<BoxCollider>().center) - other.transform.TransformPoint(other.center)).z > -1) {
                    SendMessage("on_collision", collider);
                    return false;
                }
            }
        return true;
    }

    void on_collision(Collider collider) {
        velocity = Vector3.zero;
    }

    void OnDrawGizmos() {
        Gizmos.color = gizmo_color;
        Gizmos.DrawCube(transform.TransformPoint(GetComponent<BoxCollider>().center), GetComponent<BoxCollider>().size);
    }
}
