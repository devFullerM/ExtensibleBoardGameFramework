namespace BoardGames.Interfaces
{
    public interface IBoard // 2D board abstraction, cells store integer values.
    {
        int Rows { get; }
        int Columns { get; }

        int GetCell(int row, int column);
        void SetCell(int row, int column, int value);
        void DisplayBoard();
    }
}