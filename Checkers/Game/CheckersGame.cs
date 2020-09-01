using System;
using Player;
using CheckersBoard;
using CheckerPiece;
using UI;

namespace Game
{
    public class CheckersGame
    {
        public static void runGame()
        {
            string firstPlayerName = UserIntterface.GetValidUserName();
            User firstPlayer = new User(firstPlayerName, User.ePlayerType.MainPlayer, CheckersPiece.ePieceKind.MainPlayerTool);
            ushort boardSize = UserIntterface.GetValidBoardSize();
            Board gameBoard = new Board(boardSize);
            char choice = UserIntterface.GetRival();
            string rivalName = string.Empty;
            // gameBoard.printBoard();
            if (choice == 1)
            {
                rivalName = UserIntterface.GetValidUserName();
            }
            else
            {
                rivalName = "computer";
            }
            User rivalPlayer = new User(rivalName, User.ePlayerType.RivalPlayer, CheckersPiece.ePieceKind.SecondPlayerMainTool);
            playGame(firstPlayer, rivalPlayer, gameBoard);
        }

        private static void playGame(User i_FirstPlayer, User i_SecondPlayer, Board i_GameBoard)
        {
            i_GameBoard.InitializeBoard();
            i_FirstPlayer.InitializeCheckersArray(i_GameBoard);
            i_SecondPlayer.InitializeCheckersArray(i_GameBoard);
            bool hasGameFinished = false;
            bool isFirstPlayerTurn = true;
            string currentMove= string.Empty;
            string destinationPosition;
            while (hasGameFinished)
            {
                if (isFirstPlayerTurn)
                {
                    currentMove = UserIntterface.GetValidMove(i_GameBoard);
                    if(currentMove =="Q")
                    {

                    }
                    hasGameFinished = i_FirstPlayer.MakeMove(i_GameBoard);
                }
                else
                {
                    currentMove = UserIntterface.GetValidMove(i_GameBoard);
                    if (currentMove == "Q")
                    {
                        i_FirstPlayer.quit();
                    }
                    else
                    {
                        i_SecondPlayer.MakeMove(ref hasGameFinished);
                    }
                    
                }
            }

            printResult(i_FirstPlayer, i_SecondPlayer);
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
