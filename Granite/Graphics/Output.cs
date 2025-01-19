namespace Granite.Graphics;

public static class Output
{
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);
    
    public static void OnModelChangedEvent(Object2D sender, Object2D.ModelChangedData data)
    {
        //Console.ReadKey();
        _semaphore.Wait();

        try
        {
            Cell cell;
            for (int i = data.SectY1; i <= data.SectY2; i++)
            {
                Console.SetCursorPosition(data.SectLeft, data.SectTop++);
                for (int j = data.SectX1; j <= data.SectX2; j++)
                {
                    cell = data.Object.Model[i, j];
                    Console.Write(
                        RgbToAnsiESForeground(cell.ForegroundRgbColor.R, cell.ForegroundRgbColor.G, cell.ForegroundRgbColor.B) +
                        RgbToAnsiESBackground(cell.BackgroundRgbColor.R, cell.BackgroundRgbColor.G, cell.BackgroundRgbColor.B) +
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