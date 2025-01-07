using Granite.Components;

namespace Granite.Entities;

public class Frame : Entity
{
    public Vector2 Origin { get; set; }

    private LinkedList<Entity> _entities = new LinkedList<Entity>();

    public Frame(Vector2 size, Vector2 origin) : base(size)
    {
        Origin = origin;
    }
    
    public void AddEntity(Entity entity)
    {
        _entities.AddLast(entity);
    }

    private void GetUncovoredModelPartData()
    {
        
    }
    
    private void GetModelPartInFrameData(ref Vector2 position, ref Vector2 size, ref Vector2 point)
    {
        if (position.X + size.X - 1 > Console.BufferWidth)
        {
            size.X = Console.BufferWidth - position.X;
        }
        
        if (position.Y + size.Y - 1 > Console.BufferHeight)
        {
            size.Y = Console.BufferHeight - position.Y;
        }
        
        if (position.X < 0)
        {
            point.X = -position.X;
            position.X = 0;
        }

        if (position.Y < 0)
        {
            point.Y = -position.Y;
            position.Y = 0;
        }
    }

    private bool ContainsRect(Vector2 position, Vector2 size)
    {
        return
        ContainsPoint(position) || 
        ContainsPoint(Vector2.New(position.X + size.X - 1, position.Y)) ||
        ContainsPoint(Vector2.New(position.X + size.X - 1, position.Y + size.Y - 1)) ||
        ContainsPoint(Vector2.New(position.X, position.Y + size.Y - 1));
    }

    private bool ContainsPoint(Vector2 point)
    {
        return        
        point.X >= Origin.X && point.X < Origin.X + Model.Size.X && 
        point.Y >= Origin.Y && point.Y < Origin.Y + Model.Size.Y;
    }
}