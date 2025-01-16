namespace Granite.Graphics;

public static class Output
{
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);
    
    public static void OnModelChangedEvent(Object2D.ModelChangedData data)
    {
        _semaphore.Wait();

        try
        {
            Cell cell;
            for (int i = data.Y1; i <= data.Y2; i++)
            {
                Console.SetCursorPosition(data.Left, data.Top++);
                for (int j = data.X1; j <= data.X2; j++)
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