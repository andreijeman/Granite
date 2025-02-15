using Granite.Graphics;
using Granite.Utilities;

namespace Granite.Entities;

public class Scroll : Frame, IInteractive
{
    public ModelBuilder.Border Border { get; set; } = ModelBuilder.Assets.LineBorder;
    private int _currentLine = 0;

    public void AddDown(Object2D obj)
    {
        obj.Left = 0;
        obj.Top = _currentLine;
        _currentLine += obj.Height;
        base.AddBack(obj);
    }

    public void Focus()
    {
    }

    public void Unfocus()
    {
    }

    public void ProcessPressedKey(ConsoleKey key)
    {
        if (key == ConsoleKey.UpArrow && LocalTop > 0)
        {
            LocalTop--;
        }
        else if(key == ConsoleKey.DownArrow && LocalTop + Height < _currentLine)
        {
            LocalTop++;
        }
    }
}
