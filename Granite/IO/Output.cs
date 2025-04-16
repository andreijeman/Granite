using Granite.Graphics.Components;
using Granite.Graphics.EventArgs;
using Granite.Graphics.Objects;

namespace Granite.IO;

public static class Output
{
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);

    public static void OnDrawRequested(BaseObject sender, DrawEventArgs args)
    {
        _semaphore.Wait();
        //Console.ReadKey(true);
        try
        {
            for (int i = args.Section.Y1; i <= args.Section.Y2; i++)
            {
                Console.SetCursorPosition(args.Left, args.Top++);

                for (int j = args.Section.X1; j <= args.Section.X2; j++)
                {
                    Cell cell = args.Model.Data[i, j];

                    Console.Write(
                        RgbToAnsiESForeground(cell.Foreground.R, cell.Foreground.G, cell.Foreground.B) +
                        RgbToAnsiESBackground(cell.Background.R, cell.Background.G, cell.Background.B) +
                        cell.Character);
                }
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public static string RgbToAnsiESForeground(int r, int g, int b)
    {
        return $"\u001b[38;2;{r};{g};{b}m";
    }

    public static string RgbToAnsiESBackground(int r, int g, int b)
    {
        return $"\u001b[48;2;{r};{g};{b}m";
    }
}
