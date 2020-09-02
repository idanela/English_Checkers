﻿using System;
using CheckersBoard;
using Player;

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

            private static bool IsValidUserName(string nameOfPlayer)
            {
                bool isValidName = nameOfPlayer.Length <= 20 && nameOfPlayer.Length > 0;

                if (isValidName)
                {
                    for (int i = 0; i < nameOfPlayer.Length; i++)
                    {
                        isValidName = isValidName && char.IsLetter(nameOfPlayer[i]);
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
            while (!ushort.TryParse(Console.ReadLine(), out sizeOfBoard) || !CheckersBoard.Board.isValidBoardSize(sizeOfBoard))
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

        public static string GetValidMove(Board i_Board)
        {
            Console.WriteLine("please enter Q if you like to quit otherwise insert a move");
            string move = Console.ReadLine();
            while (!IsValidMove(move,i_Board))
            {
                Console.WriteLine("Invalid Input");
                move = Console.ReadLine();
            }

            return move;
        }

        public static bool IsValidMove(string i_MoveToPreform, Board i_Board)
        {
            return i_MoveToPreform == "Q" || isLegalMove(i_MoveToPreform,i_Board);
        }
        public static bool isLegalMove(string i_moveToPreform,Board i_Board)
        {
            return (isLegalMovePattern(i_moveToPreform) && IsValidBoardMove(i_moveToPreform, i_Board)); 

        }
        public static bool isLegalMovePattern(string i_moveToPreform)
        {
                return i_moveToPreform.Length == 5 && 
                char.IsUpper(i_moveToPreform[0]) &&
                char.IsLower(i_moveToPreform[1]) &&
                i_moveToPreform[2] == '>' &&
                char.IsUpper(i_moveToPreform[3]) &&
                char.IsLower(i_moveToPreform[4]);
        }

       public static bool  IsValidBoardMove(string i_moveToPreform, Board i_Board)
        {
            string location = string.Empty;
            string destination = string.Empty;
            Validation.ParsePositions(i_moveToPreform, ref location, ref destination);
            
            return checkIndexes(i_Board, location, destination);
        }

       public static bool checkIndexes(Board i_Board, string i_Location, string i_Destination)
        {
            ushort colIndex = (ushort) (i_Location[0] - 'A');
            ushort rowIndex = (ushort)(i_Location[1] - 'a');
            ushort destinationRowIndex = (ushort) (i_Destination[0] -'A');
            ushort destinationColIndex = (ushort)(i_Destination[1] - 'a');
            bool isValidIndexesMove = colIndex >= 0 && colIndex < i_Board.SizeOfBoard && rowIndex >= 0 && rowIndex < i_Board.SizeOfBoard
                && destinationRowIndex >= 0 && destinationRowIndex < i_Board.SizeOfBoard && destinationRowIndex >= 0 && destinationRowIndex < i_Board.SizeOfBoard;

            return isValidIndexesMove;
        }

        public static void PrintForfeitMessage(string i_playerName, string i_RivalPlayerName)
        {
            string forfeitMessage = string.Format("{0} has quit, {1} is the winner", i_playerName,i_RivalPlayerName);
            Console.WriteLine(forfeitMessage);
        }

        public static string  PlayerTurn( Board i_GameBoard, User i_Player)
        {
            Console.Write(i_Player.Name+"'s turn");
            if(i_Player.CheckerKind == CheckerPiece.CheckersPiece.ePieceKind.MainPlayerTool)
            {
                Console.WriteLine("(O)");
            }
            else
            {
                Console.WriteLine("(X)");

            }
            string currentMove = UserIntterface.GetValidMove(i_GameBoard);

            return currentMove;
        }

    }

}
