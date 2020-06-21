using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
  public Vector3 opend = Vector3.right;

  public virtual void open() {
    transform.rotation *= Quaternion.FromToRotation(Vector3.forward, opend);
    opend *= -1;
    /* ===================================================== */
    GameObject.Find("Door Open").GetComponent<AudioSource>().Play();
  }
}
