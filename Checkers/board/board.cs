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

        private char[,] m_CheckersBoard;
        private eBoardSize m_SizeOfBoard;
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

            set
            {
                m_SizeOfBoard = value;
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
                    if(i % 2 == 0)
                    {
                        if (j % 2 != 0)
                        {
                            m_CheckersBoard[i,j] = 'O';
                        }
                        else
                        {
                            m_CheckersBoard[(uint)m_SizeOfBoard - 1 - i, j] = 'X';
                        }
                    }
                    else
                    {
                        if (j % 2 == 0)
                        {
                            m_CheckersBoard[i, j] = 'O';
                        }
                        else
                        {
                            m_CheckersBoard[(uint)m_SizeOfBoard - 1 - i, j] = 'X';
                        }
                    }
                }

                i++;
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
                indexesLine.Append(letterIndex + "   ");
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

        private bool isCheckerAvailable(string i_Move)
        {
            int height;
            int width = getIndexInBoard(ref i_Move, out height);

            return isCheckerValid(height, width) && m_CheckersBoard[height, width] == ' ';             
        }
       
        private int getIndexInBoard(ref string i_Move, out int o_Height)
        {
            o_Height = i_Move[0] - 'A';

            return i_Move[1] - 'a';
        }

        private bool isCheckerValid(int i_Height, int i_Width)
        {
            return i_Width < (uint)m_SizeOfBoard && i_Width >= 0 || i_Height < (uint)m_SizeOfBoard && i_Height >= 0;
                }

    }
}
