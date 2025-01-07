using Granite.Interfaces;

namespace Granite.Utilities;

public static class ConsoleKeyListener
{
    //private static ILogger _logger { get; set; }
    
    public static event Action<ConsoleKey>? ConsoleKeyEvent;

    private static readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    private static void Listen(CancellationToken cancellationToken)
    {
        ConsoleKey key;
        
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                    ConsoleKeyEvent?.Invoke(key);
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.ToString());
        }
    }
    
    public static void Start()
    {
        Task.Run(() => Listen(_cancellationTokenSource.Token));
    }
    
    public static void Stop()
    {
        _cancellationTokenSource.Cancel();
    }
}