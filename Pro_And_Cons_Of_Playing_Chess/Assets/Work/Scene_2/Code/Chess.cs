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
    public int turn = 1;

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

    public bool move(Vector2 from, Vector2 to, int who) {
      int x1 = (int) from.x; int y1 = (int) from.y;
      int x2 = (int) to.x;   int y2 = (int) to.y;
      /* ========= */
      if (turn != who || Mathf.Sign(board[y1, x1]) != who)
        return false;
      /* ========= */
      if (board[y1, x1] == -1 && black_pawn_move(from, to)) {
        turn *= -1;
        return true;
      }
      if (board[y1, x1] == 1 && white_pawn_move(from, to)) {
        turn *= -1;
        return true;
      }
      /* ========= */
      return false;
    }


    /* ===================================================== */
    bool black_pawn_move(Vector2 from, Vector2 to) {
      int x1 = (int) from.x; int y1 = (int) from.y;
      int x2 = (int) to.x;   int y2 = (int) to.y;
      /* ========= */
      if ((y1 - y2) == -1 && (x1 - x2) == 0) {
        board[y2, x2] = board[y1, x1];
        board[y1, x1] = 0;
        /* ========= */
        return true;
      } else {
        return false;
      }
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

    void redraw_pieces() {
      GameObject board_object = GameObject.Find("Chess Board");
      for (int i = 7; i >= 0; i--) {
        for (int j = 0; j < 8; j++) {
          if (board_object.transform.Find(i + "_" + j)) Destroy(board_object.transform.Find(i + "_" + j).gameObject);
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
            Vector2 s = new Vector2(piece_object.GetComponent<Image>().preferredWidth, piece_object.GetComponent<Image>().preferredHeight);
            piece_object.GetComponent<RectTransform>().sizeDelta = s * 7;
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
    }

    void redraw_board() {
      if (GameObject.Find("Chess Board")) Destroy(GameObject.Find("Chess Board"));
      /* =============*/
      GameObject board_object= Instantiate(Resources.Load("Etc/UIImage", typeof(GameObject))) as GameObject;
      /* =============*/
      board_object.name = "Chess Board";
      board_object.transform.SetParent(GameObject.Find("Canvas").transform);
      /* =============*/
      board_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_26_copy");
      /* =============*/
      Vector2 s = new Vector2(board_object.GetComponent<Image>().preferredWidth, board_object.GetComponent<Image>().preferredHeight);
      board_object.GetComponent<RectTransform>().sizeDelta = s * 7;
      /* =============*/
      board_object.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0.5f));
      board_object.GetComponent<RectTransform>().localPosition -= (Vector3)board_object.GetComponent<RectTransform>().sizeDelta * 0.5f;
      board_object.GetComponent<RectTransform>().anchorMin = Vector2.zero;
      board_object.GetComponent<RectTransform>().anchorMax = Vector2.zero;
      board_object.GetComponent<RectTransform>().pivot = Vector2.zero;
    }

    void redraw_selectors_1(Vector2 pos) {
      GameObject selector_1;
      if (!GameObject.Find("Selector 1")) {
        selector_1 = Instantiate(Resources.Load("Etc/UIImage", typeof(GameObject))) as GameObject;
        selector_1.name = "Selector 1";
        selector_1.transform.SetParent(GameObject.Find("Chess Board").transform);
        selector_1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_78");
      } else {
        selector_1 = GameObject.Find("Selector 1");
      }
      /* ========= */
      selector_1.GetComponent<Image>().enabled = true;
      /* ========= */
      selector_1.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
      selector_1.GetComponent<RectTransform>().localPosition = Vector3.zero;
      selector_1.GetComponent<RectTransform>().localPosition += new Vector3(7, 7, 0);
      selector_1.GetComponent<RectTransform>().localPosition += new Vector3(pos.x * 56, 0, 0);
      selector_1.GetComponent<RectTransform>().localPosition += new Vector3(0, pos.y * 56, 0);
      /* ========= */
      Vector2 s = new Vector2(selector_1.GetComponent<Image>().preferredWidth, selector_1.GetComponent<Image>().preferredHeight);
      selector_1.GetComponent<RectTransform>().sizeDelta = s * 7;
      /* ===================================================== */
      GameObject selector_2;
      if (!GameObject.Find("Selector 2")) {
        selector_2 = Instantiate(selector_1, selector_1.transform.parent);
        selector_2.name = "Selector 2";
      } else {
        selector_2 = GameObject.Find("Selector 2");
      }
      /* ========= */
      selector_2.GetComponent<Image>().enabled = false;
    }

    void redraw_selectors_2(Vector2 pos) {

    }


    void play_chess_1() {
      redraw_board();
      redraw_pieces();
      redraw_selectors_1(Vector2.zero);
    }


    void play_chess_2() {
      redraw_pieces();
      /* ========= */
      Vector2 pos = Input.mousePosition - GameObject.Find("Chess Board").GetComponent<RectTransform>().position;
      pos -=  new Vector2(7, 7);
      pos /=  new Vector2(56, 56);
      pos.x = (int)pos.x;
      pos.y = (int)pos.y;
      /* ========= */
      redraw_selectors_1(pos);
      /* ===================================================== */
      if (pos.x >= 0 && pos.x < 8 && pos.y >= 0 && pos.y < 8) {
        GameObject selector_1 = GameObject.Find("Selector 1");
        GameObject selector_2 = GameObject.Find("Selector 2");
        /* ===================================================== */
        if (Input.GetMouseButtonDown(1)) {
          if (clicked_1 != new Vector2(-1, -1)) {
            selector_1.name = "Selector 2";
            selector_2.name = "Selector 1";
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
          } else if (clicked_2 == new Vector2(-1, -1)) {
            clicked_2 = new Vector2(pos.x, 7 - pos.y);
            /* ===================================================== */
            move(clicked_1, clicked_2, 1);
            /* ===================================================== */
            selector_1.name = "Selector 2";
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

