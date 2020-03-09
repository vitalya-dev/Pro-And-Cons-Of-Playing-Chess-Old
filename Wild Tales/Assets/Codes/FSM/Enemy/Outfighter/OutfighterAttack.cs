using UnityEngine;
using System.Collections;

public class OutfighterAttack : BasicFSM<Outfighter> {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ============================================== */
        ob.GetComponent<Collider2D>().enabled = false;
        ob.transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector2)(-1 * ((Vector2)ob.player.transform.position - rb.position).normalized));
        /* ============================================== */
        GameObject throwing = Instantiate(ob.throwing, new Vector3(rb.position.x, rb.position.y, ob.throwing.transform.position.z), Quaternion.identity);
        throwing.GetComponent<Throwing>().throwing(-1 * ob.transform.up);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
        /* ============================================== */
        ob.GetComponent<Collider2D>().enabled = true;
    }
}