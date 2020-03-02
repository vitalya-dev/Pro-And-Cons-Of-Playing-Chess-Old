using UnityEngine;
using System.Collections;

public class EnemyIdle : BasicFSM<Enemy> {
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        /* ============================================== */
        if (ob.eye.see<Player>(LayerMask.GetMask("Top Layer"))) {
            ob.player = ob.eye.see<Player>(LayerMask.NameToLayer("Top Layer"));
            ob.GetComponent<Animator>().SetTrigger("chase");
        }
    }
}
