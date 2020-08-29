using System;
using Player;
using CheckersBoard; 
namespace Game
{
    public class CheckersGame
    {
        public static void  runGame()
        {  
            Console.WriteLine("Please enter your name:");
            string name = getValidPlayerName();

            Console.WriteLine("Please enter the size of the Board");
            ushort BoardSize = getValidBoardSize();

            Board gameBoard = new Board(BoardSize);

            Console.WriteLine(@"Please pick the game kind
1. Two players.
2. VS computer.");
          char choice = getValidchoice(Console.ReadLine());
            gameBoard.printBoard();
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

        private static bool IsValidUserName(string nameOfPlayer)
        {
            bool isValidName = nameOfPlayer.Length >20 && nameOfPlayer.Length > 0;

            if(isValidName)
            {
                for (int i = 0; i < nameOfPlayer.Length; i++)
                {
                    isValidName = isValidName && char.IsLetter(nameOfPlayer[i]);
                }
            }

            return isValidName;
        }
    }
}
