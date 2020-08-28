using System;
using CheckersBoard;
using CheckerPiece;

namespace Player
{
    class MoveUtils
    {
        // Constants:
        private const short k_RowIndex = 1;
        private const short k_ColIndex = 0;

        public static void MoveRegularTool(User i_CurrentPlayer, User i_RivalPlayer, ref Board i_GameBoard,
                                           CheckersPiece i_CurrentChecker, string i_PositionTo)
        {
            ushort nextRowIndex = Convert.ToUInt16(i_PositionTo[k_RowIndex] - 'A');
            ushort nextColIndex = Convert.ToUInt16(i_PositionTo[k_ColIndex] - 'a');

            CheckersPiece? rivalCheckerPiece = findRivalPieceExist(i_RivalPlayer, i_CurrentChecker);

            if (rivalCheckerPiece == null)
            {
                // If there is not(!) a rival checker piece in the way.
                // Check valid move - include: inborder, valid input, is empty cell.
                Validation.CheckValidMoveRegularTool(i_GameBoard, i_CurrentPlayer, i_CurrentChecker, ref i_PositionTo);
                
                // Update board - new tool position.
                i_GameBoard.UpdateBoardAccordingToPlayersMove(
                    i_CurrentChecker.RowIndex, 
                    i_CurrentChecker.ColIndex,
                    nextRowIndex,
                    nextColIndex);
            }
            else
            {
                // The current player must eat.
                eatRivalCheckerPiece(rivalCheckerPiece.Value, i_GameBoard, i_CurrentChecker, ref i_PositionTo.Length, i_CurrentPlayer.Name);
            }
        }

        // This method's params are rival user and the current player's checker piece.
        // Checks and finds if the rival player's checker piece is exist in the way.
        private static CheckersPiece? findRivalPieceExist(User i_RivalPlayer, CheckersPiece i_CurrentChecker)
        {
            CheckersPiece? rivalCheckerPiece = null;

            foreach (var checkerPiece in i_RivalPlayer.Pieces)
            {
                if (isRivalPiecePositionUp(checkerPiece, i_CurrentChecker) ||
                    isRivalPiecePositionDown(checkerPiece, i_CurrentChecker)
                {
                    rivalCheckerPiece = checkerPiece;
                }
            }

            return rivalCheckerPiece;
        }

        private static bool isRivalPiecePositionUp(CheckersPiece i_CurrentCheckerPiece, CheckersPiece i_RivalCheckerPiece)
        {
            // Checks if the rival's checker is in a suitable position.
            // This method is for the player that move "up".
            return (i_CurrentCheckerPiece.ColIndex + 1 == i_RivalCheckerPiece.ColIndex &&
                    i_CurrentCheckerPiece.RowIndex - 1 == i_RivalCheckerPiece.RowIndex) ||
                   (i_CurrentCheckerPiece.ColIndex - 1 == i_RivalCheckerPiece.ColIndex &&
                    i_CurrentCheckerPiece.RowIndex - 1 == i_RivalCheckerPiece.RowIndex);
        }

        private static bool isRivalPiecePositionDown(CheckersPiece i_CurrentCheckerPiece, CheckersPiece i_RivalCheckerPiece)
        {
            // Checks if the rival's checker is in a suitable position.
            // This method is for the player that move "down".
            return (i_CurrentCheckerPiece.ColIndex + 1 == i_RivalCheckerPiece.ColIndex &&
                    i_CurrentCheckerPiece.RowIndex + 1 == i_RivalCheckerPiece.RowIndex) ||
                   (i_CurrentCheckerPiece.ColIndex - 1 == i_RivalCheckerPiece.ColIndex &&
                    i_CurrentCheckerPiece.RowIndex + 1 == i_RivalCheckerPiece.RowIndex);
        }

        private static void eatRivalCheckerPiece(CheckersPiece i_RivalCheckerPiece, Board i_GameBoard,
                                                 CheckersPiece i_CurrentCheckerPiece, ref string i_PositionTo, string i_PlayerName)
        {
            while (!isRivalPosition(i_RivalCheckerPiece, i_PositionTo))
            {
                Console.WriteLine("You must eat the rival's checker piece!");
                Validation.UserTurnConversation(i_PlayerName, ref i_PositionTo);
            }
            // Update current checker position.
            // Update rival's checker statud (dead).
            // i_GameBoard.updateAfterEating();
            // Change position of the current checker's player.
        }

        private static bool isRivalPosition(CheckersPiece i_RivalCheckerPiece, string i_PositionTo)
        {
            return i_RivalCheckerPiece.RowIndex == i_PositionTo[k_RowIndex] &&
                   i_RivalCheckerPiece.ColIndex == i_PositionTo[k_ColIndex];
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
    }
}
