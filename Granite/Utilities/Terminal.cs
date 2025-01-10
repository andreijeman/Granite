using Granite.Entities;
using Granite.Utilities.Math;

namespace Granite.Utilities;

public static class Terminal
{
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);
    
    public static void PrintEntityModelPart(Entity interceptor, Entity sender, Rect part, Vector2 absolutePosition)
    {
        _semaphore.Wait();

        try
        {
            ConsoleColor lastFgColor = ConsoleColor.Black;
            ConsoleColor lastBgColor = ConsoleColor.Black;
            
            for (int y = part.Pos.Y; y < part.Pos.Y + part.Size.Y; y++)
            {
                Console.SetCursorPosition(absolutePosition.X, absolutePosition.Y);
                for (int x = part.Pos.X; x < part.Pos.X + part.Size.X; x++)
                {
                    if (lastFgColor != sender.Model[y, x].ForegroundColor)
                    {
                        lastFgColor = sender.Model[y, x].ForegroundColor;
                        Console.ForegroundColor = lastFgColor;
                    }
                
                    if (lastBgColor != sender.Model[y, x].BackgroundColor)
                    {
                        lastBgColor = sender.Model[y, x].BackgroundColor;
                        Console.BackgroundColor = lastBgColor;
                    }
                
                    Console.Write(sender.Model[y, x].Character);
                }

                absolutePosition.Y++;
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
}