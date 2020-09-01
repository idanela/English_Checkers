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
            if(choice == 1)
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

        //private static void playVsAnotherPlayer(User i_FirstPlayer, Board i_GameBoard)
        //{
        //    string name = UserIntterface.GetValidUserName();
        //    User secondPlayer = new User(name, User.ePlayerType.RivalPlayer, CheckersPiece.ePieceKind.SecondPlayerMainTool);

        //    playGame(i_FirstPlayer, secondPlayer, i_GameBoard);
        //}

        //private static void playVsComputer(User i_FirstPlayer, Board i_GameBoard)
        //{      
        //    User computerPlayer = new User("Computer", User.ePlayerType.RivalPlayer, CheckersPiece.ePieceKind.SecondPlayerMainTool);

        //    playGame(i_FirstPlayer, computerPlayer, i_GameBoard);
        //    printResult(i_FirstPlayer, computerPlayer);
        //}

        private static void playGame(User i_FirstPlayer, User i_SecondPlayer, Board i_GameBoard)
        {
            i_GameBoard.InitializeBoard();
            i_FirstPlayer.InitializeCheckersArray
            i_SecondPlayer.
            bool hasGameFinished = false;
            bool isFirstPlayerTurn = true;
            string currentPosition= string.Empty;
            string destinationPosition;
            while (hasGameFinished)
            {
                if (isFirstPlayerTurn)
                {
                    currentPosition = UserIntterface.GetValidMove(i_GameBoard);
                    if()
                    hasGameFinished = i_FirstPlayer.MakeMove(i_GameBoard);
                }
                else
                {
                    currentPosition = UserIntterface.GetValidMove(i_GameBoard);
                    i_SecondPlayer.MakeMove(ref hasGameFinished);
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
