using System;
using Player;
using CheckersBoard;
using UI;
namespace Game
{
    public class CheckersGame
    {
        public static void  runGame()
        {
            string name = UserIntterface.GetValidUserName();
            User firstPlayer = new User(name, 'X'); 
            ushort BoardSize = UserIntterface.GetValidBoardSize();
            Board gameBoard = new Board(BoardSize);
            char choice = UserIntterface.GetRival();

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

        private static void playVsAnotherPlayer(User i_FirstPlayer)
        {
            string name = UserIntterface.GetValidUserName();
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
