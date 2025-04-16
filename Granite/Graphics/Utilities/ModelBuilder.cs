using Granite.Graphics.Components;
using Granite.Graphics.Maths;
namespace Granite.Graphics.Utilities;

public static class ModelBuilder
{
    public static Model Fill(this Model model, char character, Color foreground, Color background)
    {
        for (int i = 0; i < model.Height; i++)
        {
            for (int j = 0; j < model.Width; j++)
            {
                model.Data[i, j] = new Cell { Character = character, Foreground = foreground, Background = background };
            }
        }

        return model;
    }

    public static Model FillDefault(this Model model)
    {
        return model.Fill(' ', default, default);
    }

    public static void DrawChessboard(this Model model, Color color1, Color color2)
    {
        Color color;

        for (int i = 0; i < model.Height; i++)
        {
            for (int j = 0; j < model.Width / 2; j++)
            {
                color = (i + j) % 2 == 0 ? color1 : color2;

                model.Data[i, 2 * j].Background = color;
                model.Data[i, 2 * j + 1].Background= color;
            }
        }
    }
}
