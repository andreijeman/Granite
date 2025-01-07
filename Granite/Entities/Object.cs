using Granite.Components;
using Granite.Helpers;
using Granite.Interfaces;

namespace Granite.Entities;

public class Object
{
    public Vector2 Position { get; set; }
    public Model Model { get; init; }

    private IFrame _frame;
    public IFrame Frame
    {
        get => _frame;
        set
        {
            _frame = value;
            _frame.AddObject(this);
        }
    }
    
    public Object(Vector2 size, IFrame frame)
    {
        Model = new Model(size);
        
        _frame = frame;
        _frame.AddObject(this);
    }
}