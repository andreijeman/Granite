namespace Granite.Engine.Primitives;

public readonly record struct Rect(Point Origin, Size Size)
{
    public Point P1 => Origin;
    public Point P2 => new(Origin.X + Size.Width - 1, Origin.Y + Size.Height - 1);
}