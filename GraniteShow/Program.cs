using Granite.Components;
using Granite.Entities;
using Granite.Utilities;
using Granite.Utilities.Math;

Console.Clear();

MyEntity entity = new MyEntity();
entity.Position = Vector2.New(0, 0);
entity.Size = Vector2.New(10, 10);

Frame frame = new Frame();
frame.AddEntity(entity);
frame.Size = Vector2.New(10, 10);
frame.Position = Vector2.New(-2, -2);
frame.Origin = Vector2.New(0, 0);

Frame mainFrame = new Frame();
mainFrame.ModelChangedEvent += Terminal.PrintEntityModelPart;
mainFrame.AddEntity(frame);
mainFrame.Size = Vector2.New(10, 10);
mainFrame.Position = Vector2.New(2, 2);
mainFrame.Origin = Vector2.New(0, 0);

entity.Trigger();

public class MyEntity : Entity
{
    protected override void SculptModel()
    {
        Model.SculptChessBoard(Vector2.New(0, 0), Vector2.New(Size.X, Size.Y), ConsoleColor.Black, ConsoleColor.Black);
        Model.SculptBorder(Vector2.New(0, 0), Vector2.New(Size.X, Size.Y), Cell.New(ConsoleColor.Magenta, ConsoleColor.Black), Assets.Border1);
        Model.SculptText(Vector2.New(1, 1), Vector2.New(Size.X - 2, Size.Y - 2), Cell.New(ConsoleColor.Magenta, ConsoleColor.Black), 
            "GRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE");
    }

    protected override void OnEntityModelChangedEvent(Entity interceptor, Entity sender, Rect part, Vector2 absolutePosition)
    {
        InvokeModelChangedEvent(interceptor, sender, part, absolutePosition);
    }

    public void Trigger()
    {
        InvokeModelChangedEvent(this, this, Rect.New(Vector2.New(0, 0), Size), Position);
    }
}