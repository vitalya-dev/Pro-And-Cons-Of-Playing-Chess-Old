using UnityEngine;
using System.Collections;

public class PlayerFire : BasicFSM<Player> {
    [SerializeField]
    GameObject molotov_prefab;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /* ================================== */
        Vector2 molot_position = ob.transform.position + (-2 * ob.transform.up);
        GameObject molotov =  GameObject.Instantiate(molotov_prefab, new Vector3(molot_position.x, molot_position.y, molotov_prefab.transform.position.z), Quaternion.identity);
        /* ================================== */
        molotov.GetComponent<Projectile>().fire(-1 * ob.transform.up);
    }
}
