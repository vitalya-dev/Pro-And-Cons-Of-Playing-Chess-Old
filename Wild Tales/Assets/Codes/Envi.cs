using UnityEngine;
using System.Collections;

public class Envi : MonoBehaviour {

    public GameObject particle;

    public void hit(Vector2 direction) {
        GameObject.Instantiate(particle, transform.position, Quaternion.identity);
        GameObject.Destroy(this.gameObject);
    }

    public void knock(GameObject knocker) {
        if (GetComponent<PlayMakerFSM>()) {
            GetComponent<PlayMakerFSM>().FsmVariables.GetVariable("player").RawValue = knocker;
            GetComponent<PlayMakerFSM>().SendEvent("KNOCK");
        }
    }
}
