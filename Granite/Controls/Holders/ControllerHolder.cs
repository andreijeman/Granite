using Granite.Controls.Controllers;

namespace Granite.Controls.Holders;

public class ControllerHolder : Controller
{
    private ControllerHolder? _parent;
    private List<Controller> _controllers = new();

    private Controller _currentCtrl = new();
    private int _currentCtrlIndex = -1;

    private bool _isFocused;

    private ConsoleKey _focusKey = ConsoleKey.Enter;
    private ConsoleKey _exitKey = ConsoleKey.Escape;
    private ConsoleKey _nextKey = ConsoleKey.Tab;

    public ControllerHolder()
    {
        AddKeyAction(_focusKey, OnFocusKey);
        AddKeyAction(_exitKey, OnExitKey);
        AddKeyAction(_nextKey, OnNextKey);
    }

    public override void OnKeyPressed(ConsoleKey key)
    {
        if (_isFocused) base.OnKeyPressed(key);
        _currentCtrl?.OnKeyPressed(key);
    }
    protected void OnFocusKey()
    {
        _parent?.Focus(false);
        _isFocused = true;
    }

    protected void OnExitKey()
    {
        _isFocused = false;
        _parent?.Focus(true);
    }

    protected void OnNextKey()
    {
        if (_currentCtrlIndex < _controllers.Count - 1) _currentCtrlIndex++;
        else _currentCtrlIndex = 0;

        _currentCtrl.OnSelected(false);
        _currentCtrl = _controllers[_currentCtrlIndex];
        _currentCtrl.OnSelected(true);
    }

    public void Add(Controller ctrl)
    {
        if (!_controllers.Contains(ctrl))
        {
            _controllers.Add(ctrl);
        }
    }
    public void Add(ControllerHolder holder)
    {
        if (!_controllers.Contains(holder))
        {
            _controllers.Add(holder);
            holder.Parent = this;
        }
    }

    public ControllerHolder? Parent { get => _parent; set => _parent = value; }
    public void Focus(bool isFocused) => _isFocused = isFocused;


    public ConsoleKey SelectKey
    {
        get => _focusKey;
        set
        {
            RemoveKeyAction(_focusKey, OnFocusKey);
            _focusKey = value;
            AddKeyAction(_focusKey, OnNextKey);
        }
    }

    public ConsoleKey ExitKey
    {
        get => _exitKey;
        set
        {
            RemoveKeyAction(_exitKey, OnExitKey);
            _exitKey = value;
            AddKeyAction(_exitKey, OnNextKey);
        }
    }

    public ConsoleKey NextKey 
    { 
        get => _nextKey; 
        set
        {
            RemoveKeyAction(_nextKey, OnNextKey);
            _nextKey = value;
            AddKeyAction(_nextKey, OnNextKey);
        }
    }
}
