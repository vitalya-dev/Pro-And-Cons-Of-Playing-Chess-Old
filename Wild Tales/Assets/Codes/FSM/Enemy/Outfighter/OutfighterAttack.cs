using UnityEngine;
using System.Collections;

public class OutfighterAttack : BasicFSM<Outfighter> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ============================================== */
        if (!ob.player)
            return;
        /* ============================================== */
        Vector2 throwing_pos = rb.position + (Vector2)(-1 * ob.transform.up);
        GameObject throwing = Instantiate(ob.throwing, new Vector3(throwing_pos.x, throwing_pos.y, ob.throwing.transform.position.z), Quaternion.identity);
        throwing.GetComponent<Throwing>().throwing(-1 * ob.transform.up);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
        /* ============================================== */
        animator.SetFloat("attack speed", 1.0f);
    }
}