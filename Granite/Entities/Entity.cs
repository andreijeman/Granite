using Granite.Helpers;
using Granite.Interfaces;

namespace Granite.Entities;

public class Entity : Object
{
    public Controller Controller { get; init; } = new Controller();

    public Entity(Vector2 size, IFrame frame) : base(size, frame) { }
} 