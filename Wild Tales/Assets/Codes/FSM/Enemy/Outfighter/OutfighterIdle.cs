using UnityEngine;
using System.Collections;

public class OutfighterIdle : BasicFSM<Outfighter> {
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        /* ============================================== */
        if (ob.eye.see<Player>(LayerMask.GetMask("Top Layer")))
            ob.player = ob.eye.see<Player>(LayerMask.GetMask("Top Layer"));
        /* ============================================== */
        if (ob.player) {
            foreach (var enemy in ob.eye.see_all<Enemy>(LayerMask.GetMask("Top Layer"))) {
                if (!enemy.player)
                    enemy.player = ob.player;
            }
            ob.GetComponent<Animator>().SetTrigger("chase");
        }
    }
}
