using System;
using CheckersBoard;

namespace CheckerPiece
{
   public struct CheckersPiece
    {
        public enum ePieceKind
        {
            X,
            O, 
            K,
            U
        }

        // Members
        private bool m_IsKing;
        private ePieceKind m_PieceKind;
        private bool m_IsAlive;
        private ushort m_RowIndex;
        private ushort m_ColIndex;

        public CheckersPiece(ePieceKind i_PieceKind, ushort i_Height, ushort i_Width) // Constructor.
        {
            m_IsKing = false;
            m_PieceKind = i_PieceKind;
            m_IsAlive = true;
            m_RowIndex = i_Height;
            m_ColIndex = i_Width; 
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
                return IsKing;
            }
        }

        public bool IsAlive
        {
            get
            {
                return m_IsAlive;
            }
        }

        public ushort RowIndex
        {
            get
            {
                return m_RowIndex;
            }
        }

        public ushort ColIndex
        {
            get
            {
                return m_ColIndex;
            }
        }

        public void GotToOtherSideOfBoard(ref Board i_CheckersBoard) // Checks if a checker piece need to become a king.
        {
            if((this.PieceKind == ePieceKind.X && m_RowIndex == 0) 
              || (this.PieceKind == ePieceKind.O && m_RowIndex == (ushort)i_CheckersBoard.SizeOfBoard - 1))
            {
                this.becomeKing();
            }
        }

       private void becomeKing() // makes a checker piece a king.
        {
            if(m_PieceKind == ePieceKind.X)
            {
                m_PieceKind = ePieceKind.K;
            }
            else
            {
                m_PieceKind = ePieceKind.U;
            }

            m_IsKing = true;
        }

        public void Die() // "Kill" a checker piece. 
        {
            m_IsAlive = false;
        }

        public void changePosition(ushort i_NewRowIndex, ushort i_NewColIndex) // Changes a piece location.
        {
            m_RowIndex = i_NewRowIndex;
            m_ColIndex = i_NewColIndex;
        }
    }
}
