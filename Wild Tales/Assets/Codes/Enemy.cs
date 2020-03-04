using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float speed;
    [HideInInspector]
    public Eye eye;
    [HideInInspector]
    public Player player;

    void Start() {
        eye = transform.Find("Eye").GetComponent<Eye>();
    }

    void FixedUpdate() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
