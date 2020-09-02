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
            bool isComputer = false;
            string firstPlayerName = UserIntterface.GetValidUserName();
            User firstPlayer = new User(firstPlayerName, User.ePlayerType.MainPlayer,isComputer);
            ushort boardSize = UserIntterface.GetValidBoardSize();
            firstPlayer.InitializeCheckersArray(boardSize);
            Board gameBoard = new Board(boardSize);
            char choice = UserIntterface.GetRival();
            string rivalName = string.Empty;
            // gameBoard.printBoard();
            if (choice == '1')
            {
                rivalName = UserIntterface.GetValidUserName();
            }
            else
            {
                isComputer = true;
                rivalName = "computer";   
            }

            User rivalPlayer = new User(rivalName, User.ePlayerType.RivalPlayer, isComputer);
            rivalPlayer.InitializeCheckersArray(boardSize);
            playGame(firstPlayer, rivalPlayer, gameBoard);
        }

        private static void playGame(User i_FirstPlayer, User i_SecondPlayer, Board i_GameBoard)
        {
            i_GameBoard.InitializeBoard();
            i_FirstPlayer.InitializeCheckersArray(i_GameBoard.SizeOfBoard);
            i_SecondPlayer.InitializeCheckersArray(i_GameBoard.SizeOfBoard);
            bool hasGameFinished = false;
            bool isFirstPlayerTurn = true;
            string currentMove= string.Empty;
            string currentLocation = string.Empty;
            string destinationPosition =string.Empty;

            while (!hasGameFinished)
            {
                i_GameBoard.printBoard();
                if (isFirstPlayerTurn)
                {
                    currentMove = UserIntterface.PlayerTurn(i_GameBoard, i_FirstPlayer);
                    Validation.ParsePositions(currentMove, ref currentLocation, ref destinationPosition);
                    if (currentMove =="Q")
                    {
                        quitGame(i_FirstPlayer, i_SecondPlayer); 
                    }
                    else
                    {
                        hasGameFinished = i_FirstPlayer.MakeMove(ref i_GameBoard, i_SecondPlayer,currentLocation,destinationPosition);
                    }
                }
                else
                {
                    currentMove = UserIntterface.PlayerTurn(i_GameBoard, i_FirstPlayer);
                    Validation.ParsePositions(currentMove, ref currentLocation, ref destinationPosition);
                    //currentMove = UserIntterface.GetValidMove(i_GameBoard);
                    if (currentMove == "Q")
                    {
                        quitGame(i_SecondPlayer, i_FirstPlayer); 
                    }
                    else
                    {
                        hasGameFinished = i_SecondPlayer.MakeMove(ref i_GameBoard, i_FirstPlayer, currentLocation, destinationPosition);
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

        private static void quitGame(User i_player,User i_RivalPlayer)
        {
            i_player.Score = 0;
            i_RivalPlayer.Score = 0;
            UserIntterface.PrintForfeitMessage(i_player.Name, i_RivalPlayer.Name);
        }

    }
}
