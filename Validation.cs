using System;
using System.Runtime.InteropServices;
using CheckersPiece;

namespace Player
{
    public struct Validation
    {
        public static void checkValidInput(string i_GivenPosition)
        {
            while (!isValidInput(i_GivenPosition))
            {

            }
        }

        public static bool isValidInput(string i_CharSequence)
        {
            // Check if the given input is A-H, a-h (for example).

            return false;   // Temporary return value.
        }

        public static bool IsValidUpperCase(char i_CharIndex)
        {
            return i_CharIndex >= 'A'; // && i_CharIndex <= 'A' + BoardSize - 1;
        }

        public static bool IsValidLowerCase(char i_CharIndex)
        {
            return i_CharIndex >= 'a'; // && i_CharIndex <= 'a' + BoardSize - 1;
        }

        public static bool IsValidPosition(string i_Position)
        {
            int rowIndex = i_Position[0] - 'A';
            int colIndex = i_Position[1] - 'a';

            return IsValidIndex(rowIndex) && IsValidIndex(colIndex);
        }

        public static bool IsValidIndex(int i_CharIndex)
        {
            return i_CharIndex >= 0; // && i_CharIndex <= 'A' + BoardSize - 1;
        }
    }
}
