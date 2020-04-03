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


    public void move_position(Vector3 position) {
        if (can_move(position))
            transform.position = position;
        else {
            // slide
            Vector3 a = position - transform.position;
            if (can_move(transform.position + new Vector3(a.x, 0, 0)))
                transform.position += new Vector3(a.x, 0, 0);
            else if (can_move(transform.position + new Vector3(0, a.y, 0)))
                transform.position += new Vector3(0, a.y, 0);
        }
    }


    bool can_move(Vector3 position) {
        Collider[] colliders = Physics.OverlapBox(position + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size / 2);
        foreach (var collider in colliders)
            if (collider.gameObject != gameObject) {
                return false;
            }
        return true;
    }
}
