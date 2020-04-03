using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour {
    public float speed;

    [HideInInspector]
    public Vector2 movement;

    [HideInInspector]
    public Vector2 mouse_input;

    public GameObject[] particles;

    Vector3 backup_position;
    Quaternion backup_rotation;

    void Awake() {
        LevelManager.restart_event.AddListener(restart);
        LevelManager.control_point_event.AddListener(control_point);
        /* ================================================== */
        backup_position = transform.position;
        backup_rotation = transform.rotation;
    }


    void Update() {
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

    public void kill() {
        hit(Vector2.zero);
    }

    public void hit(Vector2 direction) {
        foreach (var particle in particles) {
            Debug.Log(particle.transform.position);
            GameObject.Instantiate(particle, new Vector3(transform.position.x, transform.position.y, particle.transform.position.z), Quaternion.identity);
        }
        PlayMakerFSM.BroadcastEvent("WASTED");
        gameObject.SetActive(false);
    }

    public void restart() {
        transform.position = backup_position;
        transform.rotation = backup_rotation;
        gameObject.SetActive(true);
    }

    public void control_point() {
        backup_position = transform.position;
        backup_rotation = transform.rotation;
    }


}
