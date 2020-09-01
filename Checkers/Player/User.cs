using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;
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
        private const short k_RowIndex = 1;
        private const short k_ColIndex = 0;

        // Data members:
        private readonly string m_Name;
        private ushort m_Score;
        private CheckersPiece[] m_CheckersPiece;
        private readonly CheckersPiece.ePieceKind m_CheckerPieceKind;
        private readonly ePlayerType m_PlayerNumber;
        private Dictionary<string, List<string>> m_Moves;
        private CheckersPiece m_CurrentCheckerPiece;

        // Enums:
        public enum ePlayerType
        {
            MainPlayer = 0,
            RivalPlayer = 1
        }


        // Constructors:
        public User(string i_Name, ePlayerType i_PlayerNumber, CheckersPiece.ePieceKind i_CheckerKind)
        {
            m_Name = i_Name;
            m_Score = 0;
            m_CheckersPiece = null;
            m_CheckerPieceKind = i_CheckerKind;
            m_PlayerNumber = i_PlayerNumber;
            m_Moves = null;
            m_CurrentCheckerPiece = null;
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

        public CheckersPiece.ePieceKind CheckerKind
        {
            get
            {
                return m_CheckerPieceKind;
            }
        }

        public ePlayerType PlayerNumber
        {
            get
            {
                return m_PlayerNumber;
            }
        }

        public Dictionary<string, List<string>> Moves
        {
            get
            {
                return m_Moves;
            }
        }


        // Methods:
        public void InitializeCheckersArray(Board i_GameBoard)
        {
            int numOfPieces = ((i_GameBoard.SizeOfBoard / 2) * 3);
            int starIndex = 0;
            m_CheckersPiece = new CheckersPiece[numOfPieces];
            if(m_CheckerPieceKind == CheckersPiece.ePieceKind.MainPlayerTool)
            {
                starIndex = i_GameBoard.SizeOfBoard - 1;
            }
            this.ininitializePositions(starIndex, i_GameBoard, numOfPieces);
        }

       public void ininitializePositions(int startIndex, Board i_GameBoard, int i_NumOfPieces)
        {
            foreach (CheckersPiece checker in m_CheckersPiece)
            {
                while (i_NumOfPieces != 0)
                {
                    if (startIndex == 0)
                    {             
                        for(int i = 0; i <i_GameBoard.SizeOfBoard; i++ )
                        {
                            for (int j = 0; j < i_GameBoard.SizeOfBoard; j++)
                            {
                                if (i_GameBoard.CheckersBoard[i, j] == 'O')
                                {
                                    checker.changePosition((ushort)i, (ushort)j);
                                }
                            }

                        }        
                }
            else
            {
                        for (int i = i_GameBoard.SizeOfBoard - 1; i >= 0; i--)
                        {
                            for (int j = i_GameBoard.SizeOfBoard - 1; j < i_GameBoard.SizeOfBoard; j--)
                            {
                                if (i_GameBoard.CheckersBoard[i, j] == 'X')
                                {
                                    checker.changePosition((ushort)i, (ushort)j);
                                }
                            }

                        }
            }
        }

        public void GetAndCalculateScore()
        {
            foreach (CheckersPiece checkersPiece in Pieces)
            {
                if (checkersPiece.IsKing)
                {
                    m_Score += 4;
                }
                else
                {
                    m_Score++;
                }
            }
        }

        public void MakeToolAKing(Board i_GameBoard, ref CheckersPiece i_CurrentCheckerPiece)
        {
            //if (PlayerNumber == ePlayerType.MainPlayer)
            //{
            //    if (hasReachedFinalRowUp(i_CurrentCheckerPiece.RowIndex))
            //    {
            //        i_CurrentCheckerPiece.BecomeKing();
            //    }
            //}
            //else
            //{
            //    if (hasReachedFinalRowDown(i_GameBoard.SizeOfBoard, i_CurrentCheckerPiece.RowIndex))
            //    {
            //        i_CurrentCheckerPiece.BecomeKing();
            //    }
            //}
            i_CurrentCheckerPiece.GotToOtherSideOfBoard(ref i_GameBoard);
        }

        private static bool hasReachedFinalRowDown(ushort i_BoardSize, ushort i_CurrentRow)
        {
            return i_CurrentRow == i_BoardSize - 1;
        }

        private static bool hasReachedFinalRowUp(ushort i_CurrentRow)
        {
            return i_CurrentRow == 0;
        }

        public void MakeMove(ref Board i_GameBoard, string i_PositionFrom, string i_PositionTo)
        {
            ushort rowIndex = (ushort) (i_PositionFrom[k_RowIndex] - 'a');
            ushort colIndex = (ushort)(i_PositionFrom[k_ColIndex] - 'A');
            bool isCheckerUpdated = isCheckerFoundAndUpdate(rowIndex, colIndex);
           
            if (isCheckerUpdated)
            {
                if (!m_CurrentCheckerPiece.IsKing)
                {
                    MoveUtils.MoveRegularTool(this, ref i_GameBoard, ref m_CurrentCheckerPiece, i_PositionFrom, i_PositionTo);
                }
                else
                {
                    MoveUtils.MoveKingTool(this, ref i_GameBoard, ref m_CurrentCheckerPiece, i_PositionFrom, i_PositionTo);
                }
            }
        }

        public void MakeComputerMove(ref Board i_GameBoard)
        {
            string positionFrom = randomPositionFrom(ref i_GameBoard);
            string positionTo = randomPositionTo(ref i_GameBoard);

            if (!m_CurrentCheckerPiece.IsKing)
            {
                MoveUtils.MoveRegularTool(this, ref i_GameBoard, ref m_CurrentCheckerPiece, positionFrom, positionTo);
            }
            else
            {
                MoveUtils.MoveKingTool(this, ref i_GameBoard, ref m_CurrentCheckerPiece, positionFrom, positionTo);
            }
        }

        private string randomPositionFrom(ref Board i_GameBoard)
        {
            ushort rowIndex = (ushort) new Random().Next(i_GameBoard.SizeOfBoard - 1);
            ushort colIndex = (ushort)new Random().Next(i_GameBoard.SizeOfBoard - 1);

            while (!Validation.IsValidPosition(i_GameBoard.SizeOfBoard, rowIndex, colIndex) &&
                   !isCheckerFoundAndUpdate(rowIndex, colIndex))
            {
                rowIndex = (ushort)new Random().Next(i_GameBoard.SizeOfBoard - 1);
                colIndex = (ushort)new Random().Next(i_GameBoard.SizeOfBoard - 1);
            }

            char[] position = new char[2];
            position[0] = (char) (m_CurrentCheckerPiece.RowIndex + 'a');
            position[1] = (char)(m_CurrentCheckerPiece.ColIndex + 'A');

            return new string(position);
        }

        private string randomPositionTo(ref Board i_GameBoard)
        {
            string positionTo = null;
            char[] position = new char[2];

            // Down
            if (CheckerKind == CheckersPiece.ePieceKind.SecondPlayerMainTool)
            {
                if (i_GameBoard.CheckersBoard[m_CurrentCheckerPiece.RowIndex + 1, m_CurrentCheckerPiece.ColIndex + 1] != 'O' &&
                    i_GameBoard.IsCheckerAvailable((ushort) (m_CurrentCheckerPiece.RowIndex + 2),
                        (ushort)(m_CurrentCheckerPiece.ColIndex + 2)))
                {
                    position[0] = (char)(m_CurrentCheckerPiece.RowIndex + 'a' + 2);
                    position[1] = (char)(m_CurrentCheckerPiece.ColIndex + 'A' + 2);
                    positionTo = new string(position);
                }
                else if (i_GameBoard.CheckersBoard[m_CurrentCheckerPiece.RowIndex + 1,
                             m_CurrentCheckerPiece.ColIndex - 1] != 'O' &&
                         i_GameBoard.IsCheckerAvailable((ushort) (m_CurrentCheckerPiece.RowIndex + 2),
                             (ushort) (m_CurrentCheckerPiece.ColIndex - 2)))
                {
                    position[0] = (char)(m_CurrentCheckerPiece.RowIndex + 'a' + 2);
                    position[1] = (char)(m_CurrentCheckerPiece.ColIndex + 'A' - 2);
                    positionTo = new string(position);
                }
            }
            else
            {
                if (i_GameBoard.CheckersBoard[m_CurrentCheckerPiece.RowIndex - 1, m_CurrentCheckerPiece.ColIndex + 1] != 'X' &&
                    i_GameBoard.IsCheckerAvailable((ushort)(m_CurrentCheckerPiece.RowIndex + 2),
                        (ushort)(m_CurrentCheckerPiece.ColIndex + 2)))
                {
                    position[0] = (char)(m_CurrentCheckerPiece.RowIndex + 'a' - 2);
                    position[1] = (char)(m_CurrentCheckerPiece.ColIndex + 'A' + 2);
                    positionTo = new string(position);
                }
                else if (i_GameBoard.CheckersBoard[m_CurrentCheckerPiece.RowIndex - 1,
                             m_CurrentCheckerPiece.ColIndex - 1] != 'O' &&
                         i_GameBoard.IsCheckerAvailable((ushort)(m_CurrentCheckerPiece.RowIndex + 2),
                             (ushort)(m_CurrentCheckerPiece.ColIndex - 2)))
                {
                    position[0] = (char)(m_CurrentCheckerPiece.RowIndex + 'a' - 2);
                    position[1] = (char)(m_CurrentCheckerPiece.ColIndex + 'A' - 2);
                    positionTo = new string(position);
                }
            }

            return positionTo;
        }

        private bool isCheckerFoundAndUpdate(ushort i_RowIndex, ushort i_ColIndex)
        {
            bool isFound = false;

            foreach (CheckersPiece checkerPiece in Pieces)
            {
                if (CaptureUtils.isSamePosition(checkerPiece, i_RowIndex, i_ColIndex))
                {
                    m_CurrentCheckerPiece = checkerPiece;
                    isFound = true;
                }
            }

            return isFound;
        }

        public void MakeCapture(Board i_GameBoard, ref CheckersPiece i_CurrentCheckerPiece, ref string i_PositionTo,
                                ref CheckersPiece i_RivalCheckerPiece)
        {
            CaptureUtils.CaptureRivalCheckerPiece(i_GameBoard, ref i_CurrentCheckerPiece,
                                                  ref i_PositionTo, ref i_RivalCheckerPiece);
            m_CurrentCheckerPiece = i_CurrentCheckerPiece;
        }

        public bool UpdateAndGetMoves(Board i_GameBoard, User i_RivalPlayer)
        {
            bool canCapture = true;

            // If the current checker didn't make a move the turn before.
            if (m_CurrentCheckerPiece == null)
            {
                // If the user can eat, he must! the list will reture as ref value. If not, the list will return the regular moves list.
                if (!CaptureUtils.CanUserCapture(i_GameBoard, this, i_RivalPlayer, ref m_Moves))
                {
                    m_Moves = MoveUtils.CreateRegularMoves(this, i_GameBoard);
                    canCapture = false;
                }
            }
            // If there's a "soldier" that can capture again.
            else
            {
                m_Moves = createCaptureMoveList(i_GameBoard, i_RivalPlayer);
            }

            return canCapture;
        }

        // Certain Tool's Capture Move List:
        private Dictionary<string, List<string>> createCaptureMoveList(Board i_GameBoard, User i_RivalPlayer)
        {
            Dictionary<string, List<string>> captureList = new Dictionary<string, List<string>>();

            if (m_PlayerNumber == ePlayerType.MainPlayer)
            {
                CaptureUtils.CanCaptureUp(i_GameBoard, m_CurrentCheckerPiece, i_RivalPlayer.Pieces, ref captureList);
            }
            else
            {
                CaptureUtils.CanCaptureDown(i_GameBoard, m_CurrentCheckerPiece, i_RivalPlayer.Pieces, ref captureList);
            }

            return captureList;
        }

        // This method probably should be under 'Game'!!
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
