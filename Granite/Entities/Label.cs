using Granite.Graphics;
using Granite.Utilities;

namespace Granite.Entities;

public class Label : Object2D
{
    public RgbColor Color { get; set; } = new RgbColor { R = 255, G = 51, B = 255 };

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

    public override void SculptModel()
    {
        Model.SculptText(new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 }, _text, _textColor);
    }
}
