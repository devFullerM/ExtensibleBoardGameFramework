using BoardGames.Interfaces;

namespace BoardGames.Core.Commands
{
    // Base class for all commands used by the HistoryManager
    public abstract class GameCommand : IGameCommand
    {
        // Target cell
        protected readonly int Row;
        protected readonly int Column;
        // Context for command
        protected readonly IBoard Board;
        protected readonly BoardGame Game;
        protected readonly IGameRules Rules;

        // Constructor for target cell and context for this command's Execute/Undo
        protected GameCommand(int row, int column, IBoard board, BoardGame game, IGameRules rules)
        {
            Row = row;
            Column = column;
            Board = board;
            Game = game;
            Rules = rules;
        }

        protected void SwitchPlayer()
        {
            Game.SwitchPlayer();
        }

        public abstract void Execute();
        public abstract void Undo();
    }
}