using UnityEngine;
using System.Collections;

public class PlayerKick : BasicFSM<Player> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ======================================================== */
        if (ob.attack_area.overlap<Projectile>(LayerMask.GetMask("Floor Layer"))) {
            ob.attack_area.overlap<Projectile>(LayerMask.GetMask("Floor Layer")).hit(-1 * ob.transform.up);
        }
    }
}
