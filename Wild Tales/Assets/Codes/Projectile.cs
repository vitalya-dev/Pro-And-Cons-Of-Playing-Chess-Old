using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public GameObject particle;
    private bool fly = false;

    public void hit(Vector2 direction) {
        gameObject.layer = LayerMask.NameToLayer("Middle Layer");
        GetComponent<Rigidbody2D>().AddForce(direction * 25, ForceMode2D.Impulse);
        /* ========================================================= */
        fly = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (fly) {
            collision.gameObject.SendMessage("stun", SendMessageOptions.DontRequireReceiver);
            /* ========================================================= */
            GameObject.Instantiate(particle, transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject);
        }
    }
}
