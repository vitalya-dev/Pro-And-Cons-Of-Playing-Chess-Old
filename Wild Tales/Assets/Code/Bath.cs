using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Bath : MonoBehaviour {
  public void fill() {
    transform.Find("Bubble").GetComponent<SpriteRenderer>().enabled = true;
  }

  public void drain() {
    transform.Find("Bubble").GetComponent<SpriteRenderer>().enabled = false;
  }

}
