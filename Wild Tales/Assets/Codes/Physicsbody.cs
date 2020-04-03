using UnityEngine;
using System.Collections;

public class Physicsbody : MonoBehaviour {
    public Vector3 slide = Vector3.zero;

    void Update() {
        /*        RaycastHit hitInfo;
                if (Physics.BoxCast(new Vector3(0, 5, -1), new Vector3(0.5f, 0.5f, 0.5f), Vector3.forward, out hitInfo, Quaternion.identity, 1.0f

                {
                    print(hitInfo.collider.name);
                } else {
                    print("Nothing hit");
                }*/
        /*        Physics.Raycast*/
        Debug.DrawRay(transform.position + -transform.up, -transform.up, Color.yellow);
    }


    public void move_position(Vector3 position, bool slide = true) {
        bool can_move = true;
        /* =========================================== */
        Collider[] colliders = Physics.OverlapBox(position + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size / 2);
        foreach (var collider in colliders)
            if (collider.gameObject != gameObject) {
                can_move = false;
                break;
            }
        /* =========================================== */
        if (can_move)
            transform.position = position;
        else if (slide) {
            // slide
            Vector3 a = position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position + GetComponent<BoxCollider>().center, a, out hit, 2)) {
                Vector3 n = -1 * hit.normal;
                move_position(transform.position + a - (Vector3.Dot(a, n) * n), false);
            }
        }
    }

    bool can_move(Vector3 position) {
        Collider[] colliders = Physics.OverlapBox(position + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size / 2);
        foreach (var collider in colliders)
            if (collider.gameObject != gameObject) {
                return true;
            }
        return false;
    }
}
