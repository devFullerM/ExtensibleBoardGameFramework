using System.Collections.Generic;
using BoardGames.Players;

namespace BoardGames.Interfaces
{
    public interface IGameRules // Rules contract: move validation, move generation, win/draw checks and piece availability per player
    {
        bool IsValidMove(
            int row,
            int column,
            IPiece piece,
            IBoard board,
            Player currentPlayer
        );

        List<(int, int, IPiece)> GetValidMoves(
            IBoard board,
            Player currentPlayer
        );

        bool CheckWin(IBoard board);
        bool IsDraw(IBoard board);

        List<IPiece> GetAvailablePieces(
            Player player
        );
    }
}