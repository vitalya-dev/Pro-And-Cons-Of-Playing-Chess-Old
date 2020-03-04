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

    void Start() {
        eye = transform.Find("Eye").GetComponent<Eye>();
        attack_area = transform.Find("Attack Area").GetComponent<Area>();
    }

    void FixedUpdate() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
