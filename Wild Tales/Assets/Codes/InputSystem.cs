using UnityEngine;
using System.Collections;

public class InputSystem : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1"))
            GetComponent<PlayMakerFSM>().SendEvent("FIRE1");
        if (Input.GetButtonDown("Fire2"))
            GetComponent<PlayMakerFSM>().SendEvent("FIRE2");
        if (Input.GetButtonDown("Fire3"))
            GetComponent<PlayMakerFSM>().SendEvent("FIRE3");
    }
}
