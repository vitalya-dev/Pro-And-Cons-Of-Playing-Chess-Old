using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_2 {
  [SelectionBase]
  public class ChessMaster : MonoBehaviour {
    void Start() {
      GetComponent<SpriteRenderer>().enabled = false;
    }
  }
}
