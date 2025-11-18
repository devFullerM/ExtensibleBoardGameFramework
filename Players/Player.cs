using System.Collections.Generic;
using BoardGames.Interfaces;

namespace BoardGames.Players
{
    public abstract class Player
    {
        public int Id { get; private set; } // unique player id (1 or 2 for NTTT)
        public List<IPiece> AvailablePieces { get; private set; }

        // constructor - initialise player id and empty piece pool (game fills via InitialisePieces())
        protected Player(int id)
        {
            Id = id;
            AvailablePieces = new List<IPiece>();
        }
    }
}