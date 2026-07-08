namespace Granite.Engine.Primitives;

public readonly record struct Point(int X, int Y)
{
    public static Point operator +(Point p1, Point p2) => new(p1.X + p2.X, p1.Y + p2.Y);
    public static Point operator -(Point p1, Point p2) => new(p1.X - p2.X, p1.Y - p2.Y);

    public static Point Zero { get; } = new(0, 0);
    public static Point One { get; } = new(1, 1);
}