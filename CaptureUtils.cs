using System.Collections.Generic;
using CheckerPiece;
using CheckersBoard;

namespace Player
{
    public struct CaptureUtils
    {
        public static bool CanUserCapture(Board i_GameBoard, User i_CurrentPlayer, User i_RivalPlayer,
                                          ref Dictionary<string, List<string>> io_CapturePositions)
        {
            int i = 0;
            bool canCapture = false;

            foreach (CheckersPiece checkerPiece in i_CurrentPlayer.Pieces)
            {
                if (i_CurrentPlayer.PlayerNumber == User.ePlayerType.MainPlayer)
                {
                    canCapture = CanCaptureUp(i_GameBoard, checkerPiece, i_RivalPlayer.Pieces, ref io_CapturePositions);
                }
                else
                {
                    canCapture = CanCaptureDown(i_GameBoard, checkerPiece, i_RivalPlayer.Pieces, ref io_CapturePositions);
                }
            }

            return canCapture;
        }

        public static bool CanCaptureUp(Board i_GameBoard, CheckersPiece i_Current, CheckersPiece[] i_RivalCheckersPiece,
                                        ref Dictionary<string, List<string>> io_CapturePositions)
        {
            bool canCapture;
            ushort rowIndex, colIndex;
            ushort newRowIndex, newColIndex;
            CheckersPiece rivalCheckerPieceUpRight, rivalCheckerPieceUpLeft;

            // Check if can capture up-right rival.
            rowIndex = (ushort)(i_Current.RowIndex - 1);
            colIndex = (ushort)(i_Current.ColIndex + 1);
            rivalCheckerPieceUpRight = FindCheckerPiece(rowIndex, colIndex, i_RivalCheckersPiece);
            canCapture = TryInsertCapturePosition(
                i_GameBoard, i_Current,
                (ushort)(i_Current.RowIndex - 2), (ushort)(i_Current.ColIndex + 2),
                rivalCheckerPieceUpRight, ref io_CapturePositions);

            // Check if can capture up-left rival.
            rowIndex = (ushort)(i_Current.RowIndex - 1);
            colIndex = (ushort)(i_Current.ColIndex - 1);
            rivalCheckerPieceUpLeft = FindCheckerPiece(rowIndex, colIndex, i_RivalCheckersPiece);
            canCapture = TryInsertCapturePosition(
                i_GameBoard, i_Current,
                (ushort)(i_Current.RowIndex - 2), (ushort)(i_Current.ColIndex - 2),
                rivalCheckerPieceUpLeft, ref io_CapturePositions);

            return canCapture;
        }

        public static bool CanCaptureDown(Board i_GameBoard, CheckersPiece i_Current, CheckersPiece[] i_RivalCheckersPiece,
                                          ref Dictionary<string, List<string>> io_CapturePositions)
        {
            bool canCapture;
            ushort rowIndex, colIndex;
            ushort newRowIndex, newColIndex;
            CheckersPiece rivalCheckerPieceDownRight, rivalCheckerPieceDownLeft;

            // Check if can capture down-right rival.
            rowIndex = (ushort)(i_Current.RowIndex + 1);
            colIndex = (ushort)(i_Current.ColIndex + 1);
            rivalCheckerPieceDownRight = FindCheckerPiece(rowIndex, colIndex, i_RivalCheckersPiece);
            canCapture = TryInsertCapturePosition(
                i_GameBoard, i_Current,
                (ushort)(i_Current.RowIndex + 2), (ushort)(i_Current.ColIndex + 2),
                rivalCheckerPieceDownRight, ref io_CapturePositions);

            // Check if can capture down-left rival.
            rowIndex = (ushort)(i_Current.RowIndex + 1);
            colIndex = (ushort)(i_Current.ColIndex - 1);
            rivalCheckerPieceDownLeft = FindCheckerPiece(rowIndex, colIndex, i_RivalCheckersPiece);
            canCapture = TryInsertCapturePosition(
                i_GameBoard, i_Current,
                (ushort)(i_Current.RowIndex + 2), (ushort)(i_Current.ColIndex - 2),
                rivalCheckerPieceDownLeft, ref io_CapturePositions);

            return canCapture;
        }

        private static bool TryInsertCapturePosition(
            Board i_GameBoard, CheckersPiece i_CurrentChecker,
            ushort i_RowIndex, ushort i_ColIndex,
            CheckersPiece i_RivalChecker, ref Dictionary<string, List<string>> io_CapturePositions)
        {
            bool canInsert = false;

            if (i_RivalChecker != null && isAvailableCaptureCell(i_GameBoard, i_RowIndex, i_ColIndex))
            {
                string captureIndex = MoveUtils.GetStringIndexes(i_RowIndex, i_ColIndex);
                MoveUtils.addToDict(ref io_CapturePositions, i_CurrentChecker, captureIndex);
                canInsert = true;
            }

            return canInsert;
        }

        private static bool isAvailableCaptureCell(Board i_GameBoard, ushort i_RowIndex, ushort i_ColIndex)
        {
            return i_GameBoard.IsCheckerAvailable(i_RowIndex, i_ColIndex);
        }


        public static CheckersPiece FindCheckerPiece(ushort i_RowIndex, ushort i_ColIndex, CheckersPiece[] i_RivalChckersPiece)
        {
            CheckersPiece currentCheckerPiece = null;

            foreach (CheckersPiece piece in i_RivalChckersPiece)
            {
                if (isSamePosition(piece, i_RowIndex, i_ColIndex))
                {
                    currentCheckerPiece = piece;
                    break;
                }
            }

            return currentCheckerPiece;
        }

        public static bool isSamePosition(CheckersPiece i_ChckerPiece, ushort i_RowIndex, ushort i_ColIndex)
        {
            return i_ChckerPiece.ColIndex == i_ColIndex && i_ChckerPiece.RowIndex == i_RowIndex;
        }

        public static void CaptureRivalCheckerPiece(Board i_GameBoard, ref CheckersPiece io_CurrentCheckerPiece, ref string io_PositionTo,
                                                    ref CheckersPiece io_RivalCheckerPiece)
        {
            ushort nextColIndex;
            ushort nextRowIndex = i_GameBoard.GetIndexInBoard(ref io_PositionTo, out nextColIndex);

            // Update current checker position.
            io_CurrentCheckerPiece.ChangePosition(nextRowIndex, nextColIndex);
            // Update rival's checker status (dead).
            io_RivalCheckerPiece.Die();
            // Update board after eating, and move the current checker to his next place.
            i_GameBoard.UpdateAfterEating(io_CurrentCheckerPiece.RowIndex, io_CurrentCheckerPiece.ColIndex,
                nextRowIndex, nextColIndex,
                io_RivalCheckerPiece.RowIndex, io_RivalCheckerPiece.ColIndex);
        }
    }
}
