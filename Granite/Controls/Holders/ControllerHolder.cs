using Granite.Controls.Controllers;

namespace Granite.Controls.Holders;

public class ControllerHolder : Controller
{
    private ControllerHolder? _parent;
    private List<Controller> _controllers = new();

    private Controller? _currentCtrl;
    private int _currentCtrlIndex = -1;

    private ConsoleKey _focusKey = ConsoleKey.Enter;
    private ConsoleKey _exitKey = ConsoleKey.Escape;
    private ConsoleKey _nextKey = ConsoleKey.Tab;

    public ControllerHolder()
    {
        _isFocused = true;
        
        AddKeyAction(_focusKey, OnFocusKey);
        AddKeyAction(_exitKey, OnExitKey);
        AddKeyAction(_nextKey, OnNextKey);
    }

    public override void OnKeyPressed(ConsoleKey key)
    {
        if (_isFocused) base.OnKeyPressed(key);
        _currentCtrl?.OnKeyPressed(key);
    }
    private void OnFocusKey()
    {
        if(_parent is not null) _parent.IsFocused = false;
        _isFocused = true;
    }

    private void OnExitKey()
    {
        _currentCtrl?.OnFocused(false);
        _currentCtrl = null;
        _currentCtrlIndex = -1;
        
        if (_parent is not null)
        {
            _isFocused = false;
            _parent.IsFocused = true;
        }
    }

    private void OnNextKey()
    {
        if (_currentCtrlIndex < _controllers.Count - 1) _currentCtrlIndex++;
        else _currentCtrlIndex = 0;

        _currentCtrl?.OnFocused(false);
        _currentCtrl = _controllers[_currentCtrlIndex];
        _currentCtrl.OnFocused(true);
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
            holder.IsFocused = false;
        }
    }

    public ControllerHolder? Parent { get => _parent; set => _parent = value; }

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
