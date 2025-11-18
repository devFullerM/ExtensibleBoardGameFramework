using System.Collections.Generic;
using System.Linq;
using BoardGames.Core;
using BoardGames.Core.Commands;
using BoardGames.Interfaces;

namespace BoardGames.Games.NumericalTicTacToe.Commands
{
    public class PlaceNumberCommand : GameCommand
    {
        private readonly IPiece Piece;

        // Constructor - binds piece and target cell; calls GameCommand base constructor with shared context
        public PlaceNumberCommand(int row, int column, IPiece piece, IBoard board, BoardGame game, IGameRules rules)
            : base(row, column, board, game, rules)
        {
            Piece = piece;
        }

        public override void Execute() // place piece on the board and remove its value from current player's available pieces then switch player
        {
            List<IPiece> available = Rules.GetAvailablePieces(Game.CurrentPlayer);
            Board.SetCell(Row, Column, Piece.Value);
            available.RemoveAll(p => p.Value == Piece.Value);
            SwitchPlayer();
        }

        public override void Undo() // switch back to player who placed piece and return piece to player's available numbers
        {
            SwitchPlayer();
            Board.SetCell(Row, Column, 0);

            List<IPiece> pool = Rules.GetAvailablePieces(Game.CurrentPlayer);
            if (!pool.Any(p => p.Value == Piece.Value))
            {
                pool.Add(Game.CreatePiece(Piece.Value));
            }
        }
    }
}