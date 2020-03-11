using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {
    public float smooth;

    void Update() {
        Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 new_position = Vector2.Lerp(transform.position, new Vector3(mouse_position.x, mouse_position.y, transform.position.z), smooth * Time.fixedDeltaTime);
        transform.position = new_position;
    }
}
