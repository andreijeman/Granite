namespace Granite.Engine.Primitives;

public readonly record struct Rect
{
    public Point P1 { get; }
    public Point P2 { get; }

    public Rect(Point p1, Point p2)
    {
        P1 = p1;
        P2 = p2;
    }

    public Rect(Point origin, Size size)
    {
        P1 = origin;
        P2 = new Point(
            origin.X + size.Width - 1,
            origin.Y + size.Height - 1);
    }

    public bool Contains(Point point)
    {
        return point.X >= P1.X &&
               point.X <= P2.X &&
               point.Y >= P1.Y &&
               point.Y <= P2.Y;
    }

    public bool Intersects(Rect other)
    {
        return P1.X <= other.P2.X &&
               P2.X >= other.P1.X &&
               P1.Y <= other.P2.Y &&
               P2.Y >= other.P1.Y;
    }

    public bool TryGetIntersection(Rect other, out Rect intersection)
    {
        Point p1 = new(
            Math.Max(P1.X, other.P1.X),
            Math.Max(P1.Y, other.P1.Y));

        Point p2 = new(
            Math.Min(P2.X, other.P2.X),
            Math.Min(P2.Y, other.P2.Y));

        if (p1.X > p2.X || p1.Y > p2.Y)
        {
            intersection = default;
            return false;
        }

        intersection = new Rect(p1, p2);
        return true;
    }

    public IEnumerable<Rect> Subtract(Rect other)
    {
        if (!TryGetIntersection(other, out var intersection))
        {
            yield return this;
            yield break;
        }

        // Top part
        if (P1.Y < intersection.P1.Y)
        {
            yield return new Rect(
                new Point(P1.X, P1.Y),
                new Point(P2.X, intersection.P1.Y - 1));
        }

        // Bottom part
        if (intersection.P2.Y < P2.Y)
        {
            yield return new Rect(
                new Point(P1.X, intersection.P2.Y + 1),
                new Point(P2.X, P2.Y));
        }

        // Left part
        if (P1.X < intersection.P1.X)
        {
            yield return new Rect(
                new Point(P1.X, intersection.P1.Y),
                new Point(intersection.P1.X - 1, intersection.P2.Y));
        }

        // Right part
        if (intersection.P2.X < P2.X)
        {
            yield return new Rect(
                new Point(intersection.P2.X + 1, intersection.P1.Y),
                new Point(P2.X, intersection.P2.Y));
        }
    }

    public Rect Translate(Point offset) => new(P1.Translate(offset), P2.Translate(offset));

    public Rect ChangeOrigin(Point oldOrigin, Point newOrigin) =>
        new(P1.Rebase(oldOrigin, newOrigin), P2.Rebase(oldOrigin, newOrigin));
}