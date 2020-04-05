using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public GameObject particle;

    public void hit(Vector2 direction) {
        GetComponent<Physicsbody>().velocity = direction * 35;
    }



    void on_collision(Collider collider) {
        
    }

}
