namespace Granite.IO;

public static class ConsoleKeyListener
{
    public static event Action<ConsoleKey>? KeyPressed;

    private static readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    private static async Task ListenAsync(CancellationToken cancellationToken)
    {
        ConsoleKey key;

        while (!cancellationToken.IsCancellationRequested)
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(intercept: true).Key;
                KeyPressed?.Invoke(key);   
            }

            await Task.Delay(20);
        }
    }

    public static void Start()
    {
        Task.Run(() => ListenAsync(_cancellationTokenSource.Token));
    }

    public static void Stop()
    {
        _cancellationTokenSource.Cancel();
    }
}
