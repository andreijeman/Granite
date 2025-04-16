using Granite.Graphics.Components;
using Granite.Graphics.EventArgs;
using Granite.Graphics.Maths;
using static System.Collections.Specialized.BitVector32;

namespace Granite.Graphics.Objects;

public class BaseObject 
{
    protected Model _model = new(0, 0);

    protected int _left;
    protected int _top;

    public event Action<BaseObject, DrawEventArgs>? DrawRequested;
    public event Action<BaseObject>? PositionChanged;
    public event Action<BaseObject>? SizeChanged;

    public virtual Model Model
    {
        get => _model;
        set
        {
            _model = value;
            InvokeSizeChanged();
        }
    }

    public virtual int Left
    { 
        get => _left;
        set
        {
            _left = value;
            InvokePositionChanged();
        }
    }
    public virtual int Top
    {
        get => _top;
        set
        {
            _top = value;
            InvokePositionChanged();
        }
    }

    public virtual void Draw()
    {
        Draw(new Rect
        {
            X1 = 0,
            Y1 = 0,
            X2 = _model.Width - 1,
            Y2 = _model.Height - 1
        });
    }

    public virtual void Draw(Rect section)
    {
        InvokeDrawRequested(new DrawEventArgs
        {
            Model = _model,
            Section = section,
            Left = _left,
            Top = _top
        });
    }

    protected void InvokeDrawRequested(DrawEventArgs args) => DrawRequested?.Invoke(this, args);
    protected void InvokePositionChanged() => PositionChanged?.Invoke(this);
    protected void InvokeSizeChanged() => SizeChanged?.Invoke(this);

    public int Width { get => _model.Width; }
    public int Height { get => _model.Height; }
}
