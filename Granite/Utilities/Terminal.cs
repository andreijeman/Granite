using Granite.Entities;
using Granite.Utilities.Math;

namespace Granite.Utilities;

public static class Terminal
{
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);

    private static ConsoleColor _lastFgColor;
    private static ConsoleColor _lastBgColor;
    
    public static void PrintEntityModelPart(Entity interceptor, Entity sender, Rect part, Vector2 absolutePosition)
    {
        _semaphore.Wait();

        try
        {
            for (int y = part.Pos.Y; y < part.Pos.Y + part.Size.Y; y++)
            {
                Console.SetCursorPosition(absolutePosition.X, absolutePosition.Y);
                for (int x = part.Pos.X; x < part.Pos.X + part.Size.X; x++)
                {
                    if (_lastFgColor != sender.Model[y, x].ForegroundColor)
                    {
                        _lastFgColor = sender.Model[y, x].ForegroundColor;
                        Console.ForegroundColor = _lastFgColor;
                    }
                
                    if (_lastBgColor != sender.Model[y, x].BackgroundColor)
                    {
                        _lastBgColor = sender.Model[y, x].BackgroundColor;
                        Console.BackgroundColor = _lastBgColor;
                    }

                    Console.Write(sender.Model[y, x].Character);
                    //Thread.Sleep(20);
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