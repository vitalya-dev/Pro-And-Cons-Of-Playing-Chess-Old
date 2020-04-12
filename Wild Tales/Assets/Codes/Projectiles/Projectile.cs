using UnityEngine;
using System.Collections;

[SelectionBase]
public class Projectile : MonoBehaviour {
    public GameObject particle;

    private Vector3 velocity;

    public void hit(Vector2 direction, float force) {

    }

    void Update() {

    }

    void on_collision(Collider collider) {
        GameObject.Instantiate(particle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

}
