using System;
using System.Text;
using BoardGames.Interfaces;

namespace BoardGames.Boards
{
    // Grid based board used by numeric games
    public class GridBoard : IBoard
    {
        private int[,] grid;
        // Get board dimensions
        public int Rows
        {
            get { return grid.GetLength(0); }
        }

        public int Columns
        {
            get { return grid.GetLength(1); }
        }
        // Create board
        public GridBoard(int rows, int columns)
        {
            grid = new int[rows, columns];
        }

        // Read/write a single cell value
        public int GetCell(int row, int column)
        {
            return grid[row, column];
        }
        public void SetCell(int row, int column, int value)
        {
            grid[row, column] = value;
        }

        // Prints the board using Unicode
        public void DisplayBoard()
        {
            StringBuilder top = new StringBuilder("┌");
            for (int c = 0; c < Columns - 1; c++)
            {
                top.Append("─┬");
            }
            top.Append("─┐");
            Console.WriteLine(top.ToString());

            for (int row = 0; row < Rows; row++)
            {
                StringBuilder line = new StringBuilder("│");
                for (int column = 0; column < Columns; column++)
                {
                    string cell;
                    if (grid[row, column] == 0)
                    {
                        cell = " ";
                    }
                    else
                    {
                        cell = grid[row, column].ToString();
                    }
                    line.Append(cell + "│");
                }
                Console.WriteLine(line.ToString());

                if (row < Rows - 1)
                {
                    StringBuilder mid = new StringBuilder("├");
                    for (int c = 0; c < Columns - 1; c++)
                    {
                        mid.Append("─┼");
                    }
                    mid.Append("─┤");
                    Console.WriteLine(mid.ToString());
                }
            }

            StringBuilder bottom = new StringBuilder("└");
            for (int c = 0; c < Columns - 1; c++)
            {
                bottom.Append("─┴");
            }
            bottom.Append("─┘");
            Console.WriteLine(bottom.ToString());
        }
    }
}