namespace Granite.Graphics;

public static class ModelBuilder
{
    public static void SculptRectangle(this Cell[,] model, Rect section, Cell cell)
    {
        for (int i = section.X1; i <= section.X2; i++)
        {
            model[section.Y1, i] = cell;
            model[section.Y2, i] = cell;
        }

        for (int i = section.Y1 + 1; i < section.Y2; i++)
        {
            model[i, section.X1] = cell;
            model[i, section.X2] = cell;
        }
    }

    public static void SculptFilledRectangle(this Cell[,] model, Rect section, Cell cell)
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