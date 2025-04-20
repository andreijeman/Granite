using Granite.Graphics.Components;
using Granite.Graphics.Objects;
using Granite.Graphics.Utilities;
using Granite.UI.Entities.Args;

namespace Granite.UI.Entities;

public class Label : GObject
{
    public Label(LabelArgs args)
    {
        _left = args.Left;
        _top = args.Top;
        
        _model = new Model(args.Width, args.Height)
            .Init()
            .FillBackground(args.Color)
            .InsertTextCentered(args.Text, args.TextColor);
    }
}