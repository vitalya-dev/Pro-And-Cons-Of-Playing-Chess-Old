using UnityEngine;
using System.Collections;

public class EnemyAttack : BasicFSM<Enemy> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ============================================== */
        rb.MovePosition(rb.position + (Vector2)rb.transform.up * -1);
        if (ob.attack_area.overlap<Player>(LayerMask.GetMask("Top Layer")))
            ob.attack_area.overlap<Player>(LayerMask.GetMask("Top Layer")).hit(-1 * ob.transform.up);
    }
}