using System;
using Player;
using CheckersBoard;
using UI;

namespace Game
{
    public class CheckersGame
    {
        public static void runGame()
        {
            string firstPlayerName = UserIntterface.GetValidUserName();
            User firstPlayer = new User(firstPlayerName, 'X'); 
            ushort boardSize = UserIntterface.GetValidBoardSize();
            Board gameBoard = new Board(boardSize);
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

        private static void playVsAnotherPlayer(User i_FirstPlayer, Board i_GameBoard)
        {
            string name = UserIntterface.GetValidUserName();
            User secondPlayer = new User(name, 'O');

            playGame(i_FirstPlayer, secondPlayer, i_GameBoard);
        }

        private static void playVsComputer(User i_FirstPlayer, Board i_GameBoard)
        {      
            User computerPlayer = new User("Computer", 'O');

            playGame(i_FirstPlayer, computerPlayer, i_GameBoard);
            printResult(i_FirstPlayer, computerPlayer);
        }

        private static void playGame(User i_FirstPlayer, User i_SecondPlayer, Board i_GameBoard)
        {
            i_GameBoard.InitializeBoard();
            bool hasGameFinished = false;
            bool isFirstPlayerTurn = true;

            while (hasGameFinished)
            {
                if (isFirstPlayerTurn)
                {
                    i_FirstPlayer.MakeMove(ref hasGameFinished);
                }
                else
                {
                    i_SecondPlayer.MakeMove(ref hasGameFinished);
                }
            }

            printResult(i_FirstPlayer, secondPlayer);
           if(UserIntterface.WouldLikeToPlayAgain())
            { 
                playGame(i_FirstPlayer, i_SecondPlayer, i_GameBoard);
            }
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

            string ScoreMessage = string.Format(
                @"
{0}'s score is: {1} 
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
