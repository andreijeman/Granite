using System.Numerics;
using Granite.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace Granite.Utilities;

public static class ModelBuilder
{
    public static void Init(this Cell[,] model, int width, int height, char character = ' ', RgbColor foregroundColor = default, RgbColor backgroundColor = default)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                model[i, j] = new Cell { Character = character, ForegroundRgbColor = foregroundColor, BackgroundRgbColor = backgroundColor };
            }
        }
    }

    public static void SetChessboardColors(this Cell[,] model, Rect section, RgbColor color1, RgbColor color2)
    {
        for (int i = section.Y1; i <= section.Y2; i++)
        {
            for (int j = section.X1; j <= section.X2 / 2; j++)
            {
                RgbColor color = (i + j) % 2 == 0 ? color1 : color2;

                model[i, 2 * j].BackgroundRgbColor = color;
                model[i, 2 * j + 1].BackgroundRgbColor = color;
            }
        }
    }

    public static void SculptRectangle(this Cell[,] model, Rect section, Cell cell)
    {
        for (int i = section.Y1; i <= section.Y2; i++)
        {
            for (int j = section.X1; j <= section.X2; j++)
            {
                model[i, j] = cell;
            }
        }
    }

    public static void SculptRectangle(this Cell[,] model, Rect section, Char character)
    {
        for (int i = section.Y1; i <= section.Y2; i++)
        {
            for (int j = section.X1; j <= section.X2; j++)
            {
                model[i, j].Character = character;
            }
        }
    }

    public static void SetForegoundColor(this Cell[,] model, Rect section, RgbColor color)
    {
        for (int i = section.Y1; i <= section.Y2; i++)
        {
            for (int j = section.X1; j <= section.X2; j++)
            {
                model[i, j].ForegroundRgbColor = color;
            }
        }
    }

    public static void SetBackgroundColor(this Cell[,] model, Rect section, RgbColor color)
    {
        for (int i = section.Y1; i <= section.Y2; i++)
        {
            for (int j = section.X1; j <= section.X2; j++)
            {
                model[i, j].BackgroundRgbColor = color;
            }
        }
    }

    public static void SculptBorder(this Cell[,] model, Rect section, Border border, RgbColor color)
    {
        model.SetForegoundColor(section, color);

        model.SculptRectangle(new Rect { X1 = section.X1 + 1, Y1 = section.Y1, X2 = section.X2 - 1, Y2 = section.Y1 }, border.Top);
        model.SculptRectangle(new Rect { X1 = section.X1 + 1, Y1 = section.Y2, X2 = section.X2 - 1, Y2 = section.Y2 }, border.Bottom);
        model.SculptRectangle(new Rect { X1 = section.X1, Y1 = section.Y1 + 1, X2 = section.X1, Y2 = section.Y2 - 1 }, border.Left);
        model.SculptRectangle(new Rect { X1 = section.X2, Y1 = section.Y1 + 1, X2 = section.X2, Y2 = section.Y2 - 1 }, border.Right);

        model[section.Y1, section.X1].Character = border.LeftTop;
        model[section.Y1, section.X2].Character = border.RightTop;
        model[section.Y2, section.X2].Character = border.RightBottom;
        model[section.Y2, section.X1].Character = border.LeftBottom;
    }

    public static void SculptText(this Cell[,] model, Rect section, string text, RgbColor color)
    {
        int t = 0;

        for (int i = section.X1; i <= section.Y2; i++)
        {
            if (t >= text.Length) break;
            for (int j = section.Y1; j <= section.X2; j++)
            {
                if (t >= text.Length) break;
                else if (text[t] == '\n') { t++; break; }

                model[i, j].Character = text[t];
                model[i, j].ForegroundRgbColor = color;
                t++;
            }
        }
    }

    public static (int X, int Y) FindCenteredTextPosition(this Rect section, string text)
    {
        int cols = section.X2 - section.X1 + 1;
        int rows = section.Y2 - section.Y1 + 1;

        if (text.Length > rows * cols) return (section.X1, section.Y1);

        int x = text.Length < cols ? section.X1 + (cols - text.Length) / 2 : section.X1;
        int y = section.Y1 + (rows - text.Length / rows) / 2;

        return (x, y);
    }

    public struct Border
    {
        public char Left, Top, Right, Bottom, LeftTop, RightTop, RightBottom, LeftBottom;

        public Border(char left, char top, char right, char bottom, char leftTop, char rightTop, char rightBottom, char leftBottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
            this.LeftTop = leftTop;
            this.RightTop = rightTop;
            this.RightBottom = rightBottom;
            this.LeftBottom = leftBottom;
        }
    }

    public static class Assets
    {
        public static Border LineBorder = new Border('│', '─', '│', '─', '┌', '┐', '┘', '└');
        public static Border DoubleLineBorder = new Border('║', '═', '║', '═', '╔', '╗', '╝', '╚');
        public static Border FatBorder = new Border('█', '▀', '█', '▄', '█', '█', '█', '█');
    }
}