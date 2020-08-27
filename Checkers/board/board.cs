using System;
using System.Text;
namespace CheckersBoard
{
    public class Board
    {
        public enum eBoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        private const char k_BlankChecker = ' '; // constant.

        private readonly char[,] m_CheckersBoard;
        private readonly eBoardSize m_SizeOfBoard;
        public Board(eBoardSize i_SizeOfBoard)
        {
            m_CheckersBoard = new char[(uint)i_SizeOfBoard, (uint)i_SizeOfBoard];
            m_SizeOfBoard = i_SizeOfBoard;
            this.initializeBoard();
        }
        public eBoardSize SizeOfBoard
        {
            get
            {
                return m_SizeOfBoard;
            }
        }

        public char[,] CheckersBoard
        {
            get
            {
                return m_CheckersBoard;
            }
        }
        private void initializeBoard()
        {
            makeEmptyBoard();
            placePlayers();
        }
        private void makeEmptyBoard()
        {
            for (int i = 0; i < (uint)m_SizeOfBoard; i++)
            {
                for (int j = 0; j < (uint)m_SizeOfBoard; j++)
                {
                    m_CheckersBoard[i, j] = ' ';
                }
            }
        }

        private void  placePlayers()
        {
            int i = 0;
            while((uint)m_SizeOfBoard - 1 - i != i+1)
            {
               for(int j = 0; j < (uint)m_SizeOfBoard; j++)
                {
                    placePlayerAccordingToToRowAndCol(i, j);
                }

                i++;
            }
        }
        private void  placePlayerAccordingToToRowAndCol(int i_Row,int i_Col)
        {
            if (i_Row % 2 == 0)
            {
                if (i_Col % 2 != 0)
                {
                    m_CheckersBoard[i_Row, i_Col] = 'O';
                }
                else
                {
                    m_CheckersBoard[(uint)m_SizeOfBoard - 1 - i_Row, i_Col] = 'X';
                }
            }
            else
            {
                if (i_Col % 2 == 0)
                {
                    m_CheckersBoard[i_Row, i_Col] = 'O';
                }
                else
                {
                    m_CheckersBoard[(uint)m_SizeOfBoard - 1 - i_Row, i_Col] = 'X';
                }
            }
        }

          public void printBoard()
          {
            string LineString = createLineString((uint)m_SizeOfBoard);
            char leftIndex = 'a';
            StringBuilder Board = new StringBuilder(leftIndex + '|');
            printTopindex((uint)m_SizeOfBoard);
            for (int i = 0; i < (uint)m_SizeOfBoard; i++)
            {
                Board.AppendFormat(Environment.NewLine + LineString + Environment.NewLine + leftIndex + "|");
                for (int j = 0; j < (uint)m_SizeOfBoard; j++)
                {
                   Board.AppendFormat(" " + m_CheckersBoard[i, j] + " |");
                }
                leftIndex++;
            }

            Console.WriteLine(Board);
        }

        public static void printTopindex(uint i_SizeOfBoard)
        {
            char letterIndex = 'G';
            StringBuilder indexesLine = new StringBuilder("   A   B   C   D   E   F   ");

            for (int i = 6; i < i_SizeOfBoard; i++)
            {
                indexesLine.AppendFormat( letterIndex + "   ");
                letterIndex++;
            }

            Console.Write(indexesLine);
        }

        private static string createLineString(uint i_SizeOfBoard)
        {
            StringBuilder EqualsLine = new StringBuilder(" ========================");

            for(int i = 6; i < i_SizeOfBoard; i++)
            {
                EqualsLine.Append("====");
            }

            return EqualsLine.ToString();
        }

        public bool IsCheckerAvailable(string i_Move)
        {
            int height;
            int width = getIndexInBoard(ref i_Move, out height);

            return isCheckerValidPosition(height, width) && m_CheckersBoard[height, width] == k_BlankChecker;             
        }
       
        private int getIndexInBoard(ref string i_DestinationChecker, out int o_Height)
        {
            o_Height = i_DestinationChecker[0] - 'A';
            return i_DestinationChecker[1] - 'a';
        }

        private bool isCheckerValidPosition(int i_Height, int i_Width)
        {
            return i_Width < (uint)m_SizeOfBoard && i_Width >= 0 || i_Height < (uint)m_SizeOfBoard && i_Height >= 0;
        }

        private void updateBoardAccordingToPlayersMove(int i_Row,int i_Col, int i_NewRow, int i_NewCol)
        {
            m_CheckersBoard[i_NewRow, i_NewCol] = m_CheckersBoard[i_Row, i_Col];
            m_CheckersBoard[i_Row, i_Col] = ' ';
        }

        private void clearPositionOfDeadCheckerPiece(int i_Row, int i_Col)
        {
            m_CheckersBoard[i_Row, i_Col] = ' ';
        }

        private void updateAfterEating(int i_row, int i_col,int i_newRow,int i_NewCol, int i_RowTokill, int i_ColTokill )
        {
            updateBoardAccordingToPlayersMove(i_row, i_col, i_newRow, i_NewCol);
            clearPositionOfDeadCheckerPiece(i_RowTokill, i_ColTokill);
        }
    }
    
    
}
