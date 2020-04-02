using UnityEngine;
using System.Collections;

public class Molotov : Projectile {
    override protected void on_collision(Collision2D collision) {
        if (collision.gameObject.GetComponent<Building>())
            collision.gameObject.SendMessage("burn", SendMessageOptions.DontRequireReceiver);
    }
}
