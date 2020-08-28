using System;
using CheckersBoard;

namespace Checkers
{
    public class Program
    {
        public static void Main()
        {
            Board board = new Board(10);
            board.printBoard();
            board.UpdateBoardAccordingToPlayersMove(6, 1, 5, 0);
            CheckerPiece.CheckersPiece ch= new CheckerPiece.CheckersPiece();
            ch.GotToOtherSideOfBoard(ref board);
        }
    }
}
