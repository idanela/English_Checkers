using System;
using System.Linq;
using System.Runtime.InteropServices;
using CheckerPiece;
using CheckersBoard;

namespace Player
{
    public struct Validation
    {
        // Constants:
        private const int k_StartPositionTo = 0;
        private const int k_StartPositionFrom = 3;
        private const int k_SubStringLength = 2;

        public static void CheckValidInput(string i_PlayerName, ref string io_PositionFrom, ref string io_PositionTo)
        {
            string positionInput = null;

            while (!IsValidInput(io_PositionFrom) || !IsValidInput(io_PositionTo))
            {
                Console.WriteLine("Please enter valid indexes (for example: 'Aa').");
                Console.Write(i_PlayerName + "'s turn: ");
                positionInput = Console.ReadLine();
            }

            while (!IsValidPosition(io_PositionFrom) || !IsValidPosition(io_PositionTo))
            {
                Console.WriteLine("Please enter valid position.");
                Console.Write(i_PlayerName + "'s turn: ");
                positionInput = Console.ReadLine();
            }

            ParsePositions(positionInput, ref io_PositionFrom, ref io_PositionTo);
        }

        public static void ParsePositions(string i_StrInput, ref string io_PositionTo, ref string io_PositionFrom)
        {
            io_PositionFrom = i_StrInput.Substring(k_StartPositionFrom, k_SubStringLength);
            io_PositionTo = i_StrInput.Substring(k_StartPositionTo, k_SubStringLength);
        }



        public static bool IsValidInput(string i_CharSequence)
        {
            // Check if the given input is A-H, a-h (for example).

            return isValidUpperCase(i_CharSequence[0]) &&
                   isValidLowerCase(i_CharSequence[1]);
        }

        private static bool isValidUpperCase(char i_Char)
        {
            return i_Char >= 'A'; // && i_CharIndex <= 'A' + BoardSize - 1;
        }

        private static bool isValidLowerCase(char i_Char)
        {
            return i_Char >= 'a'; // && i_CharIndex <= 'a' + BoardSize - 1;
        }

        public static bool IsValidPosition(string i_Position)
        {
            int rowIndex = i_Position[0] - 'A';
            int colIndex = i_Position[1] - 'a';

            return isValidIndex(rowIndex) && isValidIndex(colIndex);
        }

        private static bool isValidIndex(int i_CharIndex)
        {
            return i_CharIndex >= 0; // && i_CharIndex <= BoardSize - 1;
        }
    }
}
