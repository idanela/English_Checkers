using System;
using Player;
using CheckersBoard;
using UI;
using Validation;
using System.Collections.Generic;

namespace Game
{
    public class CheckersGame
    {
        public static void RunGame()
        {
            bool isComputer = false;
            string firstPlayerName = UserIntterface.GetValidUserName();
            User firstPlayer = new User(firstPlayerName, User.ePlayerType.MainPlayer, isComputer);
            ushort boardSize = UserIntterface.GetValidBoardSize();
            Board gameBoard = new Board(boardSize);
            char choseRival = UserIntterface.GetRival();
            string rivalName = getSecondPlayersName(choseRival, ref isComputer); 
            User rivalPlayer = new User(rivalName, User.ePlayerType.RivalPlayer, isComputer);

            startGame(ref firstPlayer, ref rivalPlayer, gameBoard);
        }

        private static void startGame(ref User i_FirstPlayer, ref User i_SecondPlayer, Board i_GameBoard)
        {
            bool isGameFinished = false;
            bool isFirstPlayerTurn = true;   

            setBeginningOfGame(ref i_FirstPlayer, ref i_SecondPlayer, i_GameBoard);
            while (!isGameFinished)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                i_GameBoard.printBoard();
                if (isFirstPlayerTurn)
                {
                    isGameFinished = PlayYourTurn(ref i_FirstPlayer, ref i_SecondPlayer, i_GameBoard, ref isGameFinished);          
                }
                else
                {
                    isGameFinished = PlayYourTurn(ref i_SecondPlayer, ref i_FirstPlayer, i_GameBoard, ref isGameFinished);
                }

                getAnotherTurn(ref i_FirstPlayer, ref i_SecondPlayer, ref isFirstPlayerTurn, i_GameBoard);
                isFirstPlayerTurn = !isFirstPlayerTurn;
                isGameFinished = isGameFinished || isDraw(ref i_FirstPlayer, ref i_SecondPlayer, i_GameBoard);
            }

            finishRound(ref i_FirstPlayer, ref i_SecondPlayer);

            if (UserIntterface.WouldLikeToPlayAgain())
            { 
                startGame(ref i_FirstPlayer, ref i_SecondPlayer, i_GameBoard);
            }
        }

        private static void setBeginningOfGame(ref User i_FirstPlayer, ref User i_SecondPlayer, Board i_GameBoard)
        {
            i_GameBoard.InitializeBoard();
            i_FirstPlayer.InitializeCheckersArray(i_GameBoard.SizeOfBoard);
            i_FirstPlayer.HasQuit = false;
            i_SecondPlayer.InitializeCheckersArray(i_GameBoard.SizeOfBoard);
            i_SecondPlayer.HasQuit = false;
        }

      private static string getSecondPlayersName(char i_ChioceOfRival, ref bool io_IsComputer)
        {
            string rivalName = string.Empty;

            if (i_ChioceOfRival == '1')
            {
                rivalName = UserIntterface.GetValidUserName();
            }
            else
            {
                io_IsComputer = true;
                rivalName = "Computer";
            }

            return rivalName;
        }

       public static bool PlayYourTurn(ref User i_Player, ref User i_RivalPlayer, Board i_GameBoard, ref bool io_IsGameFinished)
       {
            string currentMove = string.Empty;
            string currentLocation = null;
            string destinationPosition = null;
            bool hasQuit = false;
            if (!i_Player.IsComputer)
            {
                currentMove = UserIntterface.GetPlayerTurn(i_GameBoard.SizeOfBoard, i_Player.Name, i_Player.CheckerKind, ref hasQuit);
                i_Player.HasQuit = hasQuit; 
               if(!i_Player.HasQuit)
               {
                    Validate.ParsePositions(currentMove, ref currentLocation, ref destinationPosition);
               }
            }

            if(!i_Player.HasQuit)
            {
                io_IsGameFinished = i_Player.MakeMove(i_GameBoard, i_RivalPlayer, currentLocation, destinationPosition);
            }

            return hasQuit || io_IsGameFinished;
        }

        private static void calculateAndPrintResulsts(ref User i_FirstPlayer, ref User i_SecondPlayer)
        {
            ushort firstPlayerScore = i_FirstPlayer.GetAndCalculateScore();
            ushort secondPlayerScore = i_SecondPlayer.GetAndCalculateScore();

            if(firstPlayerScore > secondPlayerScore)
            {
                i_FirstPlayer.Score += (ushort)(firstPlayerScore - secondPlayerScore);
            }
            else
            {
                i_SecondPlayer.Score += (ushort)(secondPlayerScore - firstPlayerScore);
            }
        }

        private static void printResult(User i_Player, User i_RivalPlayer)
        {
            if (i_Player.HasQuit)
            {
                UserIntterface.PrintForfeitMessage(i_Player.Name, i_RivalPlayer.Name);
            }
            else if (i_RivalPlayer.HasQuit)
            {
                UserIntterface.PrintForfeitMessage(i_RivalPlayer.Name, i_Player.Name);
            }
            else
            {
                UserIntterface.PrintResultOfTheGame(i_Player.Name, i_RivalPlayer.Name, i_Player.Score, i_RivalPlayer.Score);
            }
        }

        private static void finishRound(ref User i_FirstPlayer, ref User i_SecondPlayer)
        {
            if (!i_FirstPlayer.HasQuit && !i_SecondPlayer.HasQuit)
            {
                calculateAndPrintResulsts(ref i_FirstPlayer, ref i_SecondPlayer);
            }

            printResult(i_FirstPlayer, i_SecondPlayer);
            UserIntterface.PrintScore(i_FirstPlayer.Name, i_SecondPlayer.Name, i_FirstPlayer.Score, i_SecondPlayer.Score);
        }

        private static void getAnotherTurn(ref User i_Player, ref User i_Rival, ref bool i_currentTurn, Board i_GameBoard)
        {
            if (i_Player.HasAnotherTurn(ref i_Rival, i_GameBoard) || i_Rival.HasAnotherTurn(ref i_Player, i_GameBoard)) 
            {
                i_currentTurn = !i_currentTurn;
            }
        }

        private static bool isDraw(ref User i_FirstPlayer, ref User i_SecondPlayer, Board i_GameBoard)
        {
            Dictionary<string, List<string>> movesFirstPlayer = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> movesSecondPlayer = new Dictionary<string, List<string>>();

            movesFirstPlayer = MoveUtils.CreateRegularMoves(i_FirstPlayer, i_GameBoard);
            CaptureUtils.CanUserCapture(i_GameBoard, i_FirstPlayer, i_SecondPlayer, ref movesFirstPlayer);

            movesSecondPlayer = MoveUtils.CreateRegularMoves(i_SecondPlayer, i_GameBoard);
            CaptureUtils.CanUserCapture(i_GameBoard, i_SecondPlayer, i_FirstPlayer, ref movesSecondPlayer);

            return movesFirstPlayer.Count == 0 && movesSecondPlayer.Count == 0;
        }
    }
}
