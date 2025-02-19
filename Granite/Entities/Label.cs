using Granite.Graphics;
using Granite.Utilities;

namespace Granite.Entities;

public class Label : Object2D
{

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
    private RgbColor _color { get; set; } = new RgbColor { R = 153, G = 153, B = 255 };
    public RgbColor Color
    {
        get => _color;
        set
        {
            _color = value;
            Model.SetBackgroundColor(new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 }, _color);
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

    public override void SculptModel()
    {
        Model.SetBackgroundColor(new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 }, _color);
        Model.SculptText(new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 }, _text, _textColor);
    }
}
