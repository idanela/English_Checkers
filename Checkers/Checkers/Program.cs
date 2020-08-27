using System;
using CheckersBoard;

namespace Checkers
{
    public class Program
    {
        public static void Main()
        {
            Board board = new Board((Board.eBoardSize) 10);
            board.printBoard();
            CheckerPiece.CheckersPiece ch= new CheckerPiece.CheckersPiece();
            ch.GotToOtherSideOfBoard(ref board);
        }
    }
}
