using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public string particle = "Yellow";

    bool is_active;
    
    public Vector3 opend = Vector3.right;

    void Awake() {
        is_active = gameObject.activeSelf;
    }

    public void knock() {
        EventsActions.door_knocked_stack.Push(this);
    }

    public void hit() {
        GameObject p =
            Instantiate(Resources.Load("Particles/" + particle, typeof(GameObject))) as GameObject;
        p.transform.position = transform.position;
        /* ================================= */
        gameObject.SetActive(false);
    }

    public void open(Vector3 dir) {
        transform.rotation *= Quaternion.FromToRotation(Vector3.forward, opend);
        opend *= -1;
    }


    public void restart() {
        gameObject.SetActive(is_active);
    }

    public void control_point() {
        is_active = gameObject.activeSelf;
    }
}
