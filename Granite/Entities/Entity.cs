using Granite.Graphics;

namespace Granite.Entities;

public abstract class Entity : Object2D, IInteractive
{
    private bool _isIdle = true;
    public void Unfocus()
    {
        if (!_isIdle)
        {
            _isIdle = true;
            SculptIdleModel();
            InvokeEntireModelChangedEvent();
        }
    }

    public void Focus()
    {
        if (_isIdle)
        {
            _isIdle = false;
            SculptHoveredModel();
            InvokeEntireModelChangedEvent();
        }
    }

    public override void SculptModel()
    {
        if (_isIdle) SculptIdleModel();
        else SculptHoveredModel();
    }

    public abstract void SculptIdleModel();
    public abstract void SculptHoveredModel();

    public abstract void ProcessPressedKey(ConsoleKey key);
}
