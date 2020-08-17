using System;
using System.Linq;
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

    string state = "WAIT";
    void Update() {
      if (state == "WAIT" && chess.turn == -1) {
        Invoke("think", 2);
        state = "THINK";
      }
    }

    void think() {
      var bm = chess.best_move(-1, 2);
      chess.move(bm.Item1, bm.Item2, -1);
      /* ========= */
      GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
      GameObject.Find("Yeah").GetComponent<AudioSource>().Play();
      /* ========= */
      state = "WAIT";
    }
  }
}
