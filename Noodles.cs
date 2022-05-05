using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Noodles
    {
        static void Main(string[] args)
        {
            bool keepPlaying = true;
            string playingQuestion = "";
            int numberOfPlayers = 0;
            bool firstGoesFirst = true;
            bool twoGoodAnswers;
            TicTacToe game;

            while (keepPlaying)
            {
                twoGoodAnswers = false;
                Console.Clear();
                Console.Write("\nHow many players will be playing?(1 / 2): ");
                try
                {
                    var answer = Console.ReadLine();
                    if (answer != null && (int.Parse(answer) == 1 || int.Parse(answer) == 2))
                    {
                        numberOfPlayers = int.Parse(answer);
                    }
                    else
                    {
                        throw new Exception("\nPlease enter only 1 or 2.");
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                if (numberOfPlayers == 1 || numberOfPlayers == 2)
                {
                    Console.Clear();
                    Console.Write("\nWill PLAYER_1 go first?(Y / N): ");
                    try
                    {
                        var answer = Console.ReadLine();
                        if (answer == "Y" || answer == "y")
                        {
                            firstGoesFirst = true;
                            twoGoodAnswers = true;
                        }
                        else if (answer == "N" || answer == "n")
                        {
                            twoGoodAnswers = true;
                            firstGoesFirst = false;
                        }
                        else
                        {
                            throw new Exception("\nLet's try this again from the top...\n\nPress any key to continue");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
                if (twoGoodAnswers)
                {
                    game = new TicTacToe(numberOfPlayers, firstGoesFirst);
                    game.PlayGame();
                    Console.Write("\nWould you like to play another game?(Y / N)");
                    while(playingQuestion != "Y" || playingQuestion != "y")
                    {
                        playingQuestion = Console.ReadLine();
                        if (playingQuestion != null)
                        {
                            playingQuestion = char.ToUpper(playingQuestion[0]).ToString();
                            if(playingQuestion == "N")
                            {
                                keepPlaying = false;
                                break;
                            }
                        }
                    }
                }

            }
        }

    }
}
