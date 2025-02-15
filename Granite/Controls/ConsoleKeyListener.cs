using System.Diagnostics;

namespace Granite.Entities;

public static class ConsoleKeyListener
{

    private static ConsoleKey _lastKey = ConsoleKey.None;
    private static Stopwatch _watch = new Stopwatch();

    public static event Action<ConsoleKey>? KeyPressedEvent;
    public static event Action<ConsoleKey>? KeyReleasedEvent;


    private static readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    private static async Task ListenAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(intercept: true).Key;
                if (key != _lastKey)
                {
                    if (_lastKey != ConsoleKey.None) KeyReleasedEvent?.Invoke(_lastKey);
                    KeyPressedEvent?.Invoke(key);
                    _lastKey = key;
                }
                _watch.Restart();
            }
            else if(_watch.ElapsedMilliseconds > 100)
            {
                _watch.Reset();
                KeyReleasedEvent?.Invoke(_lastKey);
                _lastKey = ConsoleKey.None;
            }
        }

        await Task.Delay(200);
    }

    public static void Start()
    {
        Task.Run(async () => await ListenAsync(_cancellationTokenSource.Token));
    }

    public static void Stop()
    {
        _cancellationTokenSource.Cancel();
    }
}
