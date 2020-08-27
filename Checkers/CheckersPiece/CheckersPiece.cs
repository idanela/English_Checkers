using System;

namespace CheckersPiece
{
   public struct CheckersPiece
    {
        private ePieceKind m_PieceKind;

        public CheckersPiece(ePieceKind i_PieceKind)
        {
            m_PieceKind = i_PieceKind;
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

        public enum ePieceKind
        {
            X,
            Y
        }


    }
}
