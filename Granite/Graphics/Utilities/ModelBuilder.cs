using System.Reflection;
using Granite.Graphics.Components;
using Granite.Graphics.Maths;
namespace Granite.Graphics.Utilities;

public static class ModelBuilder
{
    public static Model Init(this Model model)
    {
        for (int i = 0; i < model.Height; i++)
        {
            for (int j = 0; j < model.Width; j++)
            {
                model.Data[i, j] = new Cell() { Character = ' '};
            }
        }

        return model;
    }

    public static Model Fill(this Model model, char character, Color foreground, Color background)
    {
        for (int i = 0; i < model.Height; i++)
        {
            for (int j = 0; j < model.Width; j++)
            {
                model.Data[i, j].Character = character;
                model.Data[i, j].Foreground = foreground;
                model.Data[i, j].Background = background;
            }
        }

        return model;
    }

    public static Model Fill(this Model model, Color color)
    {
        for (int i = 0; i < model.Height; i++)
        {
            for (int j = 0; j < model.Width; j++)
            {
                model.Data[i, j].Foreground = color;
                model.Data[i, j].Background = color;
            }
        }

        return model;
    }

    public static Model DrawChessboard(this Model model, Color color1, Color color2)
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

        return model;
    }
}
