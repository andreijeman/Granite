using Granite.Components;
using Granite.Entities;

namespace Granite.Utilities;

public static class Terminal
{
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);
    
    public static void PrintEntityModelPart(Entity entity, Rect part, Vector2 absolutePosition)
    {
        _semaphore.Wait();

        try
        {
            ConsoleColor lastFgColor = ConsoleColor.Black;
            ConsoleColor lastBgColor = ConsoleColor.Black;
            
            for (int y = part.Pos.Y; y < part.Pos.Y + part.Size.Y; y++)
            {
                Console.SetCursorPosition(absolutePosition.X, absolutePosition.Y++ );
                for (int x = part.Pos.X; x < part.Pos.X + part.Size.X; x++)
                {
                    if (lastFgColor != entity.Model[y, x].ForegroundColor)
                    {
                        lastFgColor = entity.Model[y, x].ForegroundColor;
                        Console.ForegroundColor = lastFgColor;
                    }
                
                    if (lastBgColor != entity.Model[y, x].BackgroundColor)
                    {
                        lastBgColor = entity.Model[y, x].BackgroundColor;
                        Console.BackgroundColor = lastBgColor;
                    }
                
                    Console.Write(entity.Model[y, x].Character);
                }
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public static void PrintEntityModelParts(Entity entity, List<Rect> parts, Vector2 absolutePosition)
    {
        foreach (Rect part in parts)
        {
            PrintEntityModelPart(entity, part, absolutePosition);
        }
    }
}