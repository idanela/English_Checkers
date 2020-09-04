namespace Validation
{
    public class Validate
    {
        private const short k_StartPositionTo = 0;
        private const short k_StartPositionFrom = 3;
        private const short k_SubStringLength = 2;

        public static bool IsValidMove(string i_MoveToPreform, ushort i_BoardSize)
        {
            return i_MoveToPreform == "Q" || isLegalMove(i_MoveToPreform, i_BoardSize);
        }

        private static bool isLegalMove(string i_MoveToPreform, ushort i_BoardSize)
        {
            return isLegalMovePattern(i_MoveToPreform) && isValidBoardMove(i_MoveToPreform, i_BoardSize);
        }

        private static bool isLegalMovePattern(string i_MoveToPreform)
        {
            return i_MoveToPreform.Length == 5 &&
            char.IsUpper(i_MoveToPreform[0]) &&
            char.IsLower(i_MoveToPreform[1]) &&
            i_MoveToPreform[2] == '>' &&
            char.IsUpper(i_MoveToPreform[3]) &&
            char.IsLower(i_MoveToPreform[4]);
        }

        private static bool isValidBoardMove(string i_MoveToPreform, ushort i_BoardSize)
        {
            string location = string.Empty;
            string destination = string.Empty;
            ParsePositions(i_MoveToPreform, ref location, ref destination);

            return checkIndexesBounderies(i_BoardSize, location, destination);
        }

        public static void ParsePositions(string i_StrInput, ref string io_PositionFrom, ref string io_PositionTo)
        {
            io_PositionTo = i_StrInput.Substring(k_StartPositionFrom, k_SubStringLength);
            io_PositionFrom = i_StrInput.Substring(k_StartPositionTo, k_SubStringLength);
        }

        private static bool checkIndexesBounderies(ushort i_BoardSize, string i_Location, string i_Destination)
        {
            ushort colIndex = (ushort)(i_Location[0] - 'A');
            ushort rowIndex = (ushort)(i_Location[1] - 'a');
            ushort destinationRowIndex = (ushort)(i_Destination[0] - 'A');
            ushort destinationColIndex = (ushort)(i_Destination[1] - 'a');
            bool isValidIndexesMove = colIndex >= 0 && colIndex < i_BoardSize && rowIndex >= 0 && rowIndex < i_BoardSize
                && destinationRowIndex >= 0 && destinationRowIndex < i_BoardSize && destinationRowIndex >= 0 && destinationRowIndex < i_BoardSize;

            return isValidIndexesMove;
        }
    }
}
