using System;

namespace UI
{
   public class UserIntterface
    {
        public static string GetValidUserName()
        {
            Console.WriteLine("Please enter player's name:");
            return getValidPlayerName();
        }

        private static string getValidPlayerName()
        {
            string playerName = Console.ReadLine();

            while (IsValidUserName(playerName))
            {
                Console.WriteLine("Invalid Input please type your name again.");
                playerName = Console.ReadLine();
            }

            return playerName;
        }

            private static bool IsValidUserName(string nameOfPlayer)
            {
                bool isValidName = nameOfPlayer.Length > 20 && nameOfPlayer.Length > 0;

                if (isValidName)
                {
                    for (int i = 0; i < nameOfPlayer.Length; i++)
                    {
                        isValidName = isValidName && char.IsLetter(nameOfPlayer[i]);
                    }
                }

                return isValidName;
            }

        public static ushort GetValidBoardSize()
        {
            Console.WriteLine("Please enter the size of the Board");
            return getValidBoardSize();
        }

        private static ushort getValidBoardSize()
        {
            ushort sizeOfBoard;
            while (!ushort.TryParse(Console.ReadLine(), out sizeOfBoard) || !CheckersBoard.Board.isValidBoardSize(sizeOfBoard))
            {
                Console.WriteLine("Invalid input please insert size again.");
                ushort.TryParse(Console.ReadLine(), out sizeOfBoard);
            }

            return sizeOfBoard;
        }

        public static char GetRival()
        {
            Console.WriteLine(@"Please pick the game kind
1. Two players.
2. VS computer.");

            return getCompetitor();
        }

        private static char getCompetitor()
        {
            char choice = getValidchoice(Console.ReadLine());

            return choice;
        }

        private static char getValidchoice(string PlayerChoice)
        {
            char choice;
            while (!char.TryParse(PlayerChoice, out choice) && choice != '1' && choice != '2')
            {
                Console.WriteLine("invalid choice. please try again:");
                PlayerChoice = Console.ReadLine();
            }

            return choice;
        }

        public static bool WouldLikeToPlayAgain()
        {
            Console.WriteLine("To play Again press '1' otherwise press '2'.");
            char WouldLikeToPlayAgain = getValidchoice(Console.ReadLine());

            return WouldLikeToPlayAgain == '1';
        }
    }
}
