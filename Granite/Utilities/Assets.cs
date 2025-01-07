namespace Granite.Utilities;

public static class Assets
{
    public static Border Border1 = new Border('│', '─', '│', '─', '┌', '┐', '┘', '└');
    public static Border Border2 = new Border('║', '═', '║', '═', '╔', '╗', '╝', '╚');
    public static Border Border3 = new Border('█', '▀', '█', '▄', '█', '█', '█', '█');
    
    public record Border(char Left, char Top,  char Right,  char Bottom, char LeftTop, char RightTop, char RightBottom, char LeftBottom);
}