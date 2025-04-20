using Granite.Graphics.Components;

namespace Granite.UI.Entities.Args;

public class LabelArgs : EntityArgs
{
    public Color Color { get; set; }
    public Color TextColor { get; set; }
    public String Text { get; set; } = String.Empty;
}