using Granite.Graphics;
using Granite.Utilities;
namespace Granite.Entities;

public class Button : Entity
{
    public event Action? PressedEvent;
    public RgbColor IdleColor { get; set; } = new RgbColor { R = 255, G = 51, B = 255 };
    public RgbColor HoveredColor { get; set; } = new RgbColor { R = 153, G = 51, B = 255 };


    private string _text = "";
    private RgbColor _textColor = new RgbColor { R = 255, G = 255, B = 255 };

    public string Text 
    { 
        get => _text; 
        set  
        { 
            _text = value;
            Model.SculptText(new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 }, _text, _textColor);
        } 
    }

    public RgbColor TextColor 
    { 
        get => _textColor; 
        set 
        {
            _textColor = value;
            Model.SculptText(new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 }, _text, _textColor);
        } 
    } 

    public override void SculptIdleModel()
    {
        Model.SetBackgroundColor(new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 }, IdleColor);
    }

    public override void SculptHoveredModel()
    {
        Model.SetBackgroundColor(new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 }, HoveredColor);
    }

    public override void ProcessPressedKey(ConsoleKey key)
    {
        if (key == ConsoleKey.Enter) PressedEvent?.Invoke();
    }

    protected override void impl_InvokeModelChangedEvent()
    {
        InvokeEntireModelChangedEvent();
    }
}
