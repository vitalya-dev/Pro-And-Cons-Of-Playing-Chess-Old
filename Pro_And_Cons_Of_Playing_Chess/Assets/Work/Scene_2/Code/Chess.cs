using System;
using System.Linq;
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
      {+0000, +0005, +0000, +0000, +0000, +0000, +0000, +1000}, //7
    };


    public bool move(Vector2Int from, Vector2Int to, int who) {
      if (game_over())
        return false;
      if (from.x > 7 || from.x < 0 || from.y > 7 || from.y < 0 || to.x > 7 || to.x < 0 || to.y > 7 || to.y < 0)
        return false;
      /* ========= */
      if (turn != who || Mathf.Sign(board[from.y, from.x]) != who)
        return false;
      /* ========= */
      foreach (var m in xxx_moves(from)) {
        if (to == m)  {
          board[to.y, to.x] = board[from.y, from.x];
          board[from.y, from.x] = 0;
          /* ========= */
          turn *= -1;
          /* ========= */
          redraw_pieces();
          /* ========= */
          return true;
        }
      }
      /* ========= */
      return false;
    }

    public bool game_over() {
      return (score(1) < 1000 || score(-1) > -1000) ? true :  false;
    }


    public int score(int who) {
      int s = 0;
      /* ========= */
      for (int i = 0; i < 8; i++)
        for (int j = 0; j < 8; j++)
          if (Mathf.Sign(board[i, j]) == who) s += board[i, j];
      /* ========= */
      return s;
    }


    public (Vector2Int, Vector2Int, float) best_move(int who, int think_ahead = 1) {
      if (think_ahead == 0)
        return (-Vector2Int.one, -Vector2Int.one, score(1) + score(-1));
      /* ========= */
      var bm = (-Vector2Int.one, -Vector2Int.one, Mathf.Infinity * who * -1);
      /* ========= */
      foreach (var move in player_moves(who)) {
        Vector2Int from = move.Item1;
        foreach (var to in move.Item2) {
          int a = board[from.y, from.x];
          int b = board[to.y, to.x];
          /* ========= */
          board[to.y, to.x] = a;
          board[from.y, from.x] = 0;
          /* ========= */
          float s = best_move(who * -1, think_ahead - 1).Item3;        
          /* ========= */
          switch (who) {
            case 1:
              if (s > bm.Item3) {
                bm.Item1 = from;
                bm.Item2 = to;
                bm.Item3 = s;
              }
              break;
            case -1:
              if (s < bm.Item3) {
                bm.Item1 = from;
                bm.Item2 = to;
                bm.Item3 = s;
              }
              break;
          }
          /* ========= */
          board[from.y, from.x] = a;
          board[to.y, to.x] = b;
        }
      }
      return bm;
    }


    /* ===================================================== */
    List<(Vector2Int, Vector2Int[])> player_moves(int who) {
      var moves = new List<(Vector2Int, Vector2Int[])>();
      /* ========= */
      for (int i = 0; i < 8; i++)
        for (int j = 0; j < 8; j++)
          if (Mathf.Sign(board[i, j]) == who)
            moves.Add((new Vector2Int(j, i), xxx_moves(new Vector2Int(j, i))));
      /* ========= */
      return moves;
    }


    Vector2Int[] xxx_moves(Vector2Int from) {
      if (from.x > 7 || from.x < 0 || from.y > 7 || from.y < 0) return new Vector2Int[]{};
      /* ========= */
      if (board[from.y, from.x] == 1) return pawn_moves(from, 1);
      if (board[from.y, from.x] == 5) return bishop_moves(from, 1);
      if (board[from.y, from.x] == 6) return rook_moves(from, 1);
      if (board[from.y, from.x] == 99) return queen_moves(from, 1);
      if (board[from.y, from.x] == 1000) return king_moves(from, 1);
      if (board[from.y, from.x] == -1) return pawn_moves(from, -1);
      if (board[from.y, from.x] == -5) return bishop_moves(from, -1);
      if (board[from.y, from.x] == -6) return rook_moves(from, -1);
      if (board[from.y, from.x] == -99) return queen_moves(from, -1);
      if (board[from.y, from.x] == -1000) return king_moves(from, -1);
      /* ========= */
      return new Vector2Int[]{};
    }


    Vector2Int[] king_moves(Vector2Int pos, int who) {
      List<Vector2Int> moves = new List<Vector2Int>();
      /* ========= */
      foreach (var d in new[] {new int[]{1, 1}, new int[]{1, -1}, new int[]{-1, 1}, new int[]{-1, -1}}) {
        if (pos.y + d[1] > 7 || pos.y + d[1] < 0 || pos.x + d[0] > 7 || pos.x + d[0] < 0) continue;
        /* ========= */
        int p = board[pos.y + d[1], pos.x + d[0]];
        if (p == 0 || Mathf.Sign(p) != who) moves.Add(new Vector2Int(pos.x + d[0],  pos.y + d[1]));
      }
      foreach (var d in new[] {new int[]{0, 1}, new int[]{0, -1}, new int[]{1, 0}, new int[]{-1, 0}}) {
        if (pos.y + d[1] > 7 || pos.y + d[1] < 0 || pos.x + d[0] > 7 || pos.x + d[0] < 0) continue;
        /* ========= */
        int p = board[pos.y + d[1], pos.x + d[0]];
        if (p == 0 || Mathf.Sign(p) != who) moves.Add(new Vector2Int(pos.x + d[0],  pos.y + d[1]));
      }
      /* ========= */
      return moves.ToArray();
    }


    Vector2Int[] bishop_moves(Vector2Int pos, int who) {
      List<Vector2Int> moves = new List<Vector2Int>();
      /* ========= */
      foreach (var d in new[] {new int[]{1, 1}, new int[]{1, -1}, new int[]{-1, 1}, new int[]{-1, -1}}) {
        for (int i = 1; i < 8; i++) {
          if (pos.y + d[1] * i > 7 || pos.y + d[1] * i < 0 || pos.x + d[0] * i > 7 || pos.x + d[0] * i < 0) break;
          /* ========= */
          int p = board[pos.y + d[1] * i, pos.x + d[0] * i];
          if (p == 0) moves.Add(new Vector2Int(pos.x + d[0] * i,  pos.y + d[1] * i));
          else if (Mathf.Sign(p) != who) { moves.Add(new Vector2Int(pos.x + d[0] * i,  pos.y + d[1] * i)); break; }
          else break;
        }
      }
      return moves.ToArray();
    }

    Vector2Int[] rook_moves(Vector2Int pos, int who) {
      List<Vector2Int> moves = new List<Vector2Int>();
      /* ========= */
      foreach (var d in new[] {new int[]{0, 1}, new int[]{0, -1}, new int[]{1, 0}, new int[]{-1, 0}}) {
        for (int i = 1; i < 8; i++) {
          if (pos.y + d[1] * i > 7 || pos.y + d[1] * i < 0 || pos.x + d[0] * i > 7 || pos.x + d[0] * i < 0) break;
          /* ========= */
          int p = board[pos.y + d[1] * i, pos.x + d[0] * i];
          if (p == 0) moves.Add(new Vector2Int(pos.x + d[0] * i,  pos.y + d[1] * i));
          else if (Mathf.Sign(p) != who) { moves.Add(new Vector2Int(pos.x + d[0] * i,  pos.y + d[1] * i)); break; }
          else break;
        }
      }
      return moves.ToArray();
    }

    Vector2Int[] queen_moves(Vector2Int pos, int who) {
      return bishop_moves(pos, who).Concat(rook_moves(pos, who)).ToArray();
    }

    Vector2Int[] pawn_moves(Vector2Int pos, int who) {
      List<Vector2Int> moves = new List<Vector2Int>();
      if (who == 1) {
        if (pos.y > 0 && board[pos.y - 1, pos.x] == 0)
          moves.Add(new Vector2Int(pos.x, pos.y - 1));
        if (pos.y > 0 && pos.x > 0 && board[pos.y - 1, pos.x - 1] != 0 && Mathf.Sign(board[pos.y - 1, pos.x - 1]) != who)
          moves.Add(new Vector2Int(pos.x - 1, pos.y - 1));
        if (pos.y > 0 && pos.x < 7 && board[pos.y - 1, pos.x + 1] != 0 && Mathf.Sign(board[pos.y - 1, pos.x + 1]) != who)
          moves.Add(new Vector2Int(pos.x + 1, pos.y - 1));
      }
      else if (who == -1)  {
        if (pos.y < 7 && board[pos.y + 1, pos.x] == 0)
          moves.Add(new Vector2Int(pos.x, pos.y + 1));
        if (pos.y < 7 && pos.x < 7 && board[pos.y + 1, pos.x + 1] != 0 && Mathf.Sign(board[pos.y + 1, pos.x + 1]) != who)
          moves.Add(new Vector2Int(pos.x + 1, pos.y + 1));
        if (pos.y < 7 && pos.x > 0 && board[pos.y + 1, pos.x - 1] != 0 && Mathf.Sign(board[pos.y + 1, pos.x - 1]) != who)
          moves.Add(new Vector2Int(pos.x - 1, pos.y + 1));
      }
      return moves.ToArray();
    }
    /* ===================================================== */

    Vector2Int clicked_1 = new Vector2Int(-1, -1);
    Vector2Int clicked_2 = new Vector2Int(-1, -1);
    string play_chess_state = "Clicked None";


    void show_target_1() {
      GameObject arrow_object = Instantiate(Resources.Load("Etc/UIImage", typeof(GameObject))) as GameObject;
      /* ===================================================== */
      arrow_object.name = "Target Arrow";
      arrow_object.transform.SetParent(GameObject.Find("Chess Board").transform);
      /* ===================================================== */
      arrow_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Shape_1");
      /* ===================================================== */
      Vector2 s = new Vector2(arrow_object.GetComponent<Image>().preferredWidth, arrow_object.GetComponent<Image>().preferredHeight);
      arrow_object.GetComponent<RectTransform>().sizeDelta = s * 7;
      /* ===================================================== */
      arrow_object.GetComponent<RectTransform>().anchorMin = Vector2.zero;
      arrow_object.GetComponent<RectTransform>().anchorMax = Vector2.zero;
      arrow_object.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
      /* ===================================================== */
      arrow_object.GetComponent<RectTransform>().localPosition = Vector3.zero;
      arrow_object.GetComponent<RectTransform>().localPosition += new Vector3(7, 7, 0);
      arrow_object.GetComponent<RectTransform>().localPosition += new Vector3(7 * 56 + 56 / 2, 0, 0);
      arrow_object.GetComponent<RectTransform>().localPosition += new Vector3(0, 8 * 56 + 56 / 2, 0);
      /* ===================================================== */
    }

    void show_target_2() {
      /* ===================================================== */
      GameObject text_object = Instantiate(Resources.Load("Etc/UIText", typeof(GameObject))) as GameObject;
      /* ===================================================== */
      text_object.name = "Target Text";
      text_object.transform.SetParent(GameObject.Find("Chess Board").transform);
      /* ===================================================== */
      text_object.GetComponent<TMPro.TextMeshProUGUI>().text = "Король";
      text_object.GetComponent<TMPro.TextMeshProUGUI>().margin = new Vector4(0, 0, -190, -55);
      /* ===================================================== */
      text_object.GetComponent<RectTransform>().localPosition = Vector3.zero;
      text_object.GetComponent<RectTransform>().localPosition += new Vector3(7, 7, 0);
      text_object.GetComponent<RectTransform>().localPosition += new Vector3(8 * 56 + 56 / 2, 0, 0);
      text_object.GetComponent<RectTransform>().localPosition += new Vector3(0, 9 * 56 + 56 / 2, 0);
    }

    void show_target_3() {
      /* ===================================================== */
      GameObject text_object = Instantiate(Resources.Load("Etc/UIText", typeof(GameObject))) as GameObject;
      /* ===================================================== */
      text_object.name = "Target Text 2";
      text_object.transform.SetParent(GameObject.Find("Chess Board").transform);
      /* ===================================================== */
      text_object.GetComponent<TMPro.TextMeshProUGUI>().text = "Возьми его за 2 хода";
      text_object.GetComponent<TMPro.TextMeshProUGUI>().margin = new Vector4(0, 0, -210, -155);
      /* ===================================================== */
      text_object.GetComponent<RectTransform>().localPosition = Vector3.zero;
      text_object.GetComponent<RectTransform>().localPosition += new Vector3(7, 7, 0);
      text_object.GetComponent<RectTransform>().localPosition += new Vector3(8 * 56 + 56 / 2, 0, 0);
      text_object.GetComponent<RectTransform>().localPosition += new Vector3(0, 7 * 56 + 56 / 2, 0);
    }



    void hide_target_1() {
      Destroy(GameObject.Find("Chess Board/Target Arrow"));
    }

    void hide_target_2() {
      Destroy(GameObject.Find("Chess Board/Target Text"));
    }

    void hide_target_3() {
      Destroy(GameObject.Find("Chess Board/Target Text 2"));
    }



    void redraw_pieces() {
      GameObject board_object = GameObject.Find("Chess Board");
      /* ========= */
      if (!board_object)
        return;
      /* ========= */
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
            if (board[i,j] == 99)
              piece_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_78_copy_2");
            if (board[i,j] == 1000)
              piece_object.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_76_copy");
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

    void redraw_selector(Vector2 pos) {
      GameObject selector = GameObject.Find("Selector");
      if (!selector) {
        selector = Instantiate(Resources.Load("Etc/UIImage", typeof(GameObject))) as GameObject;
        selector.name = "Selector";
        selector.transform.SetParent(GameObject.Find("Chess Board").transform);
        selector.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_78");
        /* ========= */
        Vector2 s = new Vector2(selector.GetComponent<Image>().preferredWidth, selector.GetComponent<Image>().preferredHeight);
        selector.GetComponent<RectTransform>().sizeDelta = s * 7;
      }
      /* ========= */
      selector.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
      selector.GetComponent<RectTransform>().localPosition = Vector3.zero;
      selector.GetComponent<RectTransform>().localPosition += new Vector3(7, 7, 0);
      selector.GetComponent<RectTransform>().localPosition += new Vector3(pos.x * 56, 0, 0);
      selector.GetComponent<RectTransform>().localPosition += new Vector3(0, pos.y * 56, 0);
    }

    public void highlight(Vector2Int[] moves) {
      if (!GameObject.Find("Chess Board"))
        return;
      /* ========= */
      if (GameObject.Find("Chess Board/Highlight")) {
        Destroy(GameObject.Find("Chess Board/Highlight"));
      }
      /* ========= */
      GameObject h = new GameObject();
      h.name = "Highlight";
      h.transform.SetParent(GameObject.Find("Chess Board").transform);
      h.transform.localPosition = Vector3.zero;
      /* ========= */
      foreach (var p in moves) {
        GameObject m = Instantiate(Resources.Load("Etc/UIImage", typeof(GameObject))) as GameObject;;
        m.transform.SetParent(h.transform);
        m.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Layers/Layer_78_copy_3");
        m.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        m.GetComponent<RectTransform>().localPosition = Vector3.zero;
        m.GetComponent<RectTransform>().localPosition += new Vector3(7, 7, 0);
        m.GetComponent<RectTransform>().localPosition += new Vector3(p.x * 56, 0, 0);
        m.GetComponent<RectTransform>().localPosition += new Vector3(0, (7 - p.y) * 56, 0);
        /* ========= */
        Vector2 s = new Vector2(m.GetComponent<Image>().preferredWidth, m.GetComponent<Image>().preferredHeight);
        m.GetComponent<RectTransform>().sizeDelta = s * 7;
      }
    }


    void play_chess_1() {
      GameObject.Find("Pink Mist").GetComponent<SpriteRenderer>().enabled = true;
      /* ========= */
      redraw_board();
      redraw_pieces();
      /* ========= */
      GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
    }

    
    void play_chess_2() {
      Vector2 pos_0 = Input.mousePosition - GameObject.Find("Chess Board").GetComponent<RectTransform>().position;
      pos_0 -=  new Vector2(7, 7);
      pos_0 /=  new Vector2(56, 56);
      /* ========= */
      Vector2Int pos_1 = new Vector2Int((int)pos_0.x,  (int)pos_0.y);
      if (pos_1.x < 0 || pos_1.x > 7 || pos_1.y < 0 || pos_1.y > 7)
        return;
      /* ========= */
      redraw_selector(pos_1);
      switch (play_chess_state) {
        case "Clicked None":
          if (Input.GetMouseButtonDown(0)) {
            clicked_1 = new Vector2Int(pos_1.x, 7 - pos_1.y);
            /* ========= */
            highlight(xxx_moves(clicked_1));
            /* ========= */
            GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
            /* ========= */
            play_chess_state = "Clicked 1";
          } else if (Input.GetMouseButtonDown(1)) {
            GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
          }
          break;
        case "Clicked 1":
          if (Input.GetMouseButtonDown(0)) {
            clicked_2 = new Vector2Int(pos_1.x, 7 - pos_1.y);
            /* ========= */
            play_chess_state = "Clicked 2";
            /* ========= */
          } else if (Input.GetMouseButtonDown(1)) {
            GameObject.Find("Chess Touch").GetComponent<AudioSource>().Play();
            /* ========= */
            GameObject.Find("No 2").GetComponent<AudioSource>().Play();
            /* ========= */
            highlight(new Vector2Int[]{});
            /* ========= */
            play_chess_state = "Clicked None";
          }
          break;
        case "Clicked 2":
          if (move(clicked_1, clicked_2, 1))
            GameObject.Find("Yeah").GetComponent<AudioSource>().Play();
          else
            GameObject.Find("No").GetComponent<AudioSource>().Play();
          /* ========= */
          clicked_1 = new Vector2Int(-1, -1);
          clicked_2 = new Vector2Int(-1, -1);
          /* ========= */
          highlight(new Vector2Int[]{});
          /* ========= */
          play_chess_state = "Clicked None";
          /* ========= */
          break;
      }
    }
    
    void play_chess_3() {
      GameObject.Find("Pink Mist").GetComponent<SpriteRenderer>().enabled = false;
      /* ========= */
      Destroy(GameObject.Find("Chess Board"));    
    }
  }
}


