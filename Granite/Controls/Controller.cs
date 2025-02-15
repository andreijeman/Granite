namespace Granite.Entities;

public class Controller : IInteractive
{
    private Controller? _parentController;
    private List<IInteractive> _objects = new List<IInteractive>();

    private int _currentObjIndex = -1;
    private IInteractive? _currentObj;

    public ConsoleKey MoveNextKey { get; set; } = ConsoleKey.Tab;
    public ConsoleKey ExitKey { get; set; } = ConsoleKey.Escape;
    public ConsoleKey SelectKey { get; set; } = ConsoleKey.Enter;

    public Controller? ParentController { get => _parentController; set => _parentController = value; }

    public void AddFront(IInteractive obj)
    {
        if (!_objects.Contains(obj))
        {
            _objects.Add(obj);
        }
    }

    public void AddBack(IInteractive obj)
    {
        if (!_objects.Contains(obj))
        {
            _objects.Insert(0, obj);
        }
    }

    public void AddFront(Controller obj)
    {
        if (!_objects.Contains(obj))
        {
            _objects.Add(obj);
            obj.ParentController = this;
        }
    }

    public void AddBack(Controller obj)
    {
        if (!_objects.Contains(obj))
        {
            _objects.Insert(0, obj);
            obj.ParentController = this;
        }
    }

    private void ProcessPressedKeyHelper(ConsoleKey key)
    {
        if (key == MoveNextKey)
        {
            _currentObj?.Unfocus();
            MoveNext();
            _currentObj?.Focus();
        }
        else if (key == ExitKey)
        {
            if(_parentController != null)
            {
                this.UnbindToConsoleKeyListener();
                _parentController.BindToConsoleKeyListener();
            }
        }
        else
        {
            _currentObj?.ProcessPressedKey(key);
        }
    }

    public void ProcessPressedKey(ConsoleKey key)
    {
        if (key == SelectKey)
        {
            _parentController?.UnbindToConsoleKeyListener();
            BindToConsoleKeyListener();
        }
    }

    public void BindToConsoleKeyListener()
    {
        ConsoleKeyListener.KeyPressedEvent += ProcessPressedKeyHelper;
    }

    public void UnbindToConsoleKeyListener()
    {
        ConsoleKeyListener.KeyPressedEvent -= ProcessPressedKeyHelper;
    }


    private void MoveNext()
    {
        if (_currentObjIndex < _objects.Count - 1) _currentObjIndex++;
        else _currentObjIndex = 0;

        _currentObj = _objects[_currentObjIndex];
    }
    public void Focus()
    {
        throw new NotImplementedException();
    }

    public void Unfocus()
    {
        throw new NotImplementedException();
    }
}
