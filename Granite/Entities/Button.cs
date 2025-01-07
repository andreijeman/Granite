using Granite.Components;
using Granite.Interfaces;
using Granite.Utilities;

namespace Granite.Entities;

public class Button : Entity
{
    public Button(Vector2 size) : base(size) { }

    public void SculptModelIdle()
    {
        this.Model.SculptRectangle(
            Vector2.New(0, 0), 
            this.Model.Size, 
            Model.Cell.New(ConsoleColor.Magenta));
    }
    
    public void SculptModelHovered()
    {
        this.Model.SculptRectangle(
            Vector2.New(0, 0), 
            this.Model.Size, 
            Model.Cell.New(ConsoleColor.DarkCyan));
    }
    
}

