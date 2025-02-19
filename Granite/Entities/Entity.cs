using Granite.Graphics;

namespace Granite.Entities;

public abstract class Entity : Object2D, IInteractive
{
    private bool _isIdle = true;
    public abstract void ProcessPressedKey(ConsoleKey key);
 
    public void Unfocus()
    {
        if (!_isIdle)
        {
            _isIdle = true;
            SculptIdleModel();
            impl_InvokeModelChangedEvent();
        }
    }

    public void Focus()
    {
        if (_isIdle)
        {
            _isIdle = false;
            SculptHoveredModel();
            impl_InvokeModelChangedEvent();
        }
    }

    // Do you know a better name?
    protected abstract void impl_InvokeModelChangedEvent();

    public override void SculptModel()
    {
        if (_isIdle) SculptIdleModel();
        else SculptHoveredModel();
    }

    public abstract void SculptIdleModel();
    public abstract void SculptHoveredModel();

}
