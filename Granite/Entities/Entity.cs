using Granite.Components;
using Granite.Utilities;

namespace Granite.Entities;

public abstract class Entity
{
    public Vector2 Position { get; set; }

    private Vector2 _size;
    public Vector2 Size
    {
        get => _size;
        set
        {
            _size = value;
            Model = new Cell[value.X, value.Y];
            SculptModel();
        }
    }
    
    protected Cell[,]? Model { get; private set; }
    
    protected Controller Controller { get; init; } = new Controller();
    
    public event Action<Entity>? StateChangedEvent;
    
    protected void NotifyStateChanged(Entity entity)
    {
        StateChangedEvent?.Invoke(entity);
    }
    
    public abstract void SculptModel();
} 
