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

            setBeginningOfGame(ref i_FirstPlayer, ref i_SecondPlayer, i_GameBoard);
            while (!isGameFinished)
            {
                i_GameBoard.printBoard();
                if (isFirstPlayerTurn)
                {
                    PlayYourTurn(ref i_FirstPlayer, ref i_SecondPlayer, i_GameBoard, ref isGameFinished);
                    if (i_FirstPlayer.HasAnotherTurn())
                    {
                        isFirstPlayerTurn = !isFirstPlayerTurn;
                    }
                }
                else
                {
                    PlayYourTurn(ref i_SecondPlayer, ref i_FirstPlayer, i_GameBoard,ref isGameFinished);
                    if (i_SecondPlayer.HasAnotherTurn())
                    {
                        isFirstPlayerTurn = !isFirstPlayerTurn;
                    }
                }

                isFirstPlayerTurn = !isFirstPlayerTurn;
            }

            //printResult(i_FirstPlayer, i_SecondPlayer);
           if(UserIntterface.WouldLikeToPlayAgain())
            { 
                StartGame(ref i_FirstPlayer, ref i_SecondPlayer, i_GameBoard);
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

        private static void playTurns(User i_FirstPlayer, User i_SecondPlayer, Board i_GameBoard)
        {
            bool isGameFinished = false;
            bool isFirstPlayerTurn = true;
            string currentMove = null;
            string currentLocation = null;
            string destinationPosition = string.Empty;

            while (!isGameFinished)
            {
                i_GameBoard.printBoard();
                if (isFirstPlayerTurn)
                {
                    currentMove = UserIntterface.PlayerTurn(i_GameBoard, i_FirstPlayer.Name, CheckersPiece.ePieceKind.MainPlayerTool);
                    if (currentMove == "Q")
                    {
                        quitGame(ref i_FirstPlayer, ref i_SecondPlayer, ref isGameFinished);
                    }
                    else
                    {
                        Validation.ParsePositions(currentMove, ref currentLocation, ref destinationPosition);
                        isGameFinished = i_FirstPlayer.MakeMove(ref i_GameBoard, i_SecondPlayer, currentLocation, destinationPosition);
                    }
                }
                else
                {
                    currentMove = UserIntterface.PlayerTurn(i_GameBoard, i_SecondPlayer.Name, CheckersPiece.ePieceKind.SecondPlayerTool);
                    Validation.ParsePositions(currentMove, ref currentLocation, ref destinationPosition);
                    if (currentMove == "Q")
                    {
                        quitGame(ref i_SecondPlayer, ref i_FirstPlayer, ref isGameFinished);
                    }
                    else
                    {
                        isGameFinished = i_SecondPlayer.MakeMove(ref i_GameBoard, i_FirstPlayer, currentLocation, destinationPosition);
                    }

                    isFirstPlayerTurn = !isFirstPlayerTurn;
                }
            }
        }

       private static void PlayYourTurn(ref User i_Player, ref User i_RivalPlayer, Board i_GameBoard,ref bool isGameFinished)
        {
            string currentMove = string.Empty;
            string currentLocation = null;
            string destinationPosition = string.Empty;
            
            if (!i_Player.IsComputer)
            {
                currentMove = UserIntterface.PlayerTurn(i_GameBoard, i_Player.Name, i_Player.CheckerKind);
                if (currentMove == "Q")
                {
                    quitGame(ref i_Player, ref i_RivalPlayer, ref isGameFinished);
                }
                else
                {
                    Validation.ParsePositions(currentMove, ref currentLocation, ref destinationPosition);
                }
            }

            isGameFinished = i_Player.MakeMove(ref i_GameBoard, i_RivalPlayer, currentLocation, destinationPosition);   
        }

        private static void setBeginningOfGame(ref User i_FirstPlayer, ref User i_SecondPlayer,Board i_GameBoard)
        {
            i_GameBoard.InitializeBoard();
            i_FirstPlayer.InitializeCheckersArray(i_GameBoard.SizeOfBoard);
            i_SecondPlayer.InitializeCheckersArray(i_GameBoard.SizeOfBoard);
        }
    }
}
