using Granite.Components;
using Granite.Utilities;
using Granite.Utilities.Math;

namespace Granite.Entities;

public class Entity
{
    protected Vector2 _position;
    public Vector2 Position
    {
        get => _position;
        set
        {
            if (Rect.New(_position, _size).TryGetUncoveredSections(Rect.New(value, _size), out List<Rect> sections))
            {
                foreach (Rect section in sections)
                {
                    PositionChangedEvent?.Invoke(this, section);
                }
            }
            
            _position = value;
            TriggerOnModelChangedEvent();
        }
    }

    protected Vector2 _size;
    public Vector2 Size
    {
        get => _size;
        set
        {
            _size = value;
            Model = new Cell[value.Y, value.X];
            SculptModel();
            TriggerOnModelChangedEvent();
        }
    }
    
    public Cell[,] Model { get; private set; } = new Cell[0, 0];
    
    protected Controller Controller { get; init; } = new Controller();
    
    protected virtual void SculptModel()
        => this.Model.SculptChessBoard(Vector2.New(0, 0), this.Size, ConsoleColor.Black, ConsoleColor.DarkGray);
    
    public ModelChangedDelegate? ModelChangedEvent;

    protected virtual void OnModelChangedEvent(Entity interceptor, Entity sender, Rect part, Vector2 absolutePosition)
        => ModelChangedEvent?.Invoke(interceptor, sender, part, absolutePosition);
    
    public virtual void TriggerOnModelChangedEvent()
        => OnModelChangedEvent(this, this, Rect.New(Vector2.New(0, 0), this.Size), this.Position);

    public Action<Entity, Rect>? PositionChangedEvent;
} 

public delegate void ModelChangedDelegate(Entity interceptor, Entity sender, Rect part, Vector2 absolutePosition);