namespace Granite.Engine.Primitives;

public readonly record struct Point
{
    public int Y { get; }
    public int X { get; }

    public static Point Zero { get; } = new(0, 0);
    public static Point One { get; } = new(1, 1);

    public static Point operator +(Point p1, Point p2) => new(p1.X + p2.X, p1.Y + p2.Y);
    public static Point operator -(Point p1, Point p2) => new(p1.X - p2.X, p1.Y - p2.Y);
    
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Rect ToRect(Size size) => new(this, size);
    public Point Translate(Point offset) => this + offset;
    public Point Rebase(Point oldOrigin, Point newOrigin) => this + oldOrigin - newOrigin;
}