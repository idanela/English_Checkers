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

        private bool m_IsKing;
        public ePieceKind m_PieceKind;
        bool m_IsAlive;
        int m_RowIndex;
        int m_ColIndex;


        public CheckersPiece(ePieceKind i_PieceKind, int i_Height , int i_Width)
        {
            m_IsKing = false;
            m_PieceKind = i_PieceKind;
            m_IsAlive = true;
            m_RowIndex = i_Height;
            m_ColIndex = i_Width; 
        }

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

        public int Height
        {
            get
            {
                return m_RowIndex;
            }
        }

        public int width
        {
            get
            {
                return m_ColIndex;
            }
        }

        public void GotToOtherSideOfBoard(ref Board i_CheckersBoard)
        {
            if(this.PieceKind == ePieceKind.X && m_RowIndex == 0 
              || this.PieceKind == ePieceKind.O && m_RowIndex == (uint)i_CheckersBoard.SizeOfBoard)
            {
                this.becomeKing();
            }
        }
       private void becomeKing()
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
        public void Die()
        {
            m_IsAlive = false;
        }

        public void changePosition(int i_NewRowIndex,int  i_NewColIndex)
        {
            m_RowIndex = i_NewRowIndex;
            m_ColIndex = i_NewColIndex;
        }
      



    }
}
