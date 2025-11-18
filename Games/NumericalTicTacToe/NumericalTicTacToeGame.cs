using System;
using BoardGames.Boards;
using BoardGames.Core;
using BoardGames.Core.Commands;
using BoardGames.Interfaces;
using BoardGames.Players;

namespace BoardGames.Games.NumericalTicTacToe
{
    public class NumericalTicTacToeGame : BoardGame
    {
        // Constructor to inject NTTT dependencies into the BoardGame engine. Creates a 3x3 grid
        public NumericalTicTacToeGame(IGameRules rules, IGameHelp gameHelp, AbstractCommandFactory commandFactory, HistoryManager history, GameStorage gameStorage)
            : base(rules, gameHelp, commandFactory, new GridBoard(3, 3), history, gameStorage)
        {
        }
        // Factory for numerical pieces
        public override IPiece CreatePiece(int value)
        {
            return new NumericalTicTacToePiece(value);
        }
        public override string[] GetStartingOrderOptions()
        {
            return new[] { "You play odd numbers (go first)", "You play even numbers (go second)" };
        }

        // Use the numerical-specific computer player
        protected override Player CreateComputerPlayer(int id)
        {
            return new NumericalComputerPlayer(id);
        }
        protected override void InitialisePieces()
        {
            PlayerOne.AvailablePieces.Add(CreatePiece(1));
            PlayerOne.AvailablePieces.Add(CreatePiece(3));
            PlayerOne.AvailablePieces.Add(CreatePiece(5));
            PlayerOne.AvailablePieces.Add(CreatePiece(7));
            PlayerOne.AvailablePieces.Add(CreatePiece(9));

            PlayerTwo.AvailablePieces.Add(CreatePiece(2));
            PlayerTwo.AvailablePieces.Add(CreatePiece(4));
            PlayerTwo.AvailablePieces.Add(CreatePiece(6));
            PlayerTwo.AvailablePieces.Add(CreatePiece(8));
        }

        public override void ShowWelcome() // welcome + rules summary
        {
            Console.WriteLine("***** Numerical Tic-Tac-Toe *****");
            Console.WriteLine("Two players place numbers on a 3×3 board.");
            Console.WriteLine("P1: odds (1,3,5,7,9); P2: evens (2,4,6,8).");
            Console.WriteLine("Each number can be used once. Any line of three placed numbers summing to 15 wins.");
            Console.WriteLine("Make a move with: place <row> <column> <number> (e.g. place 1 1 3).");
            Console.WriteLine("Type 'help' for commands or 'rules' to see this again.");
            Console.WriteLine("*********************************");
        }
    }
}