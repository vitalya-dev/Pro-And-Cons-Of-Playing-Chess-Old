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
      Debug.Log(score());
      for (int i = 0; i < 8; i++) {
        for (int j = 0; j < 8; j++) {
          if (chess.board[i,j] == -1) {
            chess.move(new Vector2Int(j, i), new Vector2Int(j, i + 1), -1);
          }
        }
      }
      state = "WAIT";
    }

    int score() {
      int s = 0;
      for (int i = 0; i < 8; i++)
        for (int j = 0; j < 8; j++)
          s += chess.board[i, j];
      return s;
    }
  }
}
