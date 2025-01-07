using System.Drawing;
using Granite.Components;

namespace Granite.Utilities;

public static class ModelBuilder
{
    public static void SculptRectangle(this Model model, Vector2 position, Vector2 size, Model.Cell cell)
    {
        for (int i = 0; i < size.Y; i++)
        {
            for (int j = 0; j < size.X; j++)
            {
                model.Map[position.Y + i, position.X + j] = cell;
            }
        }
    }
    
    public static void SculptBorder(this Model model, Vector2 position, Vector2 size, Model.Cell cell, Assets.Border border)
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
        model.Map[position.Y, position.X] = cell;
        
        cell.Character = border.RightTop;
        model.Map[position.Y, position.X + size.X - 1] = cell;
        
        cell.Character = border.RightBottom;
        model.Map[position.Y + size.Y - 1, position.X + size.X - 1] = cell;
        
        cell.Character = border.LeftBottom;
        model.Map[position.Y + size.Y - 1, position.X] = cell;
    }

    public static void SculptBorder(this Model model, Model.Cell cell, Assets.Border border)
    {
        model.SculptBorder(Vector2.New(0, 0), model.Size, cell, border);
    }

    public static void SculptText(this Model model, Vector2 position, Vector2 size, Model.Cell cell, string text)
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
                model.Map[position.Y + y, position.X + x] = cell;
            }
            else 
            { 
                x = -1; 
                y++;
            }

            i++; x++;
        }
    }

    public static void SculptText(this Model model, Model.Cell cell, string text)
    {
        model.SculptText(Vector2.New(1, 1), model.Size - Vector2.New(2, 2), cell, text);
    }
}