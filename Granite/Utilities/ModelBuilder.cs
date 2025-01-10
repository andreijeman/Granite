using Granite.Components;
using Granite.Utilities.Math;

namespace Granite.Utilities;

public static class ModelBuilder
{
    public static void SculptRectangle(this Cell[,] model, Vector2 position, Vector2 size, Cell cell)
    {
        for (int i = 0; i < size.Y; i++)
        {
            for (int j = 0; j < size.X; j++)
            {
                model[position.Y + i, position.X + j] = cell;
            }
        }
    }
    
    public static void SculptBorder(this Cell[,] model, Vector2 position, Vector2 size, Cell cell, Assets.Border border)
    {
        cell.Character = border.Left;
        SculptRectangle(model, new Vector2(position.X, position.Y + 1), new Vector2(1, size.Y - 2), cell);        
        
        cell.Character = border.Top;
        SculptRectangle(model, new Vector2(position.X + 1, position.Y), new Vector2(size.X - 2, 1), cell);        
        
        cell.Character = border.Right;
        SculptRectangle(model, new Vector2(position.X + size.X - 1, position.Y + 1), new Vector2(1, size.Y - 2), cell);        
        
        cell.Character = border.Bottom;
        SculptRectangle(model, new Vector2(position.X + 1, position.Y + size.Y - 1), new Vector2(size.X - 2, 1), cell);
        
        cell.Character = border.LeftTop;
        model[position.Y, position.X] = cell;
        
        cell.Character = border.RightTop;
        model[position.Y, position.X + size.X - 1] = cell;
        
        cell.Character = border.RightBottom;
        model[position.Y + size.Y - 1, position.X + size.X - 1] = cell;
        
        cell.Character = border.LeftBottom;
        model[position.Y + size.Y - 1, position.X] = cell;
    }

    public static void SculptText(this Cell[,] model, Vector2 position, Vector2 size, Cell cell, string text)
    {
        int i = 0, x = 0, y = 0, length = text.Length;
        
        while (i < length)
        {
            if (x >= size.X)
            {
                y++;
                x = 0;
            }
            
            if (y >= size.Y) break;

            if (text[i] != '\n')
            {
                cell.Character = text[i];
                model[position.Y + y, position.X + x] = cell;
            }
            else 
            { 
                x = -1; 
                y++;
            }

            i++; x++;
        }
    }
    
    public static void SculptChessBoard(this Cell[,] model, Vector2 position, Vector2 size, ConsoleColor color1, ConsoleColor color2)
    {
        ConsoleColor color;
        for (int y = 0; y < size.Y; y++)
        {
            for (int x = 0; x < size.X; x++)
            {
                color = (x + y) % 2 == 0 ? color1 : color2; 
                model[y, x].Character = ' ';
                model[y, x].ForegroundColor = color;
                model[y, x].BackgroundColor = color;
            }
        }
    }
}