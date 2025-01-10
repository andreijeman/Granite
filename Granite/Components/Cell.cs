namespace Granite.Components;

public struct Cell
{
    public char Character;
    public ConsoleColor ForegroundColor;
    public ConsoleColor BackgroundColor;
        
    public Cell(char character, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        Character = character;
        ForegroundColor = foregroundColor;
        BackgroundColor = backgroundColor;
    }

    public static Cell New(char characher, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        return new Cell(characher, foregroundColor, backgroundColor);
    }
    
    public static Cell New(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        return new Cell(' ', foregroundColor, backgroundColor);
    }
    
    public static Cell New(ConsoleColor color)
    {
        return new Cell(' ', color, color);
    }
}