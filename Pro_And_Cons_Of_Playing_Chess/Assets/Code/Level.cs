using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;

public class Level : MonoBehaviour {
    void Awake() {
        GameObject gui = new GameObject("GUI");
        /* ============================================== */
        GameObject crosshair =
            Instantiate(Resources.Load("Etc/Crosshair", typeof(GameObject))) as GameObject;
        crosshair.transform.position = Vector3.up * (Camera.main.transform.position.y - 1);
        crosshair.transform.parent = gui.transform;
    }
}
