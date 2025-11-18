using BoardGames.Interfaces;

namespace BoardGames.Games.NumericalTicTacToe
{
    public class NumericalTicTacToePiece : IPiece // Numerical piece. Keeps engine piece-agnostic
    {
        private readonly int _number;

        public NumericalTicTacToePiece(int number) // stores numeric value for this piece
        {
            _number = number;
        }

        public int Value // the number placed on the board
        {
            get { return _number; }
        }
    }
}