using UnityEngine;
using System.Collections;

public class PlayerFire : BasicFSM<Player> {
    [SerializeField]
    GameObject molotov_prefab;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ================================== */
    }
}
