using UnityEngine;
using System.Collections;

[SelectionBase]
public class Projectile : MonoBehaviour {
    public GameObject particle;

    private Vector3 velocity;

    public void hit(Vector2 direction, float force) {
        transform.Translate(0, 0, -1);
        velocity = direction * force;
    }

    void Update() {
        if (velocity.magnitude >= 1)
            GetComponent<Physicsbody>().move_position(transform.position + velocity * Time.fixedDeltaTime, GetComponent<BoxCollider>());

    }

    void on_collision(Collider collider) {
        GameObject.Instantiate(particle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

}
