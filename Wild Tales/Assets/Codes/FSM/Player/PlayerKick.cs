﻿using UnityEngine;
using System.Collections;

public class PlayerKick : BasicFSM<Player> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        ob.GetComponent<Collider2D>().enabled = true;
    }
}
