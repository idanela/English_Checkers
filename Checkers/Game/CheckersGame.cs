using System;
using Player;
using CheckersBoard;
using CheckerPiece;
using UI;
using Validation;

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
            Board gameBoard = new Board(boardSize);
            char choice = UserIntterface.GetRival();
            string rivalName = GetSecondPlayersName(choice,ref isComputer); 
            User rivalPlayer = new User(rivalName, User.ePlayerType.RivalPlayer, isComputer);

            StartGame(ref firstPlayer, ref rivalPlayer, gameBoard);
        }

        private static void StartGame(ref User i_FirstPlayer, ref User i_SecondPlayer, Board i_GameBoard)
        {
            bool isGameFinished = false;
            bool isFirstPlayerTurn = true;
            bool IsGameFinished = false; 

            setBeginningOfGame(ref i_FirstPlayer, ref i_SecondPlayer, i_GameBoard);
            while (!isGameFinished)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                i_GameBoard.printBoard();
                if (isFirstPlayerTurn)
                {
                    IsGameFinished = PlayYourTurn(ref i_FirstPlayer, ref i_SecondPlayer, i_GameBoard, ref isGameFinished);
                    if (i_FirstPlayer.HasAnotherTurn())
                    {
                        isFirstPlayerTurn = !isFirstPlayerTurn;
                    }
                }
                else
                {
                    IsGameFinished = PlayYourTurn(ref i_SecondPlayer, ref i_FirstPlayer, i_GameBoard,ref isGameFinished);
                    if (i_SecondPlayer.HasAnotherTurn())
                    {
                        isFirstPlayerTurn = !isFirstPlayerTurn;
                    }
                }

                isFirstPlayerTurn = !isFirstPlayerTurn;
                isGameFinished = isGameFinished|| IsGameFinished;
            }
            calculateAndPrintResulsts(ref i_FirstPlayer, ref i_SecondPlayer);    
           if(UserIntterface.WouldLikeToPlayAgain())
            { 
                StartGame(ref i_FirstPlayer, ref i_SecondPlayer, i_GameBoard);
            }
        }

private static void printResult(User i_Player, User i_RivalPlayer)
{
            if (i_Player.HasQuit || i_RivalPlayer.HasQuit)
            {
                UserIntterface.PrintForfeitMessage(i_Player.Name, i_RivalPlayer.Name);
            }
            else
            {
                UserIntterface.PrintResultOfTheGame(i_Player.Name, i_RivalPlayer.Name,i_Player.Score, i_RivalPlayer.Score);
            }          
        }

        private static void quitGame(ref User i_player, ref User i_RivalPlayer, ref bool isGameFinished)
        {
            i_player.Score = 0;
            i_RivalPlayer.Score = 0;
            UserIntterface.PrintForfeitMessage(i_player.Name, i_RivalPlayer.Name);
            isGameFinished = true;
        }

      public static string GetSecondPlayersName(char i_ChioceOfRival , ref bool isComputer)
        {
            string rivalName = string.Empty;

            if (i_ChioceOfRival == '1')
            {
                rivalName = UserIntterface.GetValidUserName();
            }
            else
            {
                isComputer = true;
                rivalName = "Computer";
            }

            return rivalName;
        }

        //private static void playTurns(User i_FirstPlayer, User i_SecondPlayer, Board i_GameBoard)
        //{
        //    bool isGameFinished = false;
        //    bool isFirstPlayerTurn = true;
        //    string currentMove = null;
        //    string currentLocation = null;
        //    string destinationPosition = string.Empty;
        //    bool hasQuit = false;
        //    while (!isGameFinished)
        //    {
        //        i_GameBoard.printBoard();
        //        if (isFirstPlayerTurn)
        //        {
        //            currentMove = UserIntterface.PlayerTurn(i_GameBoard, i_FirstPlayer.Name, CheckersPiece.ePieceKind.MainPlayerTool, ref hasQuit);
        //            if (currentMove == "Q")
        //            {
        //                quitGame(ref i_FirstPlayer, ref i_SecondPlayer, ref isGameFinished);
        //            }
        //            else
        //            {
        //                Validation.ParsePositions(currentMove, ref currentLocation, ref destinationPosition);
        //                isGameFinished = i_FirstPlayer.MakeMove(ref i_GameBoard, i_SecondPlayer, currentLocation, destinationPosition);
        //            }
        //        }
        //        else
        //        {
        //            currentMove = UserIntterface.PlayerTurn(i_GameBoard, i_SecondPlayer.Name, CheckersPiece.ePieceKind.SecondPlayerTool, ref hasQuit);
        //            Validation.ParsePositions(currentMove, ref currentLocation, ref destinationPosition);
        //            if (currentMove == "Q")
        //            {
        //                quitGame(ref i_SecondPlayer, ref i_FirstPlayer, ref isGameFinished);
        //            }
        //            else
        //            {
        //                isGameFinished = i_SecondPlayer.MakeMove(ref i_GameBoard, i_FirstPlayer, currentLocation, destinationPosition);
        //            }
        //        }

        //        isFirstPlayerTurn = !isFirstPlayerTurn;
        //        isGameFinished = hasQuit || i_FirstPlayer.HasQuit || i_SecondPlayer.HasQuit;    
        //    }
        //}

       public static bool PlayYourTurn(ref User i_Player, ref User i_RivalPlayer, Board i_GameBoard,ref bool isGameFinished)
       {
            string currentMove = string.Empty;
            string currentLocation = null;
            string destinationPosition = null;
            bool hasQuit = i_Player.HasQuit;
            if (!i_Player.IsComputer)
            {
                currentMove = UserIntterface.PlayerTurn(i_GameBoard.SizeOfBoard, i_Player.Name, i_Player.CheckerKind,ref hasQuit);
                i_Player.HasQuit = hasQuit; 
               if(!i_Player.HasQuit)
               {
                    Validate.ParsePositions(currentMove, ref currentLocation, ref destinationPosition);
               }
            }

            if(!i_Player.HasQuit)
            {
                isGameFinished = i_Player.MakeMove(ref i_GameBoard, i_RivalPlayer, currentLocation, destinationPosition);
            }

            return hasQuit;
        }

        private static void setBeginningOfGame(ref User i_FirstPlayer, ref User i_SecondPlayer,Board i_GameBoard)
        {
            i_GameBoard.InitializeBoard();
            i_FirstPlayer.InitializeCheckersArray(i_GameBoard.SizeOfBoard);
            i_FirstPlayer.HasQuit = false;
            i_SecondPlayer.InitializeCheckersArray(i_GameBoard.SizeOfBoard);
            i_SecondPlayer.HasQuit = false;
        }

        private static void calculateAndPrintResulsts(ref User i_FirstPlayer, ref User i_SecondPlayer)
        {
            ushort firstPlayerScore = i_FirstPlayer.GetAndCalculateScore();
            ushort secondPlayerScore = i_SecondPlayer.GetAndCalculateScore();

            if(firstPlayerScore> secondPlayerScore)
            {
                i_FirstPlayer.Score += (ushort)(firstPlayerScore - secondPlayerScore);
            }
            else
            {
                i_SecondPlayer.Score += (ushort)(secondPlayerScore - firstPlayerScore);
            }

            UserIntterface.PrintResultOfTheGame(i_FirstPlayer.Name, i_SecondPlayer.Name, firstPlayerScore, secondPlayerScore);
            UserIntterface.PrintScore(i_FirstPlayer.Name, i_SecondPlayer.Name, i_FirstPlayer.Score, i_SecondPlayer.Score);
        }

    }
}
