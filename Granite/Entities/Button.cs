using Granite.Controls;
using Granite.Graphics;
namespace Granite.Entities;

public class Button : InteractiveObject
{
    public event Action? PressedEvent;
    public RgbColor IdleColor { get; set; } = new RgbColor { R = 255, G = 51, B = 255 };
    public RgbColor HoveredColor { get; set; } = new RgbColor { R = 153, G = 51, B = 255 };

    public override void SculptIdleModel()
    {
        Model.SculptFilledRectangle(
            new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 },
            new Cell() { Character = ' ', BackgroundRgbColor = IdleColor }
        );
    }

    public override void SculptHoveredModel()
    {
        Model.SculptFilledRectangle(
            new Rect() { X1 = 0, Y1 = 0, X2 = Width - 1, Y2 = Height - 1 },
            new Cell() { Character = ' ', BackgroundRgbColor = HoveredColor }
        );
    }

    public override void ProcessPressedKey(ConsoleKey key)
    {
        if (key == ConsoleKey.Enter) PressedEvent?.Invoke();
    }
}
