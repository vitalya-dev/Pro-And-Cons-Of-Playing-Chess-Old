using UnityEngine;
using System.Collections;

abstract public class Enemy : MonoBehaviour {
    [HideInInspector]
    public Player player;

    abstract public void hit(Vector2 direction);
    abstract public void stun();
}
