using System;
using System.Collections.Generic;
using System.Linq;
using CheckersBoard;
using CheckerPiece;

namespace Player
{
    struct MoveUtils
    {
        // Constants:
        private const short k_RowIndex = 1;
        private const short k_ColIndex = 0;

        // Regular Move List:
        public static Dictionary<string, List<string>> CreateRegularMoves(User i_CurrentPlayer, Board i_GameBoard)
        {
            Dictionary<string, List<string>> optionsToMove = new Dictionary<string, List<string>>();

            foreach (CheckersPiece checkerPiece in i_CurrentPlayer.Pieces)
            {
                string checkerPiecePosition = GetStringIndexes(checkerPiece.RowIndex, checkerPiece.ColIndex);

                if (i_CurrentPlayer.PlayerNumber == User.ePlayerType.MainPlayer)
                {
                    getUpCellsPosition(ref optionsToMove, i_GameBoard, checkerPiece);
                }
                else
                {
                    getDownCellsPosition(ref optionsToMove, i_GameBoard, checkerPiece);
                }
            }

            return optionsToMove;
        }

        private static void getUpCellsPosition(ref Dictionary<string, List<string>> io_OptionsMove, Board i_GameBoard, CheckersPiece i_CurrentCheckerPiece)
        {
            ushort newRowIndex = (ushort)(i_CurrentCheckerPiece.RowIndex - 1);
            ushort newColRightIndex = (ushort)(i_CurrentCheckerPiece.ColIndex + 1);
            ushort newColLeftIndex = (ushort)(i_CurrentCheckerPiece.ColIndex - 1);
            string positionRightStr = GetStringIndexes(newRowIndex, newColRightIndex);
            string positionLeftStr = GetStringIndexes(newRowIndex, newColLeftIndex);

            if (isAvailableCellUpRightWay(i_GameBoard, i_CurrentCheckerPiece))
            {
                addToDict(ref io_OptionsMove, i_CurrentCheckerPiece, positionRightStr);
            }

            if (isAvailableCellUpLeftWay(i_GameBoard, i_CurrentCheckerPiece))
            {
                addToDict(ref io_OptionsMove, i_CurrentCheckerPiece, positionLeftStr);
            }
        }

        private static bool isAvailableCellUpRightWay(Board i_GameBoard, CheckersPiece i_CurrentCheckerPiece)
        {
            ushort newRowIndex = (ushort)(i_CurrentCheckerPiece.RowIndex - 1);
            ushort newColRightIndex = (ushort)(i_CurrentCheckerPiece.ColIndex + 1);

            return i_GameBoard.IsCheckerAvailable(newRowIndex, newColRightIndex);
        }

        private static bool isAvailableCellUpLeftWay(Board i_GameBoard, CheckersPiece i_CurrentCheckerPiece)
        {
            ushort newRowIndex = (ushort)(i_CurrentCheckerPiece.RowIndex - 1);
            ushort newColLeftIndex = (ushort)(i_CurrentCheckerPiece.ColIndex - 1);

            return i_GameBoard.IsCheckerAvailable(newRowIndex, newColLeftIndex);
        }

        private static void getDownCellsPosition(ref Dictionary<string, List<string>> io_OptionsMove, Board i_GameBoard, CheckersPiece i_CurrentCheckerPiece)
        {
            ushort newRowIndex = (ushort)(i_CurrentCheckerPiece.RowIndex + 1);
            ushort newColRightIndex = (ushort)(i_CurrentCheckerPiece.ColIndex + 1);
            ushort newColLeftIndex = (ushort)(i_CurrentCheckerPiece.ColIndex - 1);
            string positionRightStr = GetStringIndexes(newRowIndex, newColRightIndex);
            string positionLeftStr = GetStringIndexes(newRowIndex, newColLeftIndex);

            if (isAvailableCellDownRightWay(i_GameBoard, i_CurrentCheckerPiece))
            {
                addToDict(ref io_OptionsMove, i_CurrentCheckerPiece , positionRightStr);
            }

            if (isAvailableCellDownLeftWay(i_GameBoard, i_CurrentCheckerPiece))
            {
                addToDict(ref io_OptionsMove, i_CurrentCheckerPiece, positionLeftStr);
            }
        }

        private static bool isAvailableCellDownRightWay(Board i_GameBoard, CheckersPiece i_CurrentCheckerPiece)
        {
            ushort newRowIndex = (ushort)(i_CurrentCheckerPiece.RowIndex + 1);
            ushort newColRightIndex = (ushort)(i_CurrentCheckerPiece.ColIndex + 1);

            return i_GameBoard.IsCheckerAvailable(newRowIndex, newColRightIndex);
        }

        private static bool isAvailableCellDownLeftWay(Board i_GameBoard, CheckersPiece i_CurrentCheckerPiece)
        {
            ushort newRowIndex = (ushort)(i_CurrentCheckerPiece.RowIndex + 1);
            ushort newColLeftIndex = (ushort)(i_CurrentCheckerPiece.ColIndex - 1);

            return i_GameBoard.IsCheckerAvailable(newRowIndex, newColLeftIndex);
        }

        public static void addToDict(ref Dictionary<string, List<string>> i_Options, CheckersPiece i_CurrentChecker, string i_OptionPosition)
        {
            string currentPosition = GetStringIndexes(i_CurrentChecker.RowIndex, i_CurrentChecker.ColIndex);
            i_Options[currentPosition].Add(i_OptionPosition);
        }

        public static string GetStringIndexes(ushort i_RowIndex, ushort i_ColIndex)
        {
            char row = (char)(i_RowIndex + 'a');
            char col = (char)(i_ColIndex + 'A');

            return new string(row, col);
        }


        // Move Tools Methods:
        public static void MoveRegularTool(User i_CurrentPlayer, ref Board i_GameBoard,
                                           ref CheckersPiece i_CurrentChecker, string i_PositionFrom, string i_PositionTo)
        {
            ushort nextRowIndex = (ushort) (i_PositionTo[k_RowIndex] - 'a');
            ushort nextColIndex = (ushort) (i_PositionTo[k_ColIndex] - 'A');
            
            // If there is not(!) a rival checker piece in the way.
            // Check valid move - include: inborder, valid input, is empty cell.
            Validation.CheckValidMoveRegularTool(i_GameBoard, i_CurrentPlayer, i_CurrentChecker, ref i_PositionFrom, ref i_PositionTo);
            
            // Update board - new tool position.
            i_GameBoard.UpdateBoardAccordingToPlayersMove(
                i_CurrentChecker.RowIndex, 
                i_CurrentChecker.ColIndex,
                nextRowIndex,
                nextColIndex);
        }

        public static void MoveKingTool(User i_CurrentPlayer, ref Board i_GameBoard,
                                        ref CheckersPiece i_CurrentChecker, string i_PositionFrom, string i_PositionTo)
        {
            ushort nextRowIndex = (ushort)(i_PositionTo[k_RowIndex] - 'a');
            ushort nextColIndex = (ushort)(i_PositionTo[k_ColIndex] - 'A');

            // If there is not(!) a rival checker piece in the way.
            // Check valid move - include: inborder, valid input, is empty cell.
            Validation.CheckValidMoveKingTool(i_GameBoard, i_CurrentPlayer, i_CurrentChecker, ref i_PositionFrom, ref i_PositionTo);

            // Update board - new tool position.
            i_GameBoard.UpdateBoardAccordingToPlayersMove(
                i_CurrentChecker.RowIndex,
                i_CurrentChecker.ColIndex,
                nextRowIndex,
                nextColIndex);
        }
    }
}
