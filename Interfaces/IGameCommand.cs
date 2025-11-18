namespace BoardGames.Interfaces
{
    public interface IGameCommand // Command interface - each command must support Execute and undo
    {
        void Execute();
        void Undo();
    }
}