using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[SelectionBase]
public class Envi : MonoBehaviour {

    public GameObject particle;

    bool is_active;

    void Awake() {
        LevelManager.restart_event.AddListener(restart);
        LevelManager.control_point_event.AddListener(control_point);
        /* ================================================== */
        is_active = gameObject.activeSelf;
    }


    public void hit() {
        GameObject.Instantiate(particle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public void knock(GameObject player) {
        if (GetComponent<PlayMakerFSM>()) {
            GetComponent<PlayMakerFSM>().FsmVariables.GetVariable("player").RawValue = player;
            GetComponent<PlayMakerFSM>().SendEvent("KNOCK");
        }
    }

    public void restart() {
        gameObject.SetActive(is_active);
    }

    public void control_point() {
        is_active = gameObject.activeSelf;
    }

}
