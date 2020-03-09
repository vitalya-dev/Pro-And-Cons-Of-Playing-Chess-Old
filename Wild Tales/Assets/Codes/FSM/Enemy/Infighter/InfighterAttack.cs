using UnityEngine;
using System.Collections;

public class InfighterAttack : BasicFSM<Infighter> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ============================================== */
        if (ob.attack_area.overlap<Player>(LayerMask.GetMask("Top Layer"))) {
            ob.attack_area.overlap<Player>(LayerMask.GetMask("Top Layer")).hit(-1 * ob.transform.up);
        }
        else if (ob.attack_area.overlap<Envi>(LayerMask.GetMask("Middle Layer"))) {
            animator.SetFloat("attack speed", 5.0f);
            ob.attack_area.overlap<Envi>(LayerMask.GetMask("Middle Layer")).hit(-1 * ob.transform.up);
        }
        rb.MovePosition(rb.position + (Vector2)rb.transform.up * -1);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
        /* ============================================== */
        animator.SetFloat("attack speed", 1.0f);
    }
}