using System;
using System.Collections.Generic;
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
        private const char k_MoveUp = 'X';
        private const char k_MoveDown = 'O';
        private const char k_MoveUpKing = 'K';
        private const char k_MoveDownKing = 'U';

        // Data members:
        private readonly string m_Name;
        private ushort m_Score;
        private CheckersPiece[] m_CheckersPiece;
        private CheckersPiece.ePieceKind m_CheckerPieceKind;
        private readonly ePlayerType m_PlayerNumber;
        private Dictionary<string, List<string>> m_Moves;
        private CheckersPiece m_CurrentCheckerPiece;
        private readonly bool m_IsComputer;

        // Enums:
        public enum ePlayerType
        {
            MainPlayer = 0,
            RivalPlayer = 1
        }


        // Constructors:
        public User(string i_Name, ePlayerType i_PlayerNumber, bool i_IsComputer)
        {
            m_Name = i_Name;
            m_Score = 0;
            m_CheckersPiece = null;
            m_PlayerNumber = i_PlayerNumber;
            m_CheckerPieceKind = i_PlayerNumber == ePlayerType.MainPlayer ?
                CheckersPiece.ePieceKind.MainPlayerTool : CheckersPiece.ePieceKind.SecondPlayerTool;
            m_Moves = null;
            m_CurrentCheckerPiece = null;
            m_IsComputer = i_IsComputer;
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

        public bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }
        }


        // Methods:
        public void InitializeCheckersArray(int i_BoardSize)
        {
            int sizeOfPieces = ((i_BoardSize / 2) * 3);

            m_CheckersPiece = new CheckersPiece[sizeOfPieces];
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

        public void MakeToolAKing(Board i_GameBoard, ref CheckersPiece io_CurrentCheckerPiece)
        {
            if (PlayerNumber == ePlayerType.MainPlayer)
            {
                if (hasReachedFinalRowUp(io_CurrentCheckerPiece.RowIndex))
                {
                    io_CurrentCheckerPiece.BecomeKing();
                }
            }
            else
            {
                if (hasReachedFinalRowDown(i_GameBoard.SizeOfBoard, io_CurrentCheckerPiece.RowIndex))
                {
                    io_CurrentCheckerPiece.BecomeKing();
                }
            }
        }

        private static bool hasReachedFinalRowDown(ushort i_BoardSize, ushort i_CurrentRow)
        {
            return i_CurrentRow == i_BoardSize - 1;
        }

        private static bool hasReachedFinalRowUp(ushort i_CurrentRow)
        {
            return i_CurrentRow == 0;
        }

        public void MakeMove(ref Board io_GameBoard, User i_RivalPlayer, string i_PositionFrom, string i_PositionTo)
        {
            CheckersPiece checkerPieceToMove = null;
            CheckersPiece rivalCheckerPiece = null;

            if (GetMovesAndUpdate(io_GameBoard, i_RivalPlayer))
            {
                playerMustCapture(io_GameBoard, i_PositionFrom, i_PositionTo,
                    ref checkerPieceToMove, i_RivalPlayer.Pieces, ref rivalCheckerPiece);
                // Checks if there's an optional capture, and updating the data structure.
                MakeCapture(io_GameBoard, ref checkerPieceToMove, ref i_PositionTo, ref rivalCheckerPiece);
            }
            else
            {
                playerMustMoveValid(io_GameBoard, i_PositionFrom, i_PositionTo, ref checkerPieceToMove);
                if (!IsComputer)
                {
                    MakeUserMove(ref io_GameBoard, ref checkerPieceToMove, i_PositionFrom, i_PositionTo);
                }
                else
                {
                    MakeComputerMove(ref io_GameBoard, ref checkerPieceToMove);
                }
            }
        }

        private void playerMustCapture(Board i_GameBoard, string i_PositionFrom, string i_PositionTo,
            ref CheckersPiece io_CurrentChecker, CheckersPiece[] i_RivalPieces, ref CheckersPiece io_RivalChecker)
        {
            while (!isValidCapture(i_GameBoard, i_PositionFrom, i_PositionTo, ref io_CurrentChecker, i_RivalPieces,
                ref io_RivalChecker))
            {
                Console.WriteLine("Invalid move. player must capture.");
                Validation.UserTurnConversation(Name, ref i_PositionFrom, ref i_PositionTo);
                Validation.ParsePositions(Console.ReadLine(), ref i_PositionFrom, ref i_PositionTo);
            }
        }

        private void playerMustMoveValid(Board i_GameBoard, string i_PositionFrom, string i_PositionTo, ref CheckersPiece io_CurrentChecker)
        {
            while (!isValidMove(i_GameBoard, i_PositionFrom, i_PositionTo, ref io_CurrentChecker))
            {
                Console.WriteLine("Invalid move. player must move with to free positions.");
                Validation.UserTurnConversation(Name, ref i_PositionFrom, ref i_PositionTo);
                Validation.ParsePositions(Console.ReadLine(), ref i_PositionFrom, ref i_PositionTo);
            }
        }

        private bool isValidCapture(Board i_GameBoard, string i_PositionFrom, string i_PositionTo,
                                 ref CheckersPiece io_CurrentChecker, CheckersPiece[] i_RivalPieces, ref CheckersPiece io_RivalChecker)
        {
            bool isValid = false;
            ushort rowIndex, colIndex;

            foreach (string positionFrom in Moves.Keys)
            {
                if (i_PositionFrom == positionFrom)
                {
                    if (isPositionInList(i_PositionTo, Moves[positionFrom]))
                    {
                        colIndex = i_GameBoard.GetIndexInBoard(ref i_PositionFrom, out rowIndex);
                        io_CurrentChecker = CaptureUtils.FindCheckerPiece(rowIndex, colIndex, Pieces);

                        colIndex = (ushort) (i_PositionTo[k_ColIndex] - i_PositionFrom[k_RowIndex] + 1);
                        rowIndex = (ushort)(i_PositionTo[k_ColIndex] - i_PositionFrom[k_RowIndex] + 1);
                        io_RivalChecker = CaptureUtils.FindCheckerPiece(rowIndex, colIndex, i_RivalPieces);

                        isValid = true;
                        break;
                    }
                }
            }

            return isValid;
        }

        private bool isValidMove(Board i_GameBoard, string i_PositionFrom, string i_PositionTo, ref CheckersPiece io_CurrentChecker)
        {
            bool isValid = false;
            ushort rowIndex, colIndex;

            foreach (string positionFrom in Moves.Keys)
            {
                if (i_PositionFrom == positionFrom)
                {
                    if (isPositionInList(i_PositionTo, Moves[positionFrom]))
                    {
                        colIndex = i_GameBoard.GetIndexInBoard(ref i_PositionFrom, out rowIndex);
                        io_CurrentChecker = CaptureUtils.FindCheckerPiece(rowIndex, colIndex, Pieces);

                        isValid = true;
                        break;
                    }
                }
            }

            return isValid;
        }

        private bool isPositionInList(string i_PositionTo, List<string> i_Positions)
        {
            bool isFound = false;

            foreach (string positionsTo in i_Positions)
            {
                if (i_PositionTo == positionsTo)
                {
                    isFound = true;
                    break;
                }
            }

            return isFound;
        }

        public void MakeUserMove(ref Board io_GameBoard, ref CheckersPiece io_CurrentChecker, string i_PositionFrom, string i_PositionTo)
        {
            ushort rowIndex;
            ushort colIndex = io_GameBoard.GetIndexInBoard(ref i_PositionFrom, out rowIndex);

            updateCurrentCheckerPiece(rowIndex, colIndex);
            if (!m_CurrentCheckerPiece.IsKing)
            {
                MoveUtils.MoveRegularTool(this, ref io_GameBoard, ref io_CurrentChecker, i_PositionFrom, i_PositionTo);
                MakeToolAKing(io_GameBoard, ref io_CurrentChecker);
            }
            else
            {
                MoveUtils.MoveKingTool(this, ref io_GameBoard, ref io_CurrentChecker, i_PositionFrom, i_PositionTo);
            }
        }

        public void MakeComputerMove(ref Board io_GameBoard, ref CheckersPiece io_CurrentChecker)
        {
            string[] positions = getRandomMove(io_GameBoard);

            if (!m_CurrentCheckerPiece.IsKing)
            {
                MoveUtils.MoveRegularTool(
                    this,
                    ref io_GameBoard,
                    ref io_CurrentChecker,
                    positions[0],
                    positions[1]);
                MakeToolAKing(io_GameBoard, ref io_CurrentChecker);
            }
            else
            {
                MoveUtils.MoveKingTool(
                    this, 
                    ref io_GameBoard,
                    ref io_CurrentChecker,
                    positions[0],
                    positions[1]);
            }
        }

        private string[] getRandomMove(Board i_GameBoard)
        {
            char rowIndex = (char)(new Random().Next(i_GameBoard.SizeOfBoard - 1) + 'a');
            char colIndex = (char)(new Random().Next(i_GameBoard.SizeOfBoard - 1) + 'A');
            string[] positions = new string[2];
            positions[0] = new string(colIndex, rowIndex);

            while (!isCheckerFoundAndUpdate(i_GameBoard, positions[0], out positions[1]))
            {
                rowIndex = (char)(new Random().Next(i_GameBoard.SizeOfBoard - 1) + 'a');
                colIndex = (char)(new Random().Next(i_GameBoard.SizeOfBoard - 1) + 'A');
                positions[0] = new string(colIndex, rowIndex);
            }

            return positions;
        }

        private bool isCheckerFoundAndUpdate(Board i_GameBoard, string i_PositionFrom, out string o_PositionTo)
        {
            bool isFound = false;
            o_PositionTo = null;

            foreach (string checkerPosition in m_Moves.Keys)
            {
                if (checkerPosition == i_PositionFrom)
                {
                    ushort rowIndex;
                    ushort colIndex = i_GameBoard.GetIndexInBoard(ref i_PositionFrom, out rowIndex);
                    updateCurrentCheckerPiece(rowIndex, colIndex);
                    o_PositionTo = m_Moves[checkerPosition][0]; // For now return the first available position to go to.
                    isFound = true;
                    break;
                }
            }

            return isFound;
        }

        private void updateCurrentCheckerPiece(ushort i_RowIndex, ushort i_ColIndex)
        {
            CheckersPiece checkerPieceToMove = null;

            foreach (CheckersPiece checkerPiece in m_CheckersPiece)
            {
                if (CaptureUtils.isSamePosition(checkerPiece, i_RowIndex, i_ColIndex))
                {
                    m_CurrentCheckerPiece = checkerPiece;
                    break;
                }
            }
        }

        public void MakeCapture(Board i_GameBoard, ref CheckersPiece io_CurrentCheckerPiece, ref string io_PositionTo,
                                ref CheckersPiece io_RivalCheckerPiece)
        {
            CaptureUtils.CaptureRivalCheckerPiece(i_GameBoard, ref io_CurrentCheckerPiece,
                                                  ref io_PositionTo, ref io_RivalCheckerPiece);
            m_CurrentCheckerPiece = io_CurrentCheckerPiece;
        }

        public bool GetMovesAndUpdate(Board i_GameBoard, User i_RivalPlayer)
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
                // If there's capture in a row.
                m_Moves = createCaptureMoveList(i_GameBoard, i_RivalPlayer);
                // Check if there are no more captures to do in a row with the current checker.
                if (Moves.Count == 0)
                {
                    m_CurrentCheckerPiece = null;
                }
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

        private void MakeComputerBestMove(Board i_GameBoard, User i_RivalPlayer)
        {
            CheckersPiece currentCheckersPiece = null;
            CheckersPiece rivalCheckersPiece = null;

            // If the computer has the option to capture.
            if (GetMovesAndUpdate(i_GameBoard, i_RivalPlayer))
            {
                // currentCheckersPiece
                // MakeCapture
            }
            else
            {
                // MakeMove
            }
        }
    }
}
