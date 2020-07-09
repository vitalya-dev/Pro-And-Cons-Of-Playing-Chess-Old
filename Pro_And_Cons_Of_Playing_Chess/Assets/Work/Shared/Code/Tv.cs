using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace shared {
  [SelectionBase]
  public class Tv : MonoBehaviour {
    public void turn_on() {
      GameObject.Find("Tv Noise").GetComponent<AudioSource>().Play();
      /* ===================================================== */
      transform.Find("Screen").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void turn_off() {
      //GameObject.Find("Water Flowing").GetComponent<AudioSource>().Play();
      /* ===================================================== */
      transform.Find("Screen").GetComponent<SpriteRenderer>().enabled = false;
    }
  }
}
