using Granite.Graphics.Components;
using Granite.Graphics.Utilities;
using Granite.UI.Entities.Args;

namespace Granite.UI.Entities;

public class Button : Entity
{
    private Color _color;
    private Color _focusedColor;
    
    private Color _textColor;
    private String _text;

    public Button(ButtonArgs args) : base(args)
    {
        _color = args.Color;
        _focusedColor = args.FocusedColor;
        _textColor = args.TextColor;
        _text = args.Text;
        
        SculptModel();
    }
    
    protected override void SculptModel()
    {
        if (IsFocused)
        {
            GObject.Model
                .FillBackground(_focusedColor)
                .InsertTextCentered(_text, _textColor);
        }
        else
        {
            GObject.Model 
                .FillBackground(_color)
                .InsertTextCentered(_text, _textColor);
        }
    }

    protected override void OnFocused(bool isFocused)
    {
        if (isFocused)
            GObject.Model.FillBackground(_focusedColor);
        else
            GObject.Model.FillBackground(_color);

        GObject.Draw();
    }
}