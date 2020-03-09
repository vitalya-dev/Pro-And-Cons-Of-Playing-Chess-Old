using UnityEngine;
using System.Collections;

public class Throwing : MonoBehaviour {

    public GameObject particle;

    private bool fly = false;

    public void throwing(Vector2 direction) {
        GetComponent<Rigidbody2D>().AddForce(direction * 4, ForceMode2D.Impulse);
        fly = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (fly) {
            collision.gameObject.SendMessage("hit", GetComponent<Rigidbody2D>().velocity.normalized, SendMessageOptions.DontRequireReceiver);
            /* ========================================================= */
            GameObject.Instantiate(particle, transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject);
        }
    }
}
