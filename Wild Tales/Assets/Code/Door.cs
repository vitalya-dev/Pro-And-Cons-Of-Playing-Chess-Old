using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
  public Vector3 opend = Vector3.right;

  /* ===================================================== */
  private bool is_open = false;
  public virtual void open() {
    transform.rotation *= Quaternion.FromToRotation(Vector3.forward, opend);
    opend *= -1;
    /* ===================================================== */
    if (is_open) 
      GameObject.Find("Door Close").GetComponent<AudioSource>().Play();
    else
      GameObject.Find("Door Open").GetComponent<AudioSource>().Play();
    /* ===================================================== */
    is_open = !is_open;
  }
  /* ===================================================== */
}
