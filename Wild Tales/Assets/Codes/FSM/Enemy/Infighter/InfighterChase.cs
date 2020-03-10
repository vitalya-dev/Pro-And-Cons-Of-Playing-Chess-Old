using UnityEngine;
using System.Collections;

public class InfighterChase : BasicFSM<Infighter> {
    // Use this for initialization
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ======================================================= */
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!ob.player) {
            animator.SetTrigger("finished");
        }
        else if (ob.attack_area.overlap<Player>(LayerMask.GetMask("Top Layer"))) {
            ob.look_at(ob.player.transform.position - ob.transform.position);
            animator.SetTrigger("attack");
        }
        else if (ob.attack_area.overlap<Envi>(LayerMask.GetMask("Middle Layer")) && Random.Range(0, 100) > 95) {
            ob.look_at(ob.attack_area.overlap<Envi>(LayerMask.GetMask("Middle Layer")).transform.position - ob.transform.position);
            animator.SetFloat("attack speed", 5.0f);
            animator.SetTrigger("attack");
        }
        else {
            ob.move_to_player();
        }
    }
}
