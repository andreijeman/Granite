namespace Granite.Helpers;

public class Controller
{
    private Dictionary<ConsoleKey, Action> _keyActionDict = new Dictionary<ConsoleKey, Action>();

    public void ProcessKey(ConsoleKey key)
    {
        if (_keyActionDict.ContainsKey(key)) _keyActionDict[key]?.Invoke();
    }
    
    public void AddKeyAction(ConsoleKey key, Action action)
    {
        if (_keyActionDict.ContainsKey(key)) _keyActionDict[key] += action;
        else _keyActionDict.Add(key, action);
    }
}