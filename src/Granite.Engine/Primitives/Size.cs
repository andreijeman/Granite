namespace Granite.Engine.Primitives;

public readonly record struct Size(int Width, int Height)
{
    public static Size operator +(Size s1, Size s2) => new(s1.Width + s2.Width, s1.Height + s2.Height);
    public static Size operator -(Size s1, Size s2) => new(s1.Width - s2.Width, s1.Height - s2.Height);

    public static Size Zero { get; } = new(0, 0);
    public static Size One { get; } = new(1, 1);
}