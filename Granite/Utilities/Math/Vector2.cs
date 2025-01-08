namespace Granite.Utilities;

public struct Vector2
{
    public int X;
    public int Y;
    
    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2 New(int x, int y) => new Vector2(x, y);
    
    public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
}