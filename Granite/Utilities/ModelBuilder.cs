using Granite.Graphics;

namespace Granite.Utilities;

public static class ModelBuilder
{
    public static void SculptRectangle(this Cell[,] model, RectMath.Rect section, Cell cell)
    {
        for (int i = section.Y1; i <= section.Y2; i++)
        {
            for (int j = section.X1; j <= section.X2; j++)
            {
                model[i, j] = cell;
            }
        }
    }
}