using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour {

    public float speed;

    [HideInInspector]
    public Vector3 movement;


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

    void Start() {
    }


    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        GetComponent<Animator>().SetFloat("movement", movement.magnitude);

        if (Input.GetButtonDown("Fire1"))
            GetComponent<Animator>().SetTrigger("fire1");
        if (Input.GetButtonDown("Fire2"))
            GetComponent<Animator>().SetTrigger("fire2");
        if (Input.GetButtonDown("Fire3"))
            GetComponent<Animator>().SetTrigger("fire3");
    }

    public void hit() {
        foreach (var particle in particles) {
            GameObject.Instantiate(particle, new Vector3(transform.position.x, transform.position.y, particle.transform.position.z), Quaternion.identity);
        }
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
