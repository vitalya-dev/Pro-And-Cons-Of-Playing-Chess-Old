using UnityEngine;
using System.Collections;


public class PlayerWalk : BasicBehaviour<Player> {
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
        rb.MovePosition(rb.position + ob.movement * ob.speed * Time.fixedDeltaTime);
    }
}
