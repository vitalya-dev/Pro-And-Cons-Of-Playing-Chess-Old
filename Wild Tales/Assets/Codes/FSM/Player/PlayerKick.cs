using UnityEngine;
using System.Collections;

public class PlayerKick : BasicFSM<Player> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ============================================== */
        ob.GetComponent<Collider>().enabled = false;
        if (ob.kick_area.overlap<Projectile>()) {
            ob.kick_area.overlap<Projectile>().hit(-1 * ob.transform.up, 10);
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
        /* ============================================== */
        ob.GetComponent<Collider>().enabled = true;
    }
}
