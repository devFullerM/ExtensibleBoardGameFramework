using BoardGames.Players;

namespace BoardGames.Games.NumericalTicTacToe
{
    // Numerical-specific computer player - inherits random move from ComputerPlayer
    public class NumericalComputerPlayer : ComputerPlayer
    {
        public NumericalComputerPlayer(int id) : base(id) { }

        // A different strategy other than random move could be added here later by overriding GenerateMove() with different logic
    
    }
}