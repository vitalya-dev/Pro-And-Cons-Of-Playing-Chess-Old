using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace shared {
  [SelectionBase]
  public class SceneLight : MonoBehaviour {
    public void turn_on() {
      GameObject.Find("Light Switch").GetComponent<AudioSource>().Play();
      /* ===================================================== */
      GetComponent<Light>().enabled = true;
    }

    public void turn_off() {
      GameObject.Find("Light Switch").GetComponent<AudioSource>().Play();
      /* ===================================================== */
      GetComponent<Light>().enabled = false;
    }
  }
}
