using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_2 {
  static class ChessGame {
    public static bool white_move(int[,] board, Vector2 from, Vector2 to) {
      int x1 = (int) from.x; int y1 = (int) from.y;
      int x2 = (int) to.x;   int y2 = (int) to.y;
      /* ========= */
      if (board[y1, x1] == 0)
        return false;
      if (board[y1, x1] == 1)
        return pawn_move(board, from, to);
      else
        return false;
    }

    private static bool pawn_move(int[,] board, Vector2 from, Vector2 to) {
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
  }
}


