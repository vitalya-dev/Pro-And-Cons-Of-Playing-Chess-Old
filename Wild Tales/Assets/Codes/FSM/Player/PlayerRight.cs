using UnityEngine;
using System.Collections;

public class PlayerRight : BasicFSM<Player> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        rb.MovePosition(rb.position + (Vector2)rb.transform.up * -1);
    }
}
