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

    [HideInInspector]
    public Area attack_area;

    [HideInInspector]
    public Area kick_area;

    public GameObject[] particles;

    Vector3 backup_position;
    Quaternion backup_rotation;

    void Awake() {
        LevelManager.gameobjects.Add(this.gameObject);
        /* ================================================== */
        backup_position = transform.position;
        backup_rotation = transform.rotation;
    }

    void Start() {
        attack_area = transform.Find("Attack Area").GetComponent<Area>();
        kick_area = transform.Find("Kick Area").GetComponent<Area>();
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
        if (Input.GetKeyDown(KeyCode.R)) {
            foreach (var ob in LevelManager.gameobjects) {
                ob.SendMessage("restart");
            }
        }


        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().inertia = 0f;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
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
    }
}
