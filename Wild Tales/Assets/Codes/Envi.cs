using UnityEngine;
using System.Collections;

public class Envi : MonoBehaviour {

    public GameObject particle;

    public void hit(Vector2 direction) {
        GameObject.Instantiate(particle, transform.position, Quaternion.identity);
        GameObject.Destroy(this.gameObject);
    }

    public void knock() {
        Debug.Log("Knock " + GetInstanceID());
    }
}
