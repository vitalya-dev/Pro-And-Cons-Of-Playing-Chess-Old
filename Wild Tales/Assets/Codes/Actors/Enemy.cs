using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float speed;
    [HideInInspector]
    public Eye eye;
    [HideInInspector]
    public Area attack_area;
    [HideInInspector]
    public Player player;

    public int health;

    public GameObject[] particles;

    void Start() {
        eye = transform.Find("Eye").GetComponent<Eye>();
        attack_area = transform.Find("Attack Area").GetComponent<Area>();
    }

    void Update() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void hit(Vector2 direction) {
        foreach (var particle in particles) {
            Debug.Log(particle.transform.position);
            GameObject.Instantiate(particle, new Vector3(transform.position.x, transform.position.y, particle.transform.position.z), Quaternion.identity);
        }
        /* ================================ */
        health -= 1;
        if (health <= 0)
            GameObject.Destroy(this.gameObject);
        else
            GetComponent<Animator>().SetTrigger("hurt");
    }

    public void stun() {
        GetComponent<Animator>().SetTrigger("stun");
    }
}
