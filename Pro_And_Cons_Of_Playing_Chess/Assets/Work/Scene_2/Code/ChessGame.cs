using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_2 {
  static class ChessGame {
    
    static int turn = 1;

    public static int[,] board = new int [,] {
      {0, 0, 0, 0, 0, 0, 0, 0}, 
      {0, 0, 0, 0, 0, 0, 0, 0},
      {0, 0, 0, 0, 0, 0, 0, 0}, 
      {0, 0, 0, 0, 0, 0, 0, 0}, 
      {0, 0, 0, 0, 0, 0, 0, 0}, 
      {0, 0, 0, 0, 0, 0, 0, 0}, 
      {0, 0, 0, 0, 0, 0, 1, 0}, 
      {1, 0, 0, 0, 0, 0, 0, 0}, 
    };

    public static bool white_move(Vector2 from, Vector2 to) {
      int x1 = (int) from.x; int y1 = (int) from.y;
      int x2 = (int) to.x;   int y2 = (int) to.y;
      /* ========= */
      if (board[y1, x1] <= 0 || turn != 1)
        return false;
      if (board[y1, x1] == 1)
        return white_pawn_move(from, to);
      else
        return false;
    }

    private static bool white_pawn_move(Vector2 from, Vector2 to) {
      int x1 = (int) from.x; int y1 = (int) from.y;
      int x2 = (int) to.x;   int y2 = (int) to.y;
      /* ========= */
      if ((y1 - y2) == 1 && (x1 - x2) == 0) {
        board[y2, x2] = board[y1, x1];
        board[y1, x1] = 0;
        /* ========= */
        turn = -1;
        /* ========= */
        return true;
      } else {
        return false;
      }
    }
  }
}


