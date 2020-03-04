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

}
