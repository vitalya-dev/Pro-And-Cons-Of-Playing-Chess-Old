using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_1 {
  public class Phone : shared.Phone {
    void gas() {
      if (GameObject.Find("Gas").GetComponent<SpriteRenderer>().enabled) {
        GameObject.Find("Gas").GetComponent<SpriteRenderer>().enabled = false;
      } else {
        GameObject.Find("Gas Spray").GetComponent<AudioSource>().Play();
        GameObject.Find("Gas").GetComponent<SpriteRenderer>().enabled = true;
      }
    }
  }
}

