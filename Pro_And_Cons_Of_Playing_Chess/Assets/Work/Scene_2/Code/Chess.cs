using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace scene_2 {
  [SelectionBase]
  public class Chess : MonoBehaviour {
    /* ===================================================== */
    int turn = 1;

    public int[,] board = new int [,] {
      {0, 0, 0, 0, 0, 0, 0, 0}, 
      {0, 0, 0, 0, 0, -1, 0, 0},
      {0, 0, 0, 0, 0, 0, 0, 0}, 
      {0, 0, 0, 0, 0, 0, 0, 0}, 
      {0, 0, 0, 0, 0, 0, 0, 0}, 
      {0, 0, 0, 0, 0, 0, 0, 0}, 
      {0, 0, 0, 0, 0, 0, 1, 0}, 
      {1, 0, 0, 0, 0, 0, 0, 0}, 
    };

    public bool white_move(Vector2 from, Vector2 to) {
      int x1 = (int) from.x; int y1 = (int) from.y;
      int x2 = (int) to.x;   int y2 = (int) to.y;
      /* ========= */
      if (board[y1, x1] <= 0 || turn != 1)
        return false;
      if (board[y1, x1] == 1 && white_pawn_move(from, to)) {
        turn = -1;
        return true;
      }
      else
        return false;
    }

    bool white_pawn_move(Vector2 from, Vector2 to) {
      int x1 = (int) from.x; int y1 = (int) from.y;
      int x2 = (int) to.x;   int y2 = (int) to.y;
      /* ========= */
      if ((y1 - y2) == 1 && (x1 - x2) == 0) {
        board[y2, x2] = board[y1, x1];
        board[y1, x1] = 0;
        /* ========= */
        return true;
      } else {
        return false;
      }
    }
    /* ===================================================== */

    Vector2 clicked_1 = new Vector2(-1, -1);
    Vector2 clicked_2 = new Vector2(-1, -1);

    void play_chess_1() {
      GameObject board_object= Instantiate(Resources.Load("Etc/UIImage", typeof(GameObject))) as GameObject;
      /* ===================================================== */
      board_object.name = "Chess Board";
      board_object.transform.SetParent(GameObject.Find("Canvas").transform);
      /* ===================================================== */
      board_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_26_copy");
      /* ===================================================== */
      Vector2 s0 = new Vector2(board_object.GetComponent<Image>().preferredWidth, board_object.GetComponent<Image>().preferredHeight);
      board_object.GetComponent<RectTransform>().sizeDelta = s0 * 7;
      /* ===================================================== */
      board_object.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0.5f));
      board_object.GetComponent<RectTransform>().localPosition -= (Vector3)board_object.GetComponent<RectTransform>().sizeDelta * 0.5f;
      board_object.GetComponent<RectTransform>().anchorMin = Vector2.zero;
      board_object.GetComponent<RectTransform>().anchorMax = Vector2.zero;
      board_object.GetComponent<RectTransform>().pivot = Vector2.zero;
      /* ===================================================== */
      for (int i = 7; i >= 0; i--) {
        for (int j = 0; j < 8; j++) {
          if (board[i,j] != 0) {
            GameObject piece_object = Instantiate(Resources.Load("Etc/UIImage", typeof(GameObject))) as GameObject;
            /* ===================================================== */
            piece_object.name = i + "_" + j;
            piece_object.transform.SetParent(GameObject.Find("Chess Board").transform);
            /* ===================================================== */
            if (board[i,j] == 1)
              piece_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_77_copy_26");
            if (board[i,j] == -1)
              piece_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_77_copy_27");
            /* ===================================================== */
            Vector2 s1 = new Vector2(piece_object.GetComponent<Image>().preferredWidth, piece_object.GetComponent<Image>().preferredHeight);
            piece_object.GetComponent<RectTransform>().sizeDelta = s1 * 7;
            /* ===================================================== */
            piece_object.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            piece_object.GetComponent<RectTransform>().anchorMax = Vector2.zero;
            piece_object.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            /* ===================================================== */
            piece_object.GetComponent<RectTransform>().localPosition = Vector3.zero;
            piece_object.GetComponent<RectTransform>().localPosition += new Vector3(7, 7, 0);
            piece_object.GetComponent<RectTransform>().localPosition += new Vector3(j * 56 + 56 / 2, 0, 0);
            piece_object.GetComponent<RectTransform>().localPosition += new Vector3(0, (7 - i) * 56 + 56 / 2, 0);
            /* ===================================================== */
          }
        }
      }
      /* ===================================================== */
      GameObject selector_1 = Instantiate(Resources.Load("Etc/UIImage", typeof(GameObject))) as GameObject;
      selector_1.name = "Selector 1";
      selector_1.transform.SetParent(GameObject.Find("Chess Board").transform);
      selector_1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_78");
      /* ========= */
      selector_1.GetComponent<RectTransform>().localPosition = Vector3.zero;
      selector_1.GetComponent<RectTransform>().anchorMin = Vector2.zero;
      selector_1.GetComponent<RectTransform>().anchorMax = Vector2.zero;
      selector_1.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
      /* ========= */
      Vector2 s2 = new Vector2(selector_1.GetComponent<Image>().preferredWidth, selector_1.GetComponent<Image>().preferredHeight);
      selector_1.GetComponent<RectTransform>().sizeDelta = s2 * 7;
      /* ===================================================== */
      GameObject selector_2 = Instantiate(selector_1, selector_1.transform.parent);
      selector_2.name = "Selector 2";
      selector_2.GetComponent<Image>().enabled = false;
      /* ===================================================== */
      GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
    }


    void play_chess_2() {
      Vector2 pos = Input.mousePosition - GameObject.Find("Chess Board").GetComponent<RectTransform>().position;
      /* ===================================================== */
      pos -=  new Vector2(7, 7);
      pos /=  new Vector2(56, 56);
      /* ===================================================== */
      pos.x = (int)pos.x;
      pos.y = (int)pos.y;
      /* ===================================================== */
      if (pos.x >= 0 && pos.x < 8 && pos.y >= 0 && pos.y < 8) {
        GameObject selector_1 = GameObject.Find("Selector 1");
        GameObject selector_2 = GameObject.Find("Selector 2");
        /* ===================================================== */
        selector_1.GetComponent<RectTransform>().localPosition = Vector3.zero;
        selector_1.GetComponent<RectTransform>().localPosition += new Vector3(7, 7, 0);
        selector_1.GetComponent<RectTransform>().localPosition += new Vector3(pos.x * 56, 0, 0);
        selector_1.GetComponent<RectTransform>().localPosition += new Vector3(0, pos.y * 56, 0);
        /* ===================================================== */
        if (Input.GetMouseButtonDown(1)) {
          GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
          /* ===================================================== */
          if (clicked_1 != new Vector2(-1, -1)) {
            selector_1.name = "Selector 2";
            selector_1.GetComponent<Image>().enabled = false;
            selector_2.name = "Selector 1";
            /* ========== */
            GameObject.Find("No 2").GetComponent<AudioSource>().Play();
          }
          clicked_1 = new Vector2(-1, -1);
          clicked_2 = new Vector2(-1, -1);
          /* ===================================================== */
        } else if (Input.GetMouseButtonDown(0)) {
          if (clicked_1 == new Vector2(-1, -1)) {
            clicked_1 = new Vector2(pos.x, 7 - pos.y);
            /* ===================================================== */
            selector_1.name = "Selector 2";
            selector_2.name = "Selector 1";
            selector_2.GetComponent<Image>().enabled = true;
            /* ===================================================== */
            GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
          } else if (clicked_2 == new Vector2(-1, -1)) {
            clicked_2 = new Vector2(pos.x, 7 - pos.y);
            /* ===================================================== */
            if (white_move(clicked_1, clicked_2)) {
              GameObject piece = GameObject.Find(clicked_1.y + "_" + clicked_1.x);
              piece.GetComponent<RectTransform>().localPosition = Vector3.zero;
              piece.GetComponent<RectTransform>().localPosition += new Vector3(7, 7, 0);
              piece.GetComponent<RectTransform>().localPosition += new Vector3(clicked_2.x * 56 + 56 / 2, 0, 0);
              piece.GetComponent<RectTransform>().localPosition += new Vector3(0, (7 - clicked_2.y) * 56 + 56 / 2, 0);
              /* ======= */
              piece.name = clicked_2.y + "_" + clicked_2.x;
              /* ======= */
              GameObject.Find("Yeah").GetComponent<AudioSource>().Play();
            } else {
              /* ===================================================== */
              GameObject.Find("No").GetComponent<AudioSource>().Play();
            }
            /* ===================================================== */
            selector_1.name = "Selector 2";
            selector_1.GetComponent<Image>().enabled = false;
            selector_2.name = "Selector 1";
            /* ===================================================== */
            clicked_1 = new Vector2(-1, -1);
            clicked_2 = new Vector2(-1, -1);
          }
        }
      }
    }

    void play_chess_3() {
      Destroy(GameObject.Find("Chess Board"));
    }
    /* ===================================================== */
  }
}

