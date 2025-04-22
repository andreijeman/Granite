using Granite.Graphics.Components;
using Granite.Graphics.Utilities;

namespace Granite.UI.Entities.Args;

public class ContainerArgs : EntityArgs
{
    public Color IdleColor { get; set; }
    public Color FocusedColor { get; set; }
    public Color BackgroundCOlor { get; set; }
    public ModelBuilder.Border Border { get; set; } = ModelBuilder.Assets.LineBorder;
}