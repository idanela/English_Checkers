using System;
namespace board
{
    public class Board
    {
        private char[,] m_CheckersBoard;
        uint m_SizeOfBoard;
        public Board(uint i_Size)
        {
            m_CheckersBoard = new char [i_Size, i_Size];
            m_SizeOfBoard= i_Size;
        }

        private void initializeBoard(int i_SizeOfBoard)
        {
          
        }
            
        private void initializePlayersLine(uint numOfLine, char playerSign )
        {
            
        }
        
        
    }
}
