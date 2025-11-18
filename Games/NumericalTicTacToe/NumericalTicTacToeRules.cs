using System.Collections.Generic;
using System.Linq;
using BoardGames.Interfaces;
using BoardGames.Players;

namespace BoardGames.Games.NumericalTicTacToe
{
    public class NumericalTicTacToeRules : IGameRules
    {
        // Valid move if in-bounds, cell empty, current player still has piece value available
        public bool IsValidMove(
            int row,
            int column,
            IPiece piece,
            IBoard board,
            Player currentPlayer
        )
        {
            if (row < 0 || row >= board.Rows) return false;
            if (column < 0 || column >= board.Columns) return false;
            if (board.GetCell(row, column) != 0) return false;

            return currentPlayer.AvailablePieces.Any(p => p.Value == piece.Value);
        }

        // Return all legal moves by pairing empty cells with remaining numbers. Used by ComputerPlayer.
        public List<(int, int, IPiece)> GetValidMoves(
            IBoard board,
            Player currentPlayer
        )
        {
            List<IPiece> availablePieces = GetAvailablePieces(currentPlayer);
            List<(int, int, IPiece)> moves = new List<(int, int, IPiece)>();

            for (int r = 0; r < board.Rows; r++)
            {
                for (int c = 0; c < board.Columns; c++)
                {
                    if (board.GetCell(r, c) == 0)
                    {
                        foreach (IPiece availPiece in availablePieces)
                        {
                            moves.Add((r, c, availPiece));
                        }
                    }
                }
            }

            return moves;
        }

        // Win if any row/col/diag has 3 placed numbers summing to 15
        public bool CheckWin(IBoard board)
        {
            // Rows
            for (int r = 0; r < board.Rows; r++)
            {
                int sum = 0;
                bool allPlaced = true;
                for (int c = 0; c < board.Columns; c++)
                {
                    int cell = board.GetCell(r, c);
                    if (cell == 0) allPlaced = false;
                    sum += cell;
                }
                if (allPlaced && sum == 15) return true;
            }

            // Columns
            for (int c = 0; c < board.Columns; c++)
            {
                int sum = 0;
                bool allPlaced = true;
                for (int r = 0; r < board.Rows; r++)
                {
                    int cell = board.GetCell(r, c);
                    if (cell == 0) allPlaced = false;
                    sum += cell;
                }
                if (allPlaced && sum == 15) return true;
            }

            // Diagonals
            int d1 = 0;
            bool allPlacedD1 = true;
            for (int i = 0; i < board.Rows; i++)
            {
                int cell = board.GetCell(i, i);
                if (cell == 0) allPlacedD1 = false;
                d1 += cell;
            }
            if (allPlacedD1 && d1 == 15) return true;

            int d2 = 0;
            bool allPlacedD2 = true;
            for (int i = 0; i < board.Rows; i++)
            {
                int cell = board.GetCell(i, board.Columns - 1 - i);
                if (cell == 0) allPlacedD2 = false;
                d2 += cell;
            }
            if (allPlacedD2 && d2 == 15) return true;

            return false;
        }

        // Draw if board full with no winning line
        public bool IsDraw(IBoard board)
        {
            for (int r = 0; r < board.Rows; r++)
            {
                for (int c = 0; c < board.Columns; c++)
                {
                    if (board.GetCell(r, c) == 0) return false;
                }
            }

            return !CheckWin(board);
        }

        public List<IPiece> GetAvailablePieces(
            Player player
        )
        {
            return player.AvailablePieces;
        }
    }
}