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

            while (IsValidUserName(playerName))
            {
                Console.WriteLine("Invalid Input please type your name again.");
                playerName = Console.ReadLine();
            }

            return playerName;
        }

            private static bool IsValidUserName(string nameOfPlayer)
            {
                bool isValidName = nameOfPlayer.Length > 20 && nameOfPlayer.Length > 0;

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
                ushort.TryParse(Console.ReadLine(), out sizeOfBoard);
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
            while (!char.TryParse(PlayerChoice, out choice) && choice != '1' && choice != '2')
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
                char.IsLower(i_moveToPreform[0]) &&
                i_moveToPreform[3] == '>' &&
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

       public bool checkIndexes(Board i_Board, string i_Location, string i_Destination)
        {
            bool isValidIndexesMove = false;
            ushort colIndex = ushort.Parse(i_Location[0].ToString());
            ushort rowIndex = ushort.Parse(i_Location[1].ToString());
            ushort destinationRowIndex = ushort.Parse(i_Destination[0].ToString());
            ushort destinationColIndex = ushort.Parse(i_Destination[1].ToString());

            if(i_Board.CheckersBoard[colIndex,rowIndex] == 'K' || i_Board.CheckersBoard[colIndex, rowIndex] == 'U')
            {
                isValidIndexesMove = i_Board.IsCheckerAvailable(destinationRowIndex, destinationColIndex) &&
                    (Math.Abs(colIndex - destinationColIndex) == 1 && Math.Abs(rowIndex - destinationRowIndex) == 1);
            }

            return isValidIndexesMove;
        }
 
    }

}
