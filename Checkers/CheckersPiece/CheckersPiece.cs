using System;
using CheckersBoard;
namespace CheckersPiece
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
        int m_Height;
        int m_width;


        public CheckersPiece(ePieceKind i_PieceKind, int i_Height , int i_Width)
        {
            m_PieceKind = i_PieceKind;
            m_IsKing = false;
            m_Height = i_Height;
            m_width = i_Width; 
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

        public int Height
        {
            get
            {
                return m_Height;
            }
        }

        public int width
        {
            get
            {
                return m_width;
            }
        }

        private void gotToOtherSideOfBoard(ref Board i_CheckersBoard)
        {
            if(this.PieceKind == ePieceKind.X && m_Height == 0 
              || this.PieceKind == ePieceKind.O && m_Height == (uint)i_CheckersBoard.SizeOfBoard)
            {
                this.becomeKing();
            }
        }
       public void becomeKing()
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
      



    }
}
