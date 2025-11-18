using System;
using BoardGames.Interfaces;

namespace BoardGames.Players
{
    public class ComputerPlayer : Player // Computer controlled player - default strategy picks a random valid move
    {
        private static readonly Random _random = new Random();

        public ComputerPlayer(int id) : base(id) { }

        // Select a legal move for this Computer Player
        public virtual (int row, int column, IPiece piece) SelectMove(IBoard board, IGameRules rules)
        {
            return GenerateMove(board, rules, this);
        }

        // strategy hook - override to implement different computer move logic
        protected virtual (int row, int column, IPiece piece) GenerateMove(IBoard board, IGameRules rules, Player currentPlayer)
        {
            var validMoves = rules.GetValidMoves(board, currentPlayer);
            // validMoves.Count == 0 check could be added here for other games, no such possible scenario in NTTT
            var pick = validMoves[_random.Next(validMoves.Count)];
            return (pick.Item1, pick.Item2, pick.Item3);
        }
    }
}