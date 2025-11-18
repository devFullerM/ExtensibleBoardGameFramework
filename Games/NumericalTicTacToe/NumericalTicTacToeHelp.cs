using System;
using BoardGames.Interfaces;

namespace BoardGames.Games.NumericalTicTacToe
{
    public class NumericalTicTacToeHelp : IGameHelp
    {
        public void ShowHelp() // List available commands for NTTT
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("  place r c n  - e.g. place 1 1 3; places the number 3 at row 1, column 1");
            Console.WriteLine("  undo         - go back to the previous human turn");
            Console.WriteLine("  redo         - go forward to the next human turn");
            Console.WriteLine("  reroll       - Human vs Computer only: try a different computer move");
            Console.WriteLine("  save <name>  - save to a file (e.g., save game1.txt)");
            Console.WriteLine("  load <name>  - load from a file (e.g., load game1.txt)");
            Console.WriteLine("  rules        - show game rules");
            Console.WriteLine("  help         - show commands and examples");
            Console.WriteLine("  quit         - quit the game");
        }
    }
}