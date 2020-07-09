using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace shared {
  [SelectionBase]
  public class Cupboard : MonoBehaviour {
    public void open() {
      GameObject.Find("Cupboard Open").GetComponent<AudioSource>().Play();
    }
  }
}
