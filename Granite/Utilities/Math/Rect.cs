namespace Granite.Utilities;

public struct Rect
{
    public Vector2 Pos;
    public Vector2 Size;

    public Rect(Vector2 pos, Vector2 size)
    {
        Pos = pos;
        Size = size;
    }

    public static Rect New(Vector2 pos, Vector2 size)
    {
        return new Rect(pos, size);
    }
}