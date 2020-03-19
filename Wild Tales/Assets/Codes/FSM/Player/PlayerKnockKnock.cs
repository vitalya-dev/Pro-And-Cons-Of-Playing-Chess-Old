using UnityEngine;
using System.Collections;

public class PlayerKnockKnock : BasicFSM<Player> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ============================================== */
        if (ob.attack_area.overlap<Envi>(LayerMask.GetMask("Top Layer"))) {
            ob.attack_area.overlap<Envi>(LayerMask.GetMask("Top Layer")).knock(ob.gameObject);
        }
    }
}