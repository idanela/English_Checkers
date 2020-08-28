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
        // Constants
        private const char k_BlankChecker = ' ';
        private const int K_smallestBoardSize = 6;

        //Members
        private readonly char[,] m_CheckersBoard;
        private readonly eBoardSize m_SizeOfBoard;
        public Board(eBoardSize i_SizeOfBoard) // Constructor.
        {
            m_CheckersBoard = new char[(uint)i_SizeOfBoard, (uint)i_SizeOfBoard];
            m_SizeOfBoard = i_SizeOfBoard;
            this.initializeBoard();
        }

        //ProPerties
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

        private void initializeBoard() // initialize start board
        {
            makeEmptyBoard();
            placePlayers();
        }

        private void makeEmptyBoard() // Creates an empty board.
        {
            for (int i = 0; i < (uint)m_SizeOfBoard; i++)
            {
                for (int j = 0; j < (uint)m_SizeOfBoard; j++)
                {
                    m_CheckersBoard[i, j] = k_BlankChecker;
                }
            }
        }

        private void  placePlayers() // Place Checkers pieces on an empty board.
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
        private void placePlayerAccordingToToRowAndCol(int i_Row, int i_Col) //Place Checker pieces According to row and col index
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

          public void printBoard() // print game Board
          {
            string LineString = createLineString((uint)m_SizeOfBoard);
            char leftIndex = 'a';
            StringBuilder Board = new StringBuilder(leftIndex + '|');
            printTopindex((uint)m_SizeOfBoard);
            for (int i = 0; i < (uint)m_SizeOfBoard; i++)
            {
                Board.AppendFormat(@"
{0}
{1}|",LineString,leftIndex);
                for (int j = 0; j < (uint)m_SizeOfBoard; j++)
                {
                   Board.AppendFormat(" {0} |" , m_CheckersBoard[i, j]);
                }
                leftIndex++;
            }

            Console.WriteLine(Board);
        }

        public static void printTopindex(uint i_SizeOfBoard) // prints top indexes of the board 
        {
            char letterIndex = 'G';
            StringBuilder indexesLine = new StringBuilder("   A   B   C   D   E   F   ");

            for (int i = K_smallestBoardSize; i < i_SizeOfBoard; i++)
            {
                indexesLine.AppendFormat("{0}   ",letterIndex);
                letterIndex++;
            }

            Console.Write(indexesLine);
        }

        private static string createLineString(uint i_SizeOfBoard) // creates the line that seperates between to checkers.
        {
            StringBuilder EqualsLine = new StringBuilder(" ========================");

            for(int i = K_smallestBoardSize; i < i_SizeOfBoard; i++)
            {
                EqualsLine.Append("====");
            }

            return EqualsLine.ToString();
        }

        public bool IsCheckerAvailable(string i_Move) // Checks if a checker is possible to go to.
        {
            int height;
            int width = getIndexInBoard(ref i_Move, out height);

            return isCheckerValidPosition(height, width) && m_CheckersBoard[height, width] == k_BlankChecker;             
        }
       
        private int getIndexInBoard(ref string i_DestinationChecker, out int o_Height) // Gets an index according to the name of checker. 
        {
            o_Height = i_DestinationChecker[0] - 'A';
            return i_DestinationChecker[1] - 'a';
        }

        private bool isCheckerValidPosition(int i_Height, int i_Width) // Checks if checker is in bound.
        {
            return i_Width < (uint)m_SizeOfBoard && i_Width >= 0 || i_Height < (uint)m_SizeOfBoard && i_Height >= 0;
        }

        //update the board according to player move
        private void updateBoardAccordingToPlayersMove(int i_Row,int i_Col, int i_NewRow, int i_NewCol)
        {
            m_CheckersBoard[i_NewRow, i_NewCol] = m_CheckersBoard[i_Row, i_Col];
            m_CheckersBoard[i_Row, i_Col] = k_BlankChecker;
        }

        //clear a checker of a checker piece that was eaten
        private void clearPositionOfDeadCheckerPiece(int i_Row, int i_Col)
        {
            m_CheckersBoard[i_Row, i_Col] = k_BlankChecker;

        }    
        
        // Update the board after a checker piece eat another.
            private void updateAfterEating(int i_row, int i_col,int i_newRow,int i_NewCol, int i_RowTokill, int i_ColTokill )
        {
            updateBoardAccordingToPlayersMove(i_row, i_col, i_newRow, i_NewCol);
            clearPositionOfDeadCheckerPiece(i_RowTokill, i_ColTokill);
        }
    }
    
    
}
