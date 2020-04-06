using UnityEngine;
using System.Collections;

public class Physicsbody : MonoBehaviour {

    void Update() {
        apply_gravity(1);
    }

    void apply_gravity(float g) {
        if (!collider_there(transform.position + Vector3.forward * g * Time.deltaTime, GetComponent<BoxCollider>()))
            transform.position += Vector3.forward * g * Time.deltaTime;
    }

    public void move_position(Vector3 position, BoxCollider collision_box) {
        if (!collider_there(position, collision_box)) {
            transform.position = position;
        } else {
            SendMessage("on_collision", collider_there(position, GetComponent<BoxCollider>()), SendMessageOptions.DontRequireReceiver);
            // slide
            Vector3 a = position - transform.position;
            if (!collider_there(transform.position + new Vector3(a.x, 0, 0), collision_box))
                transform.position += new Vector3(a.x, 0, 0);
            else if (!collider_there(transform.position + new Vector3(0, a.y, 0), collision_box))
                transform.position += new Vector3(0, a.y, 0);
            else {
                SendMessage("on_collision_head", collider_there(position, GetComponent<BoxCollider>()), SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    Collider collider_there(Vector3 position, BoxCollider collision_box) {
        Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(collision_box.center) + (position - transform.position), collision_box.size / 2);
        foreach (var collider in colliders)
            if (collider.gameObject != gameObject) {
                return collider;
            }
        return null;
    }


    [SerializeField]
    private Color gizmo_color;
    void OnDrawGizmos() {
        Gizmos.color = gizmo_color;
        Gizmos.DrawCube(transform.TransformPoint(GetComponent<BoxCollider>().center), GetComponent<BoxCollider>().size);
    }
}
