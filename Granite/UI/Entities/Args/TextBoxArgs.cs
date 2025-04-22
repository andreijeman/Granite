using System.Drawing;
using Granite.Graphics.Utilities;
using Color = Granite.Graphics.Components.Color;

namespace Granite.UI.Entities.Args;

public class TextBoxArgs : EntityArgs
{
    public Color IdleColor { get; set; }
    public Color FocusedColor { get; set; }
    public Color BackgroundColor { get; set; }
    public Color TextColor { get; set; }   
    public ModelBuilder.Border Border { get; set; } = ModelBuilder.Assets.LineBorder;
    public string Text { get; set; } = string.Empty;
}