using UnityEngine;
using System.Collections;

public class PlayerKick : BasicFSM<Player> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ============================================== */
        ob.GetComponent<Collider>().enabled = false;
       
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
        /* ============================================== */
        ob.GetComponent<Collider>().enabled = true;
    }
}
