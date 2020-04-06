using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public GameObject particle;

    public void hit(Vector2 direction, float force ) {
        GetComponent<Physicsbody>().velocity = direction * force;
    }



    void on_collision(Collider collider) {
        
    }

}
