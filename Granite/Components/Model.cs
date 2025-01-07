using Granite.Helpers;

namespace Granite.Components;

public class Model
{
    public Vector2 Size { get; init; }
    public Cell[,] Map { get; init; }

    public Model(Vector2 size)
    {
        Size = size;
        Map = new Cell[size.X, size.Y];
        
        for (int y = 0; y < size.Y; y++)
        {
            for (int x = 0; x < size.X; x++)
            {
                ConsoleColor color = (x + y) % 2 == 0 ? ConsoleColor.Magenta : ConsoleColor.Black; 
                Map[y, x].Character = ' ';
                Map[y, x].ForegroundColor = color;
                Map[y, x].BackgroundColor = color;
            }
        }
    }

    public struct Cell
    {
        public char Character;
        public ConsoleColor ForegroundColor;
        public ConsoleColor BackgroundColor;

        public Cell(char character, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Character = character;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }
    }
}