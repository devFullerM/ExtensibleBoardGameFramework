using System.IO;
using System.Text;
using System.Linq;

namespace BoardGames.Core
{
    public class GameStorage
    {
        public void Save(BoardGame game, string fileName) // save current game state to a text file
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                // Current player id
                if (game.CurrentPlayer == game.PlayerOne)
                {
                    sw.WriteLine("1");
                }
                else
                {
                    sw.WriteLine("2");
                }

                // Available pieces (CSV) for each player
                sw.WriteLine(string.Join(",", game.PlayerOne.AvailablePieces.Select(p => p.Value)));
                sw.WriteLine(string.Join(",", game.PlayerTwo.AvailablePieces.Select(p => p.Value)));

                // Board rows (0 indicates empty cell)
                for (int row = 0; row < game.Board.Rows; row++)
                {
                    StringBuilder line = new StringBuilder();
                    for (int column = 0; column < game.Board.Columns; column++)
                    {
                        if (column > 0) line.Append(" ");
                        line.Append(game.Board.GetCell(row, column));
                    }
                    sw.WriteLine(line.ToString());
                }
            }
        }

        public void Load(BoardGame game, string fileName) // Load game state and clear history
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                // Load current players turn
                string currentPlayerId = sr.ReadLine()!;

                if (currentPlayerId == "1")
                {
                    game.CurrentPlayer = game.PlayerOne;
                }
                else
                {
                    game.CurrentPlayer = game.PlayerTwo;
                }

                // PlayerOne available pieces (CSV)
                string? p1Line = sr.ReadLine();
                game.PlayerOne.AvailablePieces.Clear();
                if (!string.IsNullOrEmpty(p1Line))
                {
                    string[] p1Nums = p1Line.Split(',');
                    foreach (string s in p1Nums)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            game.PlayerOne.AvailablePieces.Add(game.CreatePiece(int.Parse(s)));
                        }
                    }
                }

                // PlayerTwo available pieces (CSV)
                string? p2Line = sr.ReadLine();
                game.PlayerTwo.AvailablePieces.Clear();
                if (!string.IsNullOrEmpty(p2Line))
                {
                    string[] p2Nums = p2Line.Split(',');
                    foreach (string s in p2Nums)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            game.PlayerTwo.AvailablePieces.Add(game.CreatePiece(int.Parse(s)));
                        }
                    }
                }

                // Grid rows
                for (int row = 0; row < game.Board.Rows; row++)
                {
                    string[] numbers = sr.ReadLine()!.Split(' ');
                    for (int column = 0; column < game.Board.Columns; column++)
                    {
                        game.Board.SetCell(row, column, int.Parse(numbers[column]));
                    }
                }
            }
            game.History.Clear();
        }
    }
}