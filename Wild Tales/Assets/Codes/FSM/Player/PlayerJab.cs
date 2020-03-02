using UnityEngine;
using System.Collections;

public class PlayerJab : BasicFSM<Player> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (ob.attack_area.overlap<Envi>(LayerMask.NameToLayer("Top Level"))) {
            ob.attack_area.overlap<Envi>(LayerMask.NameToLayer("Top Level")).hit(-1 * ob.transform.up);
        }
        if (ob.attack_area.overlap<Envi>(LayerMask.NameToLayer("Middle Level"))) {
            ob.attack_area.overlap<Envi>(LayerMask.NameToLayer("Middle Level")).hit(-1 * ob.transform.up);
        }
        rb.MovePosition(rb.position + (Vector2)rb.transform.up * -1);
    }
}
