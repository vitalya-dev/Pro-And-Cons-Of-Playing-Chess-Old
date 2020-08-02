using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

static class ChessRules {
  public static bool move(int[,] board, Vector2 from, Vector2 to) {
    int x1 = (int) from.x; int y1 = (int) from.y;
    int x2 = (int) to.x;   int y2 = (int) to.y;
    /* ========= */
    if (board[y1, x1] == 0)
      return false;
    /* ========= */
    board[y2, x2] = board[y1, x1];
    board[y1, x1] = 0;
    /* ===================================================== */
    return true;
  }

}
