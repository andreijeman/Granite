using Granite.Components;

namespace Granite.Utilities;

public static class Terminal
{
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);
    
    public static void PrintModelPart(Cell[,] model, Rect part, Vector2 position)
    {
        _semaphore.Wait();

        try
        {
            ConsoleColor lastFgColor = ConsoleColor.Black;
            ConsoleColor lastBgColor = ConsoleColor.Black;
        
            for (int y = part.Pos.Y; y < part.Pos.Y + part.Size.Y; y++)
            {
                Console.SetCursorPosition(position.X + part.Pos.X, position.Y + y );
                for (int x = part.Pos.X; x < part.Pos.X + part.Size.X; x++)
                {
                    if (lastFgColor != model[y, x].ForegroundColor)
                    {
                        lastFgColor = model[y, x].ForegroundColor;
                        Console.ForegroundColor = lastFgColor;
                    }
                
                    if (lastBgColor != model[y, x].BackgroundColor)
                    {
                        lastBgColor = model[y, x].BackgroundColor;
                        Console.BackgroundColor = lastBgColor;
                    }
                
                    Console.Write(model[y, x].Character);
                }
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public static void PrintModelParts(Cell[,] model, List<Rect> parts, Vector2 position)
    {
        foreach (Rect part in parts)
        {
            PrintModelPart(model, part, position);
        }
    }
}