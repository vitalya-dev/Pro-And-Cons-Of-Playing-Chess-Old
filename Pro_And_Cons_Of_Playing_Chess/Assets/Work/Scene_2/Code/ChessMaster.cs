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
        Invoke("think_1", 2);
        state = "THINK";
      }
    }

    (Vector2Int, Vector2Int, float) bm;
    void think_1() {
      bm = chess.best_move(-1, 2);
      /* ========= */
      chess.highlight(new Vector2Int[]{bm.Item1});
      /* ========= */
      GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
      /* ========= */
      Invoke("think_2", 2);
    }

    void think_2() {
      chess.highlight(new Vector2Int[]{bm.Item1, bm.Item2});
      /* ========= */
      GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
      /* ========= */
      Invoke("think_3", 2);
    }

    void think_3() {
      chess.move(bm.Item1, bm.Item2, -1);
      /* ========= */
      GameObject.Find("Yeah").GetComponent<AudioSource>().Play();
      /* ========= */
      Invoke("think_4", 1);
    }

    void think_4() {
      chess.highlight(new Vector2Int[]{});
      /* ========= */
      state = "WAIT";
    }
  }
}
