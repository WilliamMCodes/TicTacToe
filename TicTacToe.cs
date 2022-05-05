using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class TicTacToe
    {
        private Regex checkInput = new Regex("[A-Ca-c][1-3]");
        private int[,] gameBoard;
        readonly bool firstGoesFirst;
        private int turnNumber;
        int numberOfPlayers;
        player2 opponent = new();
        gameStatus status = new();
        public enum gameStatus
        {
            PLAYER_1Wins,
            PLAYER_2Wins,
            Draw,
            Continue
        }
        public enum player2
        {
            COMPUTER,
            PLAYER_2
        }
        public TicTacToe(int numberOfPlayers, bool firstGoesFirst)
        {
            gameBoard = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            this.numberOfPlayers = numberOfPlayers;
            if (numberOfPlayers > 1)
            {
                opponent = player2.PLAYER_2;
            }
            else
            {
                opponent = player2.COMPUTER;
            }
            this.firstGoesFirst = firstGoesFirst;
            status = gameStatus.Continue;
        }

        public void PlayGame() { 

            while (status == gameStatus.Continue)
            {
                DisplayBoard();
                if (numberOfPlayers < 2 && this.firstGoesFirst && turnNumber % 2 != 0 || numberOfPlayers < 2 && !this.firstGoesFirst && turnNumber % 2 == 0)
                {
                    SetTicsAndTacs(DecideBlockOrWin());
                }
                else
                {
                    SetTicsAndTacs(Console.ReadLine());
                }
                status = WinDrawContinue();
            }
        }

        public void DisplayBoard()
        {
            Console.Clear();
            if (firstGoesFirst && turnNumber % 2 == 0 || !firstGoesFirst && turnNumber % 2 != 0)
            {
                Console.WriteLine($"\n   | PLAYER_1 | {opponent}\n\n");
            }
            else
            {
                Console.WriteLine($"\n     PLAYER_1 | {opponent} |\n");
            }    
            Console.WriteLine($"" +
                $"          1   2   3\n" +
                $"      A   {gameBoard[0,0]} | {gameBoard[0,1]} | {gameBoard[0,2]}   \n" +
                $"        -------------\n" +
                $"      B   {gameBoard[1,0]} | {gameBoard[1,1]} | {gameBoard[1,2]}   \n" +
                $"        -------------\n" +
                $"      C   {gameBoard[2,0]} | {gameBoard[2,1]} | {gameBoard[2,2]}   \n");
            if (status == gameStatus.Continue && (firstGoesFirst && turnNumber % 2 == 0 || !firstGoesFirst && turnNumber % 2 != 0))
            {
                Console.Write($"\nPlease enter PLAYER_1's move: ");
            }
            else if(status == gameStatus.Continue)
            {
                Console.Write($"\nPlease enter {opponent}'s move: ");
            }
        }

        public void SetTicsAndTacs(string playerInput)
        {
            bool goodInput = false;
            while (!goodInput)
            {
                try
                {
                    if (checkInput.IsMatch(playerInput))
                    {
                        int player;
                        if (firstGoesFirst && turnNumber % 2 == 0 || !firstGoesFirst && turnNumber % 2 != 0)
                        {
                            player = 1;
                        }
                        else
                        {
                            player = 2;
                        }
                        int x = int.Parse(playerInput[1].ToString()) - 1;
                        switch (char.ToUpper(playerInput[0]))
                        {
                            case 'A':
                                if (IsEmptySpace(0, x))
                                {
                                    gameBoard[0, x] = player;
                                    ++turnNumber;
                                    goodInput = true;
                                    break;
                                }
                                else
                                {
                                    throw new Exception("Please enter only empty locations contained on the screen.\n\nPress any key to continue.");
                                }
                            case 'B':
                                if (IsEmptySpace(1, x))
                                {
                                    gameBoard[1, x] = player;
                                    ++turnNumber;
                                    goodInput = true;
                                    break;
                                }
                                else
                                {
                                    throw new Exception("\nPlease enter only empty locations contained on the screen.\n\nPress any key to continue.");
                                }
                            case 'C':
                                if (IsEmptySpace(2, x))
                                {
                                    gameBoard[2, x] = player;
                                    ++turnNumber;
                                    goodInput = true;
                                    break;
                                }
                                else
                                {
                                    throw new Exception("\nPlease enter only empty locations contained on the screen.\n\nPress any key to continue.");
                                }
                            default:
                                throw new Exception("\nPlease enter only accepted values.\n\nPress any key to continue.");
                        }
                    }
                    else
                    {
                        throw new Exception("\nPlease enter only empty locations contained on the screen.\n\nPress any key to continue.");
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    DisplayBoard();
                }
            }  
        }

        private bool IsEmptySpace(int y, int x)
        {
            if(gameBoard[y, x] == 0)
            {
                return true;
            }
            return false;
        }
        private string DecideBlockOrWin()
        {
            //check for winning move
            char letter;
            for (int y = 0; y < 3; y++)
            {
                //check rows for a winning move
                if (gameBoard[y, 0] == 2 && (gameBoard[y, 1] == 2 || gameBoard[y, 2] == 2) && (gameBoard[y, 1] == 0 || gameBoard[y, 2] == 0) ||
                    gameBoard[y, 1] == 2 && (gameBoard[y, 0] == 2 || gameBoard[y, 2] == 2) && (gameBoard[y, 0] == 0 || gameBoard[y, 2] == 0) ||
                    gameBoard[y, 2] == 2 && (gameBoard[y, 1] == 2 || gameBoard[y, 0] == 2) && (gameBoard[y, 1] == 0 || gameBoard[y, 0] == 0))
                {
                    switch (y)
                    {
                        case 0:
                            letter = 'A';
                            break;
                        case 1:
                            letter = 'B';
                            break;
                        case 2:
                            letter = 'C';
                            break;
                        default:
                            letter = 'D';
                            break;
                    }
                    for (int x = 0; x < 3; x++)
                    {
                        if (gameBoard[y, x] == 0)
                        {
                            return $"{letter}{x+1}";
                        }
                    }
                }
                //check diagnol top left to bottom right
                if (y == 0)
                {
                    if (gameBoard[y, 0] == 2 && (gameBoard[y + 1, 1] == 2 || gameBoard[y + 2, 2] == 2) && (gameBoard[y + 1, 1] == 0 || gameBoard[y + 2, 2] == 0) ||
                    gameBoard[y + 1, 1] == 2 && (gameBoard[y, 0] == 2 || gameBoard[y + 2, 2] == 2) && (gameBoard[y, 0] == 0 || gameBoard[y + 2, 2] == 0) ||
                    gameBoard[y + 2, 2] == 2 && (gameBoard[y + 1, 1] == 2 || gameBoard[y + 2, 0] == 2) && (gameBoard[y + 1, 1] == 0 || gameBoard[y, 0] == 0))
                    {
                        for (y = 0; y < 3; y++)
                        {
                            int x = y;
                            switch (y)
                            {
                                case 0:
                                    letter = 'A';
                                    break;
                                case 1:
                                    letter = 'B';
                                    break;
                                case 2:
                                    letter = 'C';
                                    break;
                                default:
                                    letter = 'D';
                                    break;
                            }
                            if (gameBoard[y, x] == 0)
                            {
                                return $"{letter}{x+1}";
                            }
                        }
                    }
                }
                //check bottom left to top right
                else if (y == 2)
                {
                    if (gameBoard[y, 0] == 2 && (gameBoard[y - 1, 1] == 2 || gameBoard[y - 2, 2] == 2) && (gameBoard[y - 1, 1] == 0 || gameBoard[y - 2, 2] == 0) ||
                    gameBoard[y - 1, 1] == 2 && (gameBoard[y, 0] == 2 || gameBoard[y - 2, 2] == 2) && (gameBoard[y, 0] == 0 || gameBoard[y - 2, 2] == 0) ||
                    gameBoard[y - 2, 2] == 2 && (gameBoard[y - 1, 1] == 2 || gameBoard[y - 2, 0] == 2) && (gameBoard[y - 1, 1] == 0 || gameBoard[y, 0] == 0))
                    {
                        int x = 0;
                        for (y = 2; y >= 0; y--)
                        {
                            switch (y)
                            {
                                case 0:
                                    letter = 'A';
                                    break;
                                case 1:
                                    letter = 'B';
                                    break;
                                case 2:
                                    letter = 'C';
                                    break;
                                default:
                                    letter = 'D';
                                    break;
                            }
                            if (gameBoard[y, x] == 0)
                            {
                                return $"{letter}{x+1}";
                            }
                            x++;
                        }
                    }
                }
            }
            //check columns for win
            for (int x = 0; x < 3; x++)
            {
                //check rows for a winning move
                if (gameBoard[0, x] == 2 && (gameBoard[1, x] == 2 || gameBoard[2, x] == 2) && (gameBoard[1, x] == 0 || gameBoard[2, x] == 0) ||
                    gameBoard[1, x] == 2 && (gameBoard[0, x] == 2 || gameBoard[2, x] == 2) && (gameBoard[0, x] == 0 || gameBoard[2, x] == 0) ||
                    gameBoard[2, x] == 2 && (gameBoard[1, x] == 2 || gameBoard[0, x] == 2) && (gameBoard[1, x] == 0 || gameBoard[0, x] == 0))
                {
                    for (int y = 0; y < 3; y++)
                    {
                        switch (y)
                        {
                            case 0:
                                letter = 'A';
                                break;
                            case 1:
                                letter = 'B';
                                break;
                            case 2:
                                letter = 'C';
                                break;
                            default:
                                letter = 'D';
                                break;
                        }
                        if (gameBoard[y, x] == 0)
                        {
                            return $"{letter}{x+1}";
                        }
                    }
                }
            }

            //check all the previous for blocking moves
            for (int y = 0; y < 3; y++)
            {
                //check rows for a player_1 winning move
                if (gameBoard[y, 0] == 1 && (gameBoard[y, 1] == 1 || gameBoard[y, 2] == 1) && (gameBoard[y, 1] == 0 || gameBoard[y, 2] == 0) ||
                    gameBoard[y, 1] == 1 && (gameBoard[y, 0] == 1 || gameBoard[y, 2] == 1) && (gameBoard[y, 0] == 0 || gameBoard[y, 2] == 0) ||
                    gameBoard[y, 2] == 1 && (gameBoard[y, 1] == 1 || gameBoard[y, 0] == 1) && (gameBoard[y, 1] == 0 || gameBoard[y, 0] == 0))
                {
                    switch (y)
                    {
                        case 0:
                            letter = 'A';
                            break;
                        case 1:
                            letter = 'B';
                            break;
                        case 2:
                            letter = 'C';
                            break;
                        default:
                            letter = 'D';
                            break;
                    }
                    for (int x = 0; x < 3; x++)
                    {
                        if (gameBoard[y, x] == 0)
                        {
                            return $"{letter}{x+1}";
                        }
                    }
                }
                //check diagnol top left to bottom right
                if (y == 0)
                {
                    if (gameBoard[y, 0] == 1 && (gameBoard[y + 1, 1] == 1 || gameBoard[y + 2, 2] == 1) && (gameBoard[y + 1, 1] == 0 || gameBoard[y + 2, 2] == 0) ||
                    gameBoard[y + 1, 1] == 1 && (gameBoard[y, 0] == 1 || gameBoard[y + 2, 2] == 1) && (gameBoard[y, 0] == 0 || gameBoard[y + 2, 2] == 0) ||
                    gameBoard[y + 2, 2] == 1 && (gameBoard[y + 1, 1] == 1 || gameBoard[y + 2, 0] == 1) && (gameBoard[y + 1, 1] == 0 || gameBoard[y, 0] == 0))
                    {
                        for (y = 0; y < 3; y++)
                        {
                            int x = y;
                            switch (y)
                            {
                                case 0:
                                    letter = 'A';
                                    break;
                                case 1:
                                    letter = 'B';
                                    break;
                                case 2:
                                    letter = 'C';
                                    break;
                                default:
                                    letter = 'D';
                                    break;
                            }
                            if (gameBoard[y, x] == 0)
                            {
                                return $"{letter}{x+1}";
                            }
                        }
                    }
                }
                //check bottom left to top right
                else if (y == 2)
                {
                    if (gameBoard[y, 0] == 1 && (gameBoard[y - 1, 1] == 1 || gameBoard[y - 2, 2] == 1) && (gameBoard[y - 1, 1] == 0 || gameBoard[y - 2, 2] == 0) ||
                    gameBoard[y - 1, 1] == 1 && (gameBoard[y, 0] == 1 || gameBoard[y - 2, 2] == 1) && (gameBoard[y, 0] == 0 || gameBoard[y - 2, 2] == 0) ||
                    gameBoard[y - 2, 2] == 1 && (gameBoard[y - 1, 1] == 1 || gameBoard[y - 2, 0] == 1) && (gameBoard[y - 1, 1] == 0 || gameBoard[y, 0] == 0))
                    {
                        int x = 0;
                        for (y = 2; y >= 0; y--)
                        {
                            switch (y)
                            {
                                case 0:
                                    letter = 'A';
                                    break;
                                case 1:
                                    letter = 'B';
                                    break;
                                case 2:
                                    letter = 'C';
                                    break;
                                default:
                                    letter = 'D';
                                    break;
                            }
                            if (gameBoard[y, x] == 0)
                            {
                                return $"{letter}{x+1}";
                            }
                            x++;
                        }
                    }
                }
            }
            //check columns for player_1 win
            for (int x = 0; x < 3; x++)
            {
                if (gameBoard[0, x] == 1 && (gameBoard[1, x] == 1 || gameBoard[2, x] == 1) && (gameBoard[1, x] == 0 || gameBoard[2, x] == 0) ||
                    gameBoard[1, x] == 1 && (gameBoard[0, x] == 1 || gameBoard[2, x] == 1) && (gameBoard[0, x] == 0 || gameBoard[2, x] == 0) ||
                    gameBoard[2, x] == 1 && (gameBoard[1, x] == 1 || gameBoard[0, x] == 1) && (gameBoard[1, x] == 0 || gameBoard[0, x] == 0))
                {
                    for (int y = 0; y < 3; y++)
                    {
                        switch (y)
                        {
                            case 0:
                                letter = 'A';
                                break;
                            case 1:
                                letter = 'B';
                                break;
                            case 2:
                                letter = 'C';
                                break;
                            default:
                                letter = 'D';
                                break;
                        }
                        if (gameBoard[y, x] == 0)
                        {
                            return $"{letter}{x+1}";
                        }
                    }
                }
            }

            //early moves when there may not be a "block or win" option
            if (turnNumber == 0)
            {
                return "A1";
            }
            //second turn
            else if (turnNumber == 1)
            {
                if(IsEmptySpace(1, 1))
                {
                    return "B2";
                }
                //if the first move was to take a corner
                if (!IsEmptySpace(0, 0) || !IsEmptySpace(0, 2) || !IsEmptySpace(2, 0) || !IsEmptySpace(2, 2))
                {
                    if (!IsEmptySpace(0, 0))
                    {
                        return "C3";
                    }
                    else if (!IsEmptySpace(0, 2))
                    {
                        return "C1";
                    }
                    else if (!IsEmptySpace(2, 0))
                    {
                        return "A3";
                    }
                    else
                    {
                        return "A1";
                    }
                }
                //first move took middle 
                else if (!IsEmptySpace(1, 1))
                {
                    return "A1";
                }
            }
            //third turn
            else if (turnNumber == 2)
            {
                if (IsEmptySpace(2, 2))
                {
                    return "C3";
                }
                else
                {
                    return "B2";
                }
            }

            //fourthturn
            else if (turnNumber == 3)
            {
                if(gameBoard[0, 0] == 1 && gameBoard[2, 2] == 1 || gameBoard[2, 0] == 1 && gameBoard[0, 2] == 1)
                {
                    return "A2";
                }

                else if (gameBoard[0, 1] == 1 && gameBoard[1, 0] == 1)
                {
                    return "A1";
                }
                else if(gameBoard[0, 1] == 1 && gameBoard[1, 2] == 1)
                {
                    return "A3";
                }
                else if(gameBoard[1, 0] == 1 && gameBoard[2, 1] == 1)
                {
                    return "C1";
                }
                else if(gameBoard[1, 2] == 1 && gameBoard[2, 1] == 1)
                {
                    return "C3";
                }
                else
                {
                    if (IsEmptySpace(1, 1))
                    {
                        return "B2";
                    }
                    else
                    {
                        for (int y = 0; y < 3; y+=2)
                        {
                            for (int x = 0; x < 3; x += 2)
                            {
                                if(IsEmptySpace(y, x))
                                {
                                    switch (y)
                                    {
                                        case 0:
                                            letter = 'A';
                                            return $"{letter}{x + 1}";
                                        case 2:
                                            letter = 'C';
                                            return $"{letter}{x + 1}";
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //fifth move
            if (turnNumber == 4)
            {
                if(gameBoard[2,2] == 2)
                {
                    if (IsEmptySpace(0, 2))
                    {
                        return "A3";
                    }
                    else
                    {
                        return "C1";
                    }
                }
            }
            //if at end of game and no other alternatives exist and it doesn't block or win
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (IsEmptySpace(y, x))
                    {
                        switch (y)
                        {
                            case 0:
                                letter = 'A';
                                break;
                            case 1:
                                letter = 'B';
                                break;
                            case 2:
                                letter = 'C';
                                break;
                            default:
                                letter = 'D';
                                break;
                        }
                        return $"{letter}{x + 1}";
                    }
                }
            }
            return "D4";
        }

        public gameStatus WinDrawContinue()
        {
            if (turnNumber > 3)
            {
                //test from top left corner for win
                if (gameBoard[0, 0] > 0 && gameBoard[0, 0] == gameBoard[0, 1] && gameBoard[0, 0] == gameBoard[0, 2] ||
                    gameBoard[0, 0] > 0 && gameBoard[0, 0] == gameBoard[1, 0] && gameBoard[0, 0] == gameBoard[2, 0] ||
                    gameBoard[0, 0] > 0 && gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[0, 0] == gameBoard[2, 2] ||
                    //top center
                    gameBoard[0, 1] > 0 && gameBoard[0, 1] == gameBoard[1, 1] && gameBoard[0, 1] == gameBoard[2, 1] ||
                    //top right
                    gameBoard[0, 2] > 0 && gameBoard[0, 2] == gameBoard[1, 2] && gameBoard[0, 2] == gameBoard[2, 2] ||
                    gameBoard[0, 2] > 0 && gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[0, 2] == gameBoard[2, 0] ||
                    //middle left
                    gameBoard[1, 0] > 0 && gameBoard[1, 0] == gameBoard[1, 1] && gameBoard[1, 0] == gameBoard[1, 2] ||
                    //bottom left
                    gameBoard[2, 0] > 0 && gameBoard[2, 0] == gameBoard[2, 1] && gameBoard[2, 0] == gameBoard[2, 2])
                {
                    if(firstGoesFirst && turnNumber % 2 == 0 || !firstGoesFirst && turnNumber % 2 != 0)
                    {
                        Console.WriteLine($"\n{opponent} Wins!\n\nPress Any Key to Continue.");
                        Console.ReadKey();
                        return gameStatus.PLAYER_2Wins;
                    }
                    else
                    {
                        Console.WriteLine("\nPLAYER_1 Wins!\n\nPress Any Key to Continue.");
                        Console.ReadKey();
                        return gameStatus.PLAYER_1Wins;    
                    }
                }
                if(turnNumber == 9)
                {
                    Console.WriteLine("\nThis game ends in a Draw.\n\nPress Any Key to Continue");
                    Console.ReadKey();
                    return gameStatus.Draw;
                }
            }

            return gameStatus.Continue;
                
        }
    }
}