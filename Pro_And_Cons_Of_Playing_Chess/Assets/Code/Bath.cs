using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Bath : MonoBehaviour {
  public void fill() {
    GameObject.Find("Water Flowing").GetComponent<AudioSource>().Play();
    /* ===================================================== */
    transform.Find("Bubble").GetComponent<SpriteRenderer>().enabled = true;
  }

  public void drain() {
    GameObject.Find("Water Bubble").GetComponent<AudioSource>().Play();
    /* ===================================================== */
    transform.Find("Bubble").GetComponent<SpriteRenderer>().enabled = false;
  }

}
