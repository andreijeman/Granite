using Granite.Components;

namespace Granite.Utilities;

public static class Terminal
{
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);
    
    public static void TryPrintModelPart(Model model, Vector2 position, Vector2 beginCoord, Vector2 size)
    {
        _semaphore.Wait();

        try
        {
            PrintModelPart(model, position, beginCoord, size);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private static void PrintModelPart(Model model, Vector2 position, Vector2 beginCoord, Vector2 size)
    {
        ConsoleColor lastFgColor = ConsoleColor.Black;
        ConsoleColor lastBgColor = ConsoleColor.Black;
        
        for (int y = beginCoord.Y; y < size.Y; y++)
        {
            Console.SetCursorPosition(position.X, position.Y++);
            for (int x = beginCoord.X; x < size.X; x++)
            {
                if (lastFgColor != model.Map[y, x].ForegroundColor)
                {
                    lastFgColor = model.Map[y, x].ForegroundColor;
                    Console.ForegroundColor = lastFgColor;
                }
                
                if (lastBgColor != model.Map[y, x].BackgroundColor)
                {
                    lastBgColor = model.Map[y, x].BackgroundColor;
                    Console.BackgroundColor = lastBgColor;
                }
                
                Console.Write(model.Map[y, x].Character);
            }
        }
    }
}