using UnityEngine;
using System.Collections;

public class Infighter : Enemy {
    [HideInInspector]
    public Eye eye;
    [HideInInspector]
    public Area attack_area;

    public int health;

    public GameObject[] particles;

    void Start() {
        eye = transform.Find("Eye").GetComponent<Eye>();
        attack_area = transform.Find("Attack Area").GetComponent<Area>();
    }

    void Update() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    override public void hit(Vector2 direction) {
        foreach (var particle in particles) {
            GameObject.Instantiate(particle, new Vector3(transform.position.x, transform.position.y, particle.transform.position.z), Quaternion.identity);
        }
        /* ================================ */
        health -= 1;
        if (health <= 0)
            GameObject.Destroy(this.gameObject);
        else
            GetComponent<Animator>().SetTrigger("hurt");
    }

    override public void stun() {
        GetComponent<Animator>().SetTrigger("stun");
    }
}
