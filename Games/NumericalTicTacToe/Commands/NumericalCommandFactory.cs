using BoardGames.Core;
using BoardGames.Core.Commands;
using BoardGames.Interfaces;

namespace BoardGames.Games.NumericalTicTacToe.Commands
{
    public class NumericalCommandFactory : AbstractCommandFactory
    {
        public override IGameCommand CreatePlaceCommand(int row, int column, IPiece piece, IBoard board, BoardGame game, IGameRules rules)
        {
            return new PlaceNumberCommand(row, column, piece, board, game, rules);
        }
    }
}