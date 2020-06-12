using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Bath : MonoBehaviour {
  public void fill_it() {
    transform.Find("Bubble").GetComponent<SpriteRenderer>().enabled = true;
  }

  public void drain_it() {
    transform.Find("Bubble").GetComponent<SpriteRenderer>().enabled = false;
  }

}
