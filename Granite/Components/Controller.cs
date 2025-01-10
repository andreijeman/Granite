namespace Granite.Components;

public class Controller
{
    private Dictionary<ConsoleKey, ActionHandlerDelegate> _keyActionDict = new Dictionary<ConsoleKey, ActionHandlerDelegate>();

    public void ProcessKey(ConsoleKey key)
    {
        if (_keyActionDict.ContainsKey(key)) _keyActionDict[key]?.Invoke();
    }
    
    public void AddKeyAction(ConsoleKey key, ActionHandlerDelegate action)
    {
        if (_keyActionDict.ContainsKey(key)) _keyActionDict[key] += action;
        else _keyActionDict.Add(key, action);
    }
}

public delegate void ActionHandlerDelegate(params object[] args);