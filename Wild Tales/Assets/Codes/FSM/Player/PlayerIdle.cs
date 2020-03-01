using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : BasicFSM<Player> {
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        /* ============================================== */
        Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /* ============================================================================= */
        Crosshair crosshair = GameObject.FindObjectOfType<Crosshair>();
        crosshair.transform.position = new Vector3(mouse_position.x, mouse_position.y, crosshair.transform.position.z);
        /* =============================================================================*/
        ob.transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector2)(ob.transform.position - crosshair.transform.position));
        /* ============================================== */
    }
}
