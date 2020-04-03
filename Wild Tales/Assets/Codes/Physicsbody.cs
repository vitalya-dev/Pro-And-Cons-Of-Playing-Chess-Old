using UnityEngine;
using System.Collections;

public class Physicsbody : MonoBehaviour {

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


    public void move_position(Vector3 position) {
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
        else slide(position);
    }

    private void slide(Vector3 position) {
        Debug.Log(position);
    }
}
