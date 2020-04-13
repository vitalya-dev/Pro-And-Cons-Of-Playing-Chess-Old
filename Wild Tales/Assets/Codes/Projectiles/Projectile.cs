using UnityEngine;
using System.Collections;

[SelectionBase]
public class Projectile : MonoBehaviour {
    public GameObject particle;

    private Vector3 velocity;

    public void hit(Vector3 direction, float force) {
		transform.Translate(Vector3.up);
    }
 
    void Update() {

    }

    void on_collision(Collider collider) {

    }

}
