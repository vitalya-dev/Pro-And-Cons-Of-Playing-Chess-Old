using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_2 {
  [SelectionBase]
  public class ChessMaster : MonoBehaviour {
    
    Chess chess;

    void Start() {
      GetComponent<SpriteRenderer>().enabled = false;
      chess = GameObject.FindObjectOfType<Chess>();
    }

    void Update() {
      if (chess.turn == -1)
        Debug.Log("My Turn");
    }
  }
}
