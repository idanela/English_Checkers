using System;

namespace Player
{
    public struct Player
    {
        // Data members:
        private string m_Name;
        private ushort m_Score;
        // typeOf tools = X\O (eTypeOfTools.FirstPlayer)

        // Properties:
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
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

        // Constructors:
        public Player(string i_Name)
        {
            m_Name = i_Name;
            m_Score = 0;
        }

        // Methods:

        public static void Play()
        {
            // This method called to play the user's move.
        }

        public static void Quit(string i_QuitInput)
        {
            // Need to quit the game if the user press q(?).
        }

        public static void MoveRegularTool(string i_Position)
        {
            // Check valid move - include: inborder, valid input, is empty cell.
            // Update board - new tool position.
            // Check if can eat.
        }

        private static void checkValidMove(string i_PositionTo)
        {
            // Check if the position is in border,
            // is empty cell available in the given position.
            // Check valid indexes: A-H, a-h.
        }

        public static void MoveKingTool(string i_Position)
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

        private static bool isMoveInBorders(string i_PositionTo)
        {
            // Check if the given position is in the borders.
            // Means the index is less(!) than border's size.

            return false;   // Temporary return value.
        }

        private static bool isValidInput(string i_PositionTo)
        {
            // Check if the givn input is A-H, a-h (for example).

            return false;   // Temporary return value.
        }
    }
}
