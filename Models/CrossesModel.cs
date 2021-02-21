using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CrossesModel
    {
        const int EMPTY = 0;
        const int CROSS = 1;
        const int ZERO = 2;
        const int lineLength = 3;

        private int maxMoves;
        public string zeroId { get; set; }
        public string crossId { get; set; }
        private bool zerosMove;

        private int[,] gameField;
        public CrossesModel()
        {
            maxMoves = lineLength * lineLength;
            zerosMove = true;
            gameField = new int[lineLength, lineLength];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>[0] != 0 if move suceed; [1] > 0 if curr player wins, = 0 if draw, < 0 if continue</returns>
        public int[] MakeMove(string playerId, int row, int column)
        {
            int[] result = new int[2];            
            if ((zerosMove && playerId != zeroId) || (!zerosMove && playerId != crossId) 
                || (gameField[row,column] != EMPTY)) return result;
            gameField[row, column] = zerosMove ? ZERO : CROSS;
            result[0] = 1;
            result[2] = CheckMove(zerosMove ? ZERO : CROSS, row, column);
            zerosMove = !zerosMove;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <returns>=0 if draw, >0 if win, <0 if continue</returns>
        private int CheckMove(int player, int x, int y)
        {
            maxMoves--;
            int row = 0, col = 0, diag = 0, rdiag = 0;
            for (int i = 0; i < lineLength; i++)
            {
                if (gameField[x, i] == player) row++;
                if (gameField[i, y] == player) col++;
                if (gameField[i, i] == player) diag++;
                if (gameField[i, lineLength - i] == player) rdiag++;
            }
            if (row == lineLength || col == lineLength || diag == lineLength || rdiag == lineLength) return 1;
            return maxMoves == 0 ? 0 : -1;
        }
    }
}