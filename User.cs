using System;
using System.Runtime.CompilerServices;
using CheckerPiece;
using CheckersBoard;

namespace Player
{
    public struct User
    {
        // Constants:
        private const char k_Quit = 'Q';
        private const char k_Yes = 'Y';
        private const char k_Empty = ' ';

        // Data members:
        private readonly string m_Name;
        private ushort m_Score;
        private static CheckersPiece[] m_CheckersPiece;
        private readonly char m_CheckerPieceKind;
        private readonly short m_PlayerNumber;

        // Enums:
        public enum ePlayerType
        {
            MainPlayer = 0,
            RivalPlayer = 1
        }


        // Constructors:
        public User(string i_Name, short i_PlayerNumber, char i_CheckerKind)
        {
            m_Name = i_Name;
            m_Score = 0;
            m_CheckerPieceKind = i_CheckerKind;
            m_PlayerNumber = i_PlayerNumber;
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

        public CheckersPiece[] Pieces
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
        public static void InitializeCheckersArray(int i_BoardSize)
        {
            int sizeOfPieces = ((i_BoardSize / 2) * 3);

            m_CheckersPiece = new CheckersPiece[sizeOfPieces];
        }

        public void Play(ref Board i_GameBoard, User i_RivalPlayer , string i_CheckerPosition, string i_CheckerNextPosition)
        {
            // First, check if the given input is valid.
            Validation.CheckValidInput(Name,
                ref i_CheckerPosition,
                ref i_CheckerNextPosition, 
                (int)i_GameBoard.SizeOfBoard);

            // Second, finds the match checker piece, with it's position.
            CheckersPiece? currentCheckerPiece = findCheckerPiece(i_CheckerPosition);

            // Checks if the checker piece was found.
            if (currentCheckerPiece != null)
            {
                // Make the moves that the user wanted. Regular move or King move.
                if (!currentCheckerPiece.Value.IsKing)
                {
                    MoveUtils.MoveRegularTool(this, i_RivalPlayer,
                                              ref i_GameBoard,
                                              currentCheckerPiece.Value,
                                              i_CheckerNextPosition);
                }
                else
                {
                    MoveUtils.MoveKingTool(i_CheckerPosition, i_CheckerNextPosition);
                }
            }
        }

        private static CheckersPiece? findCheckerPiece(string i_CurrentPosition)
        {
            CheckersPiece? currentCheckerPiece = null;

            foreach (var piece in m_CheckersPiece)
            {
                // if piece.position == i_CurrentPosition
                // currentCheckerPiece = piece;
            }

            return currentCheckerPiece;
        }

        public static void Quit(char i_QuitInput)
        {
            // Quit the game if the user press 'Q'.
            char answer;

            if (i_QuitInput == k_Quit)
            {
                // Make sure if the player really intended to quit the game or pressed by mistake.
                Console.WriteLine("Are you sure you want to quit the game? press Y if yes.");
                char.TryParse(Console.ReadLine(), out answer);

                if (answer == k_Yes)
                {
                    // Show score, and declare winner and loser.
                }
            }

            // Continue otherwise.
        }
    }
}
