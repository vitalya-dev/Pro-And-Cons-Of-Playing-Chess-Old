using UnityEngine;
using System.Collections;


public class PlayerWalk : BasicFSM<Player> {
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        /* ============================================== */
        ob.transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector2)(ob.transform.position - GameObject.FindObjectOfType<Crosshair>().transform.position));
        /* ============================================== */
        rb.MovePosition(ob.transform.position + (Vector3)ob.movement * ob.speed * Time.fixedDeltaTime);
    }
}
