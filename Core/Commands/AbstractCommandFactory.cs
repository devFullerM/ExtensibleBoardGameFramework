using BoardGames.Core;
using BoardGames.Interfaces;

namespace BoardGames.Core.Commands
{
    // Factory base for creating game specific commands.
    public abstract class AbstractCommandFactory
    {
        //Creates a "place number" command
        public abstract IGameCommand CreatePlaceCommand(int row, int column, IPiece piece, IBoard board, BoardGame game, IGameRules rules);
    }
}