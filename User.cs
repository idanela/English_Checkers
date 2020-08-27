using System;
using System.Runtime.CompilerServices;
using CheckersPiece;

namespace Player
{
    public struct User
    {
        // Constants:
        private const short k_NumberOfCheckerPieces = 12;

        // Data members:
        private readonly string m_Name;
        private ushort m_Score;
        private static CheckerPiece[] m_CheckersPiece;
        private readonly char m_CheckerPieceKind;


        // Constructors:
        public User(string i_Name, char i_CheckerKind)
        {
            m_Name = i_Name;
            m_Score = 0;
            m_CheckersPiece = new CheckerPiece[k_NumberOfCheckerPieces];
            m_CheckerPieceKind = i_CheckerKind;
        }

        // Properties:
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public ushort Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public CheckerPiece[] Pieces
        {
            get
            {
                return m_CheckersPiece;
            }
        }

        public char CheckerKind
        {
            get
            {
                return m_CheckerPieceKind;
            }
        }


        // Methods:
        public static void Play(string i_CheckerPosition, string i_CheckerNextPosition)
        {
            // This method called to play the user's move.
            CheckerPiece? currentCheckerPiece = findCheckerPiece(i_CheckerPosition);

            if (currentCheckerPiece != null)
            {
                if (isRegularChecker(currentCheckerPiece.Value))
                {
                    MoveRegularTool(i_CheckerPosition, i_CheckerNextPosition);
                }
                else
                {
                    MoveKingTool(i_CheckerPosition, i_CheckerNextPosition);
                }
            }
        }

        private static CheckerPiece? findCheckerPiece(string i_CurrentPosition)
        {
            CheckerPiece? currentCheckerPiece = null;

            foreach (var piece in m_CheckersPiece)
            {
                // if piece.position == i_CurrentPosition
                // currentCheckerPiece = piece;
            }

            return currentCheckerPiece;
        }

        private static bool isRegularChecker(CheckerPiece i_CheckerPiece)
        {
            return i_CheckerPiece.PieceKind == CheckerPiece.ePieceKind.FirstPlayer ||
                   i_CheckerPiece.PieceKind == CheckerPiece.ePieceKind.SecondPlayer;
        }

        public static void Quit(string i_QuitInput)
        {
            // Need to quit the game if the user press q(?).
        }

        public static void MoveRegularTool(string i_PositionFrom, string i_PositionTo)
        {
            // Check valid move - include: inborder, valid input, is empty cell.
            // Update board - new tool position.
            // Check if can eat.
        }

        private static void checkValidMove(string i_PositionTo)
        {
            // Check if the position is in border,
            // is empty cell available in the given position.
            // Check valid indexes: A-H, a-h.
        }

        public static void MoveKingTool(string i_PositionFrom, string i_PositionTo)
        {
            // Check valid move - include: inborder, valid input, is empty cell (for king tool).
            // Update board - new tool position
            // Check if can eat.
        }

        private static void checkValidKingMove(string i_PositionTo)
        {
            // Check if the position is in border,
            // is empty cell available in the given position - for king tool.
            // Check valid indexes: A-H, a-h.
        }

        private static bool isMoveInBorders(string i_PositionTo)
        {
            // Check if the given position is in the borders.
            // Means the index is less(!) than border's size.

            return false;   // Temporary return value.
        }
    }
}
