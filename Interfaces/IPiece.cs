namespace BoardGames.Interfaces
{
    public interface IPiece // minimal piece contract to keep engine piece agnostic
    {
        int Value { get; } 
    }
}