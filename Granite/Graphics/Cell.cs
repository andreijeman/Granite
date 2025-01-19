namespace Granite.Graphics;

public struct Cell
{
    public char Character;
    public RgbColor ForegroundRgbColor;
    public RgbColor BackgroundRgbColor;
    
    public struct RgbColor
    {
        public int R;
        public int G;
        public int B;
    }

    public Cell()
    {
        Character = ' ';
    }
}