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

    [HideInInspector]
    public Area attack_area;

    [HideInInspector]
    public Area kick_area;

    [HideInInspector]
    public BoxCollider walkable_box;

    Vector3 backup_position;
    Quaternion backup_rotation;

    void Awake() {
        LevelManager.restart_event.AddListener(restart);
        LevelManager.control_point_event.AddListener(control_point);
        /* ================================================== */
        backup_position = transform.position;
        backup_rotation = transform.rotation;
    }

    void Start() {
        if (transform.Find("Attack Area"))
            attack_area = transform.Find("Attack Area").GetComponent<Area>();
        if (transform.Find("Kick Area"))
            kick_area = transform.Find("Kick Area").GetComponent<Area>();
        if (transform.Find("Walkable Box"))
            walkable_box = transform.Find("Walkable Box").GetComponent<BoxCollider>();
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


    void on_collision(Collider collider) {
        Debug.Log(collider.name);
    }

}
