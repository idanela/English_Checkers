using System;
using CheckersBoard;

namespace CheckerPiece
{
   public class CheckersPiece
    {
        public enum ePieceKind
        {
          MainPlayerTool = 'O',
          SecondPlayerTool = 'X', 
          MainPlayerKing = 'U',
          SecondPlayerKing = 'K'
        }

        // Members
        private bool m_IsKing;
        private ePieceKind m_PieceKind;
        private ushort m_RowIndex;
        private ushort m_ColIndex;

        public CheckersPiece(ePieceKind i_PieceKind, ushort i_RowIndex, ushort i_ColIndex) // Constructor.
        {
            m_IsKing = false;
            m_PieceKind = i_PieceKind;
            m_RowIndex = i_RowIndex;
            m_ColIndex = i_ColIndex;
        }

        // Prpoerties
       public ePieceKind PieceKind
        {
            get
            {
                return m_PieceKind;
            }

            set
            {
                m_PieceKind = value;
            }
        }

        public bool IsKing
        {
            get
            {
                return m_IsKing;
            }
        }

        public ushort ColIndex
        {
            get
            {
                return m_ColIndex;
            }
        }

        public ushort RowIndex
        {
            get
            {
                return m_RowIndex;
            }
        }

        public void GotToOtherSideOfBoard(Board i_CheckersBoard) // Checks if a checker piece need to become a king.
        {
            if((this.PieceKind == ePieceKind.MainPlayerTool && m_RowIndex == (ushort)i_CheckersBoard.SizeOfBoard - 1)
              || (this.PieceKind == ePieceKind.SecondPlayerTool && m_RowIndex == 0))
            {
                this.becomeKing();
                updateKingChecker(i_CheckersBoard);
            }
        }

       private void becomeKing() // makes a checker piece a king.
        {
            if(m_PieceKind == ePieceKind.MainPlayerTool)
            {
                m_PieceKind = ePieceKind.MainPlayerKing;
            }
            else
            {
                m_PieceKind = ePieceKind.SecondPlayerKing;
            }

            m_IsKing = true;
        }

        private void updateKingChecker(Board i_CheckersBoard)
        {
            i_CheckersBoard.CheckersBoard[this.RowIndex, this.ColIndex] = (char)this.PieceKind;
        }

        public void ChangePosition(ushort i_NewRowIndex, ushort i_NewColIndex) // Changes a piece location.
        {
            m_RowIndex = i_NewRowIndex;
            m_ColIndex = i_NewColIndex;
        }
    }
}
