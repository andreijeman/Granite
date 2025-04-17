
namespace Granite.Controls.Controllers;

public class Controller 
{
    protected Dictionary<ConsoleKey, Action?> _keyActionDict = new();
    public event Action<bool>? Selected;

    public virtual void OnKeyPressed(ConsoleKey key)
    {
        if (_keyActionDict.ContainsKey(key))
        {
            _keyActionDict[key]?.Invoke();
        }
    }

    public virtual void OnSelected(bool isSelected) => Selected?.Invoke(isSelected);

    public void AddKeyAction(ConsoleKey key, Action action)
    {
        if (_keyActionDict.ContainsKey(key))
        {
            _keyActionDict[key] += action;
        }
        else
        {
            _keyActionDict.Add(key, action);
        }
    }

    public void RemoveKeyAction(ConsoleKey key, Action action)
    {
        if (_keyActionDict.ContainsKey(key) && _keyActionDict[key] != null)
        {
            _keyActionDict[key] -= action;
        }
        else
        {
            _keyActionDict.Remove(key);
        }
    }
}
