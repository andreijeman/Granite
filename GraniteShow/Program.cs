using System.Numerics;
using Granite.Components;
using Granite.Entities;
using Granite.Utilities;
using Granite.Utilities.Math;
using Vector2 = Granite.Utilities.Math.Vector2;

Console.Clear();

MyEntity e1 = new MyEntity()
{
    Color = ConsoleColor.DarkYellow,
    Size = Vector2.New(4, 2)
};

MyEntity e2 = new MyEntity()
{
    Color = ConsoleColor.Cyan,
    Size = Vector2.New(4, 2),
    Position = Vector2.New(4, 4)
};

Entity bg = new Entity() {Size = Vector2.New(Console.BufferWidth, Console.BufferHeight)};

Frame mainFrame = new Frame() {Size = Vector2.New(Console.BufferWidth, Console.BufferHeight), ModelChangedEvent = Terminal.PrintEntityModelPart};
mainFrame.AddEntity(bg);
mainFrame.AddEntity(e1);
mainFrame.AddEntity(e2);

mainFrame.TriggerOnModelChangedEvent();

var e = e1;

Console.CursorVisible = false;
while (true)
{
    var key = Console.ReadKey(true).Key;
    switch (key)
    {
        case ConsoleKey.UpArrow: e.Position += Vector2.New(0, -1); break;
        case ConsoleKey.DownArrow: e.Position += Vector2.New(0, 1); break;
        case ConsoleKey.LeftArrow: e.Position += Vector2.New(-1, 0); break;
        case ConsoleKey.RightArrow: e.Position += Vector2.New(1, 0); break;
        default: break;
    }
    
}

public class MyEntity : Entity
{
    private ConsoleColor _color;

    public ConsoleColor Color
    {
        get { return _color; }
        set
        {
            _color = value; 
            SculptModel();
        }
    }
    
    protected override void SculptModel()
    {
        this.Model.SculptRectangle(Vector2.New(0, 0), Vector2.New(this.Size.X, this.Size.Y), Cell.New(_color));
    }
}

