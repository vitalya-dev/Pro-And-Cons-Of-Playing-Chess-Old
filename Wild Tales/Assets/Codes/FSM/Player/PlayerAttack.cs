using UnityEngine;
using System.Collections;

public class PlayerAttack : BasicFSM<Player> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ============================================== */
        if (ob.attack_area.overlap<Envi>()) {
            ob.attack_area.overlap<Envi>().hit();
            animator.SetFloat("attack speed", 2.0f);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
        /* ============================================== */
        animator.SetFloat("attack speed", 1.0f);
    }
}