using UnityEngine;
using System.Collections;

public class Envi : MonoBehaviour {

    public GameObject particle;


    void Awake() {
        LevelManager.restart_event.AddListener(restart);
    }

    public void hit(Vector2 direction) {
        GameObject.Instantiate(particle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public void knock(GameObject knocker) {
        if (GetComponent<PlayMakerFSM>()) {
            GetComponent<PlayMakerFSM>().FsmVariables.GetVariable("player").RawValue = knocker;
            GetComponent<PlayMakerFSM>().SendEvent("KNOCK");
        }
    }

    public void restart() {
        gameObject.SetActive(true);
    }
}
