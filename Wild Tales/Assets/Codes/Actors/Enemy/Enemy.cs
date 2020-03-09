using UnityEngine;
using System.Collections;

abstract public class Enemy : MonoBehaviour {
    [HideInInspector]
    public Player player;

    abstract public void hit(Vector2 direction);
    abstract public void stun();

    public void look_at(Vector2 direction) {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, -1 * direction.normalized);
    }
}
