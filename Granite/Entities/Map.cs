using Granite.Graphics;
using Granite.Utilities;

namespace Granite.Entities;

public class Map : Frame, IInteractive
{
    public ConsoleKey UpKey { get; set; } = ConsoleKey.UpArrow;
    public ConsoleKey DownKey { get; set; } = ConsoleKey.DownArrow;
    public ConsoleKey LeftKey { get; set; } = ConsoleKey.LeftArrow;
    public ConsoleKey RightKey { get; set; } = ConsoleKey.RightArrow;

    public override void SculptModel()
    {
        base.SculptModel();
        //Model.SculptBorder(new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 }, ModelBuilder.Assets.LineBorder, new RgbColor { R = 153, G = 51, B = 255 });

    }

    public void Focus()
    {
        Model.SculptBorder(new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 }, ModelBuilder.Assets.FatBorder, new RgbColor { R = 153, G = 51, B = 255 });
        InvokeEntireModelChangedEvent();
    }

    public void Unfocus()
    {
        Model.SculptBorder(new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 }, ModelBuilder.Assets.FatBorder, new RgbColor { R = 153, G = 153, B = 255 });
        InvokeEntireModelChangedEvent();
    }

    public void ProcessPressedKey(ConsoleKey key)
    {
        if (key == UpKey) LocalTop--;
        else if (key == DownKey) LocalTop++;
        else if (key == LeftKey) LocalLeft--;
        else if (key == RightKey) LocalLeft++;
    }
}
