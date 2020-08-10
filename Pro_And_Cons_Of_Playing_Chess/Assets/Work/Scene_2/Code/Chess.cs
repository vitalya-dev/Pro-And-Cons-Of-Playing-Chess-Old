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
      // 0      1      2      3      4      5      6      7 //
      {+0000, +0000, +0000, +0000, +0000, -0006, -1000, +0000}, //0
      {+0000, +0000, +0000, +0000, +0000, -0001, +0005, +0000}, //1
      {+0000, +0000, +0000, +0000, +0000, +0000, +0000, +0000}, //2
      {+0000, +0000, +0000, +0000, +0000, +0000, +0000, +0000}, //3
      {+0000, -0099, +0000, +0000, +0000, +0000, +0006, +0000}, //4
      {+0000, +0000, +0000, +0000, +0000, +0000, +0000, +0000}, //5
      {+0000, +0000, +0000, +0000, +0000, +0000, +0000, +0000}, //6
      {+0000, +0005, +0000, +0000, +0000, +0000, +0000, +0000}, //7
    };

    public bool move(Vector2Int from, Vector2Int to, int who) {
      if (from.x > 7 || from.x < 0 || from.y > 7 || from.y < 0 || to.x > 7 || to.x < 0 || to.y > 7 || to.y < 0)
        return false;
      /* ========= */
      if (turn != who || Mathf.Sign(board[from.y, from.x]) != who)
        return false;
      /* ========= */
      if (board[from.y, from.x] == -1 && black_pawn_move(from, to)) {
        turn *= -1;
        return true;
      }
      if (board[from.y, from.x] == 1 && white_pawn_move(from, to)) {
        turn *= -1;
        return true;
      }
      if (board[from.y, from.x] == 5 && white_bishop_move(from, to)) {
        turn *= -1;
        return true;
      }
      /* ========= */
      return false;
    }


    /* ===================================================== */
    bool black_pawn_move(Vector2Int from, Vector2Int to) {
      {var s = to + "{"; foreach (var m in pawn_moves(from, -1)) s += m; Debug.Log(s + "}");} // DEBUG
      foreach (var m in pawn_moves(from, -1)) {
        if (to == m)  {
          /* ========= */
          board[to.y, to.x] = board[from.y, from.x];
          board[from.y, from.x] = 0;
          /* ========= */
          return true;
        }
      }
      return false;
    }


    bool white_pawn_move(Vector2Int from, Vector2Int to) {
      {var s = to + "{"; foreach (var m in pawn_moves(from, 1)) s += m; Debug.Log(s + "}");} // DEBUG
      foreach (var m in pawn_moves(from, 11)) {
        if (to == m)  {
          /* ========= */
          board[to.y, to.x] = board[from.y, from.x];
          board[from.y, from.x] = 0;
          /* ========= */
          return true;
        }
      }
      return false;
    }

    bool white_bishop_move(Vector2Int from, Vector2Int to) {
      {var s = to + "{"; foreach (var m in bishop_moves(from, 1)) s += m; Debug.Log(s + "}");} // DEBUG
      foreach (var m in bishop_moves(from, 1)) {
        if (to == m)  {
          /* ========= */
          board[to.y, to.x] = board[from.y, from.x];
          board[from.y, from.x] = 0;
          /* ========= */
          return true;
        }
      }
      return false;
    }

    Vector2[] bishop_moves(Vector2Int pos, int who) {
      int X = 0;
      int Y = 1;
      /* ========= */
      List<Vector2> moves = new List<Vector2>();
      /* ========= */
      foreach (var d in new[] {new int[]{1, 1}, new int[]{1, -1}, new int[]{-1, 1}, new int[]{-1, -1}}) {
        for (int i = 1; i < 8; i++) {
          if (pos.y + d[Y] * i > 7 || pos.y + d[Y] * i < 0 || pos.x + d[X] * i > 7 || pos.x + d[X] * i < 0) break;
          /* ========= */
          int p = board[pos.y + d[Y] * i, pos.x + d[X] * i];
          if (p == 0) {
            moves.Add(new Vector2(pos.x + d[X] * i,  pos.y + d[Y] * i));
          } else if (Mathf.Sign(p) != who) {
            moves.Add(new Vector2(pos.x + d[X] * i,  pos.y + d[Y] * i));
            break;
          } else {
            break;
          }
        }
      }
      return moves.ToArray();
    }

    Vector2[] pawn_moves(Vector2Int pos, int who) {
      List<Vector2> moves = new List<Vector2>();
      if (who == 1) {
        if (pos.y > 0 && board[pos.y - 1, pos.x] == 0)
          moves.Add(new Vector2(pos.x, pos.y - 1));
        if (pos.y > 0 && pos.x > 0 && board[pos.y - 1, pos.x - 1] != 0 && Mathf.Sign(board[pos.y - 1, pos.x - 1]) != who)
          moves.Add(new Vector2(pos.x - 1, pos.y - 1));
        if (pos.y > 0 && pos.x < 7 && board[pos.y - 1, pos.x + 1] != 0 && Mathf.Sign(board[pos.y - 1, pos.x + 1]) != who)
          moves.Add(new Vector2(pos.x + 1, pos.y - 1));
      }
      else if (who == -1)  {
        if (pos.y < 7 && board[pos.y + 1, pos.x] == 0)
          moves.Add(new Vector2(pos.x, pos.y + 1));
        if (pos.y < 7 && pos.x < 7 && board[pos.y + 1, pos.x + 1] != 0 && Mathf.Sign(board[pos.y + 1, pos.x + 1]) != who)
          moves.Add(new Vector2(pos.x + 1, pos.y + 1));
        if (pos.y < 7 && pos.x > 0 && board[pos.y + 1, pos.x - 1] != 0 && Mathf.Sign(board[pos.y + 1, pos.x - 1]) != who)
          moves.Add(new Vector2(pos.x - 1, pos.y + 1));
      }
      return moves.ToArray();
    }



    /* ===================================================== */

    Vector2Int clicked_1 = new Vector2Int(-1, -1);
    Vector2Int clicked_2 = new Vector2Int(-1, -1);

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
            if (board[i,j] == 5)
              piece_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_77_copy");
            if (board[i,j] == 6)
              piece_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_76_copy_2");
            if (board[i,j] == -1)
              piece_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_77_copy_27");
            if (board[i,j] == -6)
              piece_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_76_copy_3");
            if (board[i,j] == -99)
              piece_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_78_copy");
            if (board[i,j] == -1000)
              piece_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_76_copy_4");
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
      redraw_selectors_1(pos);
      /* ========= */
      GameObject.Find("Selector 1").GetComponent<Image>().enabled = true;
      GameObject.Find("Selector 2").GetComponent<Image>().enabled = true;
    }


    void play_chess_1() {
      redraw_board();
      redraw_pieces();
      redraw_selectors_1(Vector2.zero);
      /* ========= */
      GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
    }


    void play_chess_2() {
      redraw_pieces();
      /* ========= */
      Vector2 pos_0 = Input.mousePosition - GameObject.Find("Chess Board").GetComponent<RectTransform>().position;
      pos_0 -=  new Vector2(7, 7);
      pos_0 /=  new Vector2(56, 56);
      /* ========= */
      Vector2Int pos_1 = new Vector2Int((int)pos_0.x,  (int)pos_0.y);
      /* ========= */
      if (pos_1.x >= 0 && pos_1.x < 8 && pos_1.y >= 0 && pos_1.y < 8) {
        GameObject selector_1 = GameObject.Find("Selector 1");
        GameObject selector_2 = GameObject.Find("Selector 2");
        if (Input.GetMouseButtonDown(1)) {
          GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
          if (clicked_1 != new Vector2Int(-1, -1)) {
            selector_1.name = "Selector 2";
            selector_2.name = "Selector 1";
            /* ========= */
            GameObject.Find("No 2").GetComponent<AudioSource>().Play();
          }
          clicked_1 = new Vector2Int(-1, -1);
          clicked_2 = new Vector2Int(-1, -1);
        } else if (Input.GetMouseButtonDown(0)) {
          if (clicked_1 == new Vector2(-1, -1)) {
            clicked_1 = new Vector2Int(pos_1.x, 7 - pos_1.y);
            /* ========= */
            selector_1.name = "Selector 2";
            selector_2.name = "Selector 1";
            /* ========= */
            GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
          } else if (clicked_2 == new Vector2(-1, -1)) {
            clicked_2 = new Vector2Int(pos_1.x, 7 - pos_1.y);
            if (move(clicked_1, clicked_2, 1))
              GameObject.Find("Yeah").GetComponent<AudioSource>().Play();
            else
              GameObject.Find("No").GetComponent<AudioSource>().Play();
            /* ========= */
            selector_1.name = "Selector 2";
            selector_2.name = "Selector 1";
            /* ========= */
            clicked_1 = new Vector2Int(-1, -1);
            clicked_2 = new Vector2Int(-1, -1);
          }
        }
        if (clicked_1 == new Vector2(-1, -1))
          redraw_selectors_1(pos_1);
        else
          redraw_selectors_2(pos_1);
      }
    }

    void play_chess_3() {
      Destroy(GameObject.Find("Chess Board"));
    }
    /* ===================================================== */
  }
}

