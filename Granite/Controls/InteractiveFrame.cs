
using Granite.Controls;
using Granite.Graphics;

namespace Granite.Entities;

public class InteractiveFrame : Frame, IInteractive
{
    private static Stack<InteractiveFrame> _frames = new Stack<InteractiveFrame>();
    private List<IInteractive> _objects = new List<IInteractive>();

    private int _currentObjIndex = -1;
    private IInteractive? _currentObj;

    public ConsoleKey MoveNextKey { get; set; } = ConsoleKey.Tab;
    public ConsoleKey ExitKey { get; set; } = ConsoleKey.Escape;
    public ConsoleKey SelectKey { get; set; } = ConsoleKey.Enter;

    public InteractiveFrame()
    {
        if (_frames.Count == 0) _frames.Push(this);
    }

    public void AddFront(InteractiveObject obj)
    {
        if (!_objects.Contains(obj))
        {
            _objects.Add(obj);
            base.AddFront(obj);
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
            if(_frames.Count > 1)
            {
                this.Unbind();
                _frames.Pop();
                _frames.Last().Bind();
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
            _frames.Last().Unbind();
            _frames.Push(this);
            Bind();
        }
    }

    public void Bind()
    {
        ConsoleKeyListener.KeyPressedEvent += ProcessPressedKeyHelper;
    }

    public void Unbind()
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
