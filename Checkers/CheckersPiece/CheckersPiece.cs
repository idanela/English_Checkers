using System;

namespace CheckersPiece
{
   public struct CheckersPiece
    {
        private ePieceKind m_PieceKind;
        private static uint s_NumOfPieces;

        public CheckersPiece(ePieceKind i_PieceKind)
        {
            m_PieceKind = i_PieceKind;
            s_NumOfPieces++;
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

        public static uint NumOfPieces
        {
            get
            {
                return s_NumOfPieces ;
            }
        }

        public enum ePieceKind
        {
            X,
            Y
        }


    }
}
