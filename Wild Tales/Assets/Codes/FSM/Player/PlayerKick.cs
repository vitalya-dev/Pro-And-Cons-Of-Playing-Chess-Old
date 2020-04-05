using UnityEngine;
using System.Collections;

public class PlayerKick : BasicFSM<Player> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (ob.attack_area.overlap<Projectile>()) {
            ob.attack_area.overlap<Envi>().hit();
            animator.SetFloat("attack speed", 2.0f);
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
    }
}
