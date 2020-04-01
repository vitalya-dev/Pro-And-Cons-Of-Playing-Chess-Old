using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {
    public GameObject particle;
    private bool fly = false;

    public void fire(Vector2 direction) {
        gameObject.layer = LayerMask.NameToLayer("Middle Layer");
        GetComponent<Rigidbody2D>().AddForce(direction * 25, ForceMode2D.Impulse);
        /* ========================================================= */
        fly = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (fly) {
            on_collision(collision);
            GameObject.Instantiate(particle, transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject);
        }
    }

    protected abstract void on_collision(Collision2D collision);

}
