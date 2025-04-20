namespace Granite.UI.Entities.Args;

public class EntityArgs
{
    public int Left { get; set; }
    public int Top { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Dictionary<ConsoleKey, Action> keyActionDict { get; } = new();
}