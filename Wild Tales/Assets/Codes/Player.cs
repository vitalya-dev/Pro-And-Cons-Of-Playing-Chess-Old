using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed;

    [HideInInspector]
    public Vector2 movement;

    [HideInInspector]
    public Vector2 mouse_input;

    [HideInInspector]
    public Area attack_area;

    [HideInInspector]
    public Area kick_area;

    void Start() {
        attack_area = transform.Find("Attack Area").GetComponent<Area>();
        kick_area = transform.Find("Kick Area").GetComponent<Area>();
    }

    void FixedUpdate() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        GetComponent<Animator>().SetFloat("movement", movement.magnitude);

        mouse_input.x = Input.GetAxisRaw("Mouse X");
        mouse_input.y = Input.GetAxisRaw("Mouse Y");


        if (Input.GetButtonDown("Fire1"))
            GetComponent<Animator>().SetTrigger("fire1");
        if (Input.GetButtonDown("Fire2"))
            GetComponent<Animator>().SetTrigger("fire2");
        if (Input.GetButtonDown("Fire3"))
            GetComponent<Animator>().SetTrigger("fire3");
    }
}
