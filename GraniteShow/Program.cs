using Granite.Components;
using Granite.Entities;
using Granite.Utilities;
using Granite.Utilities.Math;

Console.Clear();

MyEntity entity = new MyEntity();
entity.Position = Vector2.New(-2, -2);
entity.Size = Vector2.New(4, 4);

Frame frame = new Frame();
frame.ModelChangedEvent += Terminal.PrintEntityModelPart;
frame.Size = Vector2.New(10, 10);
frame.AddEntity(entity);
frame.Position = Vector2.New(0, 0);
frame.Origin = Vector2.New(0, 0);

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

    protected override void OnEntityModelChangedEvent(Entity entity, Rect part, Vector2 absolutePosition)
    {
        InvokeModelChangedEvent(entity, part, absolutePosition);
    }

    public void Trigger()
    {
        InvokeModelChangedEvent(this, Rect.New(Vector2.New(0, 0), Size), Position);
    }
}