using System;
using BoardGames.Core;
using BoardGames.Games.NumericalTicTacToe;
using BoardGames.Games.NumericalTicTacToe.Commands;

namespace BoardGames
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) // Loop: game selection, play, then back to menu
            {
                // Choose which game to run
                BoardGame? game = SelectGame();
                if (game == null)
                {
                    Console.WriteLine("Goodbye!");
                    return; // Exit app
                }

                game.ShowWelcome(); // Show Welcome and Rules

                // Mode selection
                int mode = ReadMenuChoice("Choose game mode:", "Human vs Computer", "Human vs Human");

                bool p1Human, p2Human;

                if (mode == 2)
                {
                    p1Human = true;
                    p2Human = true;
                    Console.WriteLine("Mode: Human vs Human. Player 1 uses odds (first); Player 2 uses evens (second).");
                }
                else
                {
                    var options = game.GetStartingOrderOptions();
                    int side = ReadMenuChoice("Choose your side:", options[0], options[1]);

                    if (side == 1)
                    {
                        p1Human = true;
                        p2Human = false;
                    }
                    else
                    {
                        p1Human = false;
                        p2Human = true;
                    }
                }
                // Create players and start main game loop
                game.SetPlayers(p1Human, p2Human);
                game.Play();

                Console.WriteLine("Returning to main menu..."); // message when game finishes
            }
        }

        // Game selection menu
        private static BoardGame? SelectGame()
        {
            Console.WriteLine("Choose game:");
            Console.WriteLine("1: Numerical Tic-Tac-Toe");
            Console.WriteLine("2: Sudoku (Coming soon!)"); // placeholder
            Console.WriteLine("3: Connect Four (Coming soon!)"); // placeholder
            Console.Write("Enter 1-3 (or 'q' to quit): ");

            string? input = Console.ReadLine()?.Trim().ToLower();
            if (input == "q") return null; // Quit

            if (!int.TryParse(input, out int gameChoice) || gameChoice < 1 || gameChoice > 3)
            {
                Console.WriteLine("Invalid choice. Please enter 1-3 (or 'q' to quit).");
                return SelectGame();
            }

            if (gameChoice != 1) // update logic when additional games are added
            {
                Console.WriteLine("Please choose a game that is currently available.");
                return SelectGame();
            }

            // Wire up NTTT game
            return new NumericalTicTacToeGame(
                new NumericalTicTacToeRules(),
                new NumericalTicTacToeHelp(),
                new NumericalCommandFactory(),
                new HistoryManager(),
                new GameStorage());
        }

        // Menu helper
        private static int ReadMenuChoice(string prompt, params string[] options)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                for (int i = 0; i < options.Length; i++)
                    Console.WriteLine($"  {i + 1}) {options[i]}");
                Console.Write($"Enter a number (1 to {options.Length}): ");
                var input = Console.ReadLine()?.Trim();
                if (int.TryParse(input, out int n) && n >= 1 && n <= options.Length)
                    return n;
                Console.WriteLine("Please enter a valid number.");
            }
        }
    }
}