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


        public CheckersPiece(ePieceKind i_PieceKind)
        {
            m_PieceKind = i_PieceKind;
            m_IsKing = false;
            m_Height = 0;
            m_width = 0; 
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


        private void gotToOtherSideOfBoard(ref Board i_CheckersBoard)
        {
            if(this.PieceKind == ePieceKind.X && m_Height == 0 
              || this.PieceKind == ePieceKind.O && m_Height == (uint)i_CheckersBoard.SizeOfBoard)
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
      



    }
}
