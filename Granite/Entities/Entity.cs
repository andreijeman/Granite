using Granite.Components;
using Granite.Utilities.Math;

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
    
    public Cell[,] Model { get; private set; } = new Cell[0, 0];
    
    protected Controller Controller { get; init; } = new Controller();
    
    protected abstract void SculptModel();
    
    public EntityModelChangedDelegate? ModelChangedEvent;
    
    protected abstract void OnEntityModelChangedEvent(Entity entity, Rect part, Vector2 absolutePosition);
    
    protected void InvokeModelChangedEvent(Entity entity, Rect part, Vector2 absolutePosition) 
        => ModelChangedEvent?.Invoke(entity, part, absolutePosition);

} 

public delegate void EntityModelChangedDelegate(Entity entity, Rect part, Vector2 absolutePosition);