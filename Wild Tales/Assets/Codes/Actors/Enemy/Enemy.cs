using UnityEngine;
using System.Collections;

abstract public class Enemy : MonoBehaviour {
     abstract public void hit(Vector2 direction);
     abstract public void stun();
}
