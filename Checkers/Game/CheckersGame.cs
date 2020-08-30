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

            User firstPlayer = new User(name, 'X'); 
            Console.WriteLine("Please enter the size of the Board");
            ushort BoardSize = getValidBoardSize();

            Board gameBoard = new Board(BoardSize);
            char choice = getContestent();

            gameBoard.printBoard();
            if(choice == 1)
            {
                playVsComputer(firstPlayer);
            }
            else
            {
                playVsAnotherPlayer(firstPlayer);
            }
        }

        private static char getContestent()
        {
            Console.WriteLine(@"Please pick the game kind
1. Two players.
2. VS computer.");

            char choice = getValidchoice(Console.ReadLine());

            return choice;
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

        private static void playVsAnotherPlayer(User i_FirstPlayer)
        {
            Console.WriteLine("Please enter second player's name:");
            string name = getValidPlayerName();
            User secondPlayer = new User(name, 'O'); 
            bool hasGameFinished = false;
            bool isFirstPlayerTurn = true;

            while (hasGameFinished)
            {
                if(isFirstPlayerTurn)
                {
                    i_FirstPlayer.MakeMove(ref hasGameFinished);
                }
                else
                {
                    secondPlayer.MakeMove(ref hasGameFinished);
                }
            }

            printResult(i_FirstPlayer, secondPlayer);
        }

        private static void playVsComputer(User i_FirstPlayer)
        {
            bool hasGameFinished = false;
            bool isFirstPlayerTurn = true;
            User computerPlayer = new User("computer", 'O');

            while (hasGameFinished)
            {
                if (isFirstPlayerTurn)
                {
                    hasGameFinished = i_FirstPlayer.MakeMove(ref hasGameFinished);
                }
                else
                {
                   computerPlayer.MakeRandomMove(ref hasGameFinished);
                }
            }

            printResult(i_FirstPlayer, computerPlayer);
        }

        private static void printResult(User i_firstPlayer, User i_AnotherPlayer)
        {
            string winner = i_AnotherPlayer.Name;
            int firstPlayerScore = i_firstPlayer.Score;
            int anotherPlayerScore = i_AnotherPlayer.Score;

            if(firstPlayerScore < anotherPlayerScore)
            {
                winner = i_AnotherPlayer.Name;
            }
            string ScoreMessage = String.Format(@"{0}'s score is: {1} 
{2}'s score is: {3}
the winner is : {4}",
i_firstPlayer.Name,
i_firstPlayer.Score,
i_AnotherPlayer.Name,
i_AnotherPlayer.Score,
winner);
            Console.WriteLine(ScoreMessage);
        }
    }
}
