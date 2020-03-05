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

    void FixedUpdate() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void hit(Vector2 direction) {
        foreach (var particle in particles) {
            GameObject.Instantiate(particle, transform.position, Quaternion.identity);
        }
        /* ================================ */
        health -= 1;
        if (health <= 0)
            GameObject.Destroy(this.gameObject);
        else
            GetComponent<Animator>().SetTrigger("hurt");
    }
}
