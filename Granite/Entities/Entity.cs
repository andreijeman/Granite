using Granite.Components;

namespace Granite.Entities;

public class Entity
{
    public Vector2 Position { get; set; }
    public Model Model { get; init; }
    
    protected Controller Controller { get; init; } = new Controller();
    
    public event Action<Entity>? StateChangedEvent;

    public Entity(Vector2 size) 
    {
        Model = new Model(size);
    }

    protected void NotifyStateChanged(Entity entity)
    {
        StateChangedEvent?.Invoke(entity);
    }
} 