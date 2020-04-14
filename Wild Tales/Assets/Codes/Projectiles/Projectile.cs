using UnityEngine;
using System.Collections;

[SelectionBase]
public class Projectile : MonoBehaviour {
    public GameObject particle;

    private Vector3 velocity;

    public void hit(Vector3 direction, float force) {
		transform.Translate(Vector3.up * 0.1f);
		velocity = direction * force;
    }
 
    void Update() {
		transform.position += velocity * Time.deltaTime;
		if (velocity.sqrMagnitude > 0 && GetComponent<Area>().overlap<Collider>()) {
			GameObject.Instantiate(particle, transform.position, Quaternion.identity);
			GameObject.Destroy(gameObject);
			/* ====================================================== */
			Debug.Log(GetComponent<Area>().overlap<BoxCollider>());
		}

    }

}
