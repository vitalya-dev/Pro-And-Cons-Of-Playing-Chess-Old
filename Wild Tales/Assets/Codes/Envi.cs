using UnityEngine;
using System.Collections;

public class Envi : MonoBehaviour {

    public GameObject particle;

    public void hit(Vector2 direction) {
        GameObject.Instantiate(particle, transform.position, Quaternion.identity);
    }

    public void knock() {
    }
}
