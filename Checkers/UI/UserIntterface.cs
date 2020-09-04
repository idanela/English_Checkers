using System;
using CheckersBoard;
using CheckerPiece;
using Validation;

namespace UI
{
   public class UserIntterface
    {
        public static string GetValidUserName()
        {
            Console.WriteLine("Please enter player's name:");
            return getValidPlayerName();
        }

        private static string getValidPlayerName()
        {
            string playerName = Console.ReadLine();

            while (!IsValidUserName(playerName))
            {
                Console.WriteLine("Invalid Input please type your name again.");
                playerName = Console.ReadLine();
            }

            return playerName;
        }

            private static bool IsValidUserName(string i_NameOfPlayer)
            {
                bool isValidName = i_NameOfPlayer.Length <= 20 && i_NameOfPlayer.Length > 0;

                if (isValidName)
                {
                    for (int i = 0; i < i_NameOfPlayer.Length; i++)
                    {
                        isValidName = isValidName && char.IsLetter(i_NameOfPlayer[i]);
                    }
                }

                return isValidName;
            }

        public static ushort GetValidBoardSize()
        {
            Console.WriteLine("Please enter the size of the Board");
            return getValidBoardSize();
        }

        private static ushort getValidBoardSize()
        {
            ushort sizeOfBoard;

            while (!ushort.TryParse(Console.ReadLine(), out sizeOfBoard) || !CheckersBoard.Board.IsValidBoardSize(sizeOfBoard))
            {
                Console.WriteLine("Invalid input please insert size again.");
            }

            return sizeOfBoard;
        }

        public static char GetRival()
        {
            Console.WriteLine(@"Please pick the game kind
1. Two players.
2. VS computer.");

            return getCompetitor();
        }

        private static char getCompetitor()
        {
            char choice = getValidchoice(Console.ReadLine());

            return choice;
        }

        private static char getValidchoice(string PlayerChoice)
        {
            char choice;
            while (!char.TryParse(PlayerChoice, out choice) || (choice != '1' && choice != '2'))
            {
                Console.WriteLine("invalid choice. please try again:");
                PlayerChoice = Console.ReadLine();
            }

            return choice;
        }

        public static bool WouldLikeToPlayAgain()
        {
            Console.WriteLine("To play Again press '1' otherwise press '2'.");
            char WouldLikeToPlayAgain = getValidchoice(Console.ReadLine());

            return WouldLikeToPlayAgain == '1';
        }

        public static string GetValidMove(ushort i_BoardSize)
        {
            Console.WriteLine("please enter Q if you like to quit otherwise insert a move");
            string move = Console.ReadLine();

            while (!Validate.IsValidMove(move, i_BoardSize))
            {      
                Console.WriteLine("Invalid Input");
                move = Console.ReadLine();
            }

            return move;
        }

        public static void PrintForfeitMessage(string i_playerName, string i_RivalPlayerName)
        {
            string forfeitMessage = string.Format("{0} has quit, {1} is the winner", i_playerName, i_RivalPlayerName);
            Console.WriteLine(forfeitMessage);
        }

        public static string GetPlayerTurn(ushort i_BoardSize, string i_PlayerName, CheckersPiece.ePieceKind pieceKind, ref bool i_HasQuit)
        {
            Console.Write(i_PlayerName + "'s turn");
            if (pieceKind == CheckerPiece.CheckersPiece.ePieceKind.MainPlayerTool)
            {
                Console.WriteLine("(O)");
            }
            else
            {
                Console.WriteLine("(X)");
            }

            string currentMove = UserIntterface.GetValidMove(i_BoardSize);

            i_HasQuit = currentMove == "Q";
            return currentMove;
        }

        public static void PrintErrorMessage(string i_ErrorMsg)
        {
            Console.WriteLine(i_ErrorMsg);
        }

        public static void PrintResultOfTheGame(string i_PlayersName, string i_RivalsName, ushort i_PlayersScore, ushort i_RivalsScore)
        {
            string winner = string.Empty;
            if(i_PlayersScore == i_RivalsScore)
            {
                Console.WriteLine("it's a draw");
            }
            else 
            {
                Console.Write("The winner is:");
                if (i_PlayersScore > i_RivalsScore)
                {
                    Console.WriteLine(i_PlayersName);
                }
                else
                {
                    Console.WriteLine(i_RivalsName);
                }   
            }       
        }

        public static void PrintScore(string i_PlayersName, string i_RivalsName, ushort i_PlayersScore, ushort i_RivalsScore)
        {
            string ScoreMessage = string.Format(
    @"
{0}'s score is: {1} 
{2}'s score is: {3}",
i_PlayersName,
i_PlayersScore,
i_RivalsName,
i_RivalsScore);
            Console.WriteLine(ScoreMessage);
        }
    }
}
