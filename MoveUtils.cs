using System;

namespace Player
{
    class MoveUtils
    {
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

            if (Validation.IsValidPosition(i_PositionTo))
            {
                // Check if the cell is empty.
            }
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

        //public static bool IsEmptyCell(ref Board i_GameBoard, string i_PotisionTo)
        //{
        //    return i_GameBoard.[i_PotisionTo[0] - 'A', i_PotisionTo[1] - 'a'] == k_Empty;
        //}

        private static bool isMoveInBorders(string i_PositionTo)
        {
            // Check if the given position is in the borders.
            // Means the index is less(!) than border's size.

            return false;   // Temporary return value.
        }
    }
}
